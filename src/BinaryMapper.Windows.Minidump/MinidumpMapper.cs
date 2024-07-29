using BinaryMapper.Core;
using BinaryMapper.Windows.Minidump.Structures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BinaryMapper.Windows.Minidump
{
    public class MinidumpMapper : IMinidumpMapper
    {
        private readonly IStreamBinaryMapper _streamBinaryMapper;

        public MinidumpMapper()
        {
            _streamBinaryMapper = new StreamBinaryMapper();
        }

        public MinidumpMapper(IStreamBinaryMapper streamBinaryMapper)
        {
            _streamBinaryMapper = streamBinaryMapper;
        }

        public Minidump ReadMinidump(Stream stream)
        {
            var minidump = new Minidump
            {
                Header = _streamBinaryMapper.ReadObject<MINIDUMP_HEADER>(stream)
            };

            stream.Position = minidump.Header.StreamDirectoryRva;
            var directories = _streamBinaryMapper.ReadArray<MINIDUMP_DIRECTORY>(stream, minidump.Header.NumberOfStreams);

            foreach (var directory in directories)
            {
                stream.Position = directory.Location.Rva;

                switch (directory.StreamType)
                {
                    case MINIDUMP_STREAM_TYPE.ThreadListStream:
                        minidump.ThreadListStream = _streamBinaryMapper.ReadObject<MINIDUMP_THREAD_LIST_STREAM>(stream);

                        // Fill referenced data
                        foreach (var minidumpThread in minidump.ThreadListStream.Threads)
                        {
                            if (minidumpThread.ThreadContext.DataSize != 0)
                            {
                                var descr = minidumpThread.ThreadContext;
                                stream.Position = descr.Rva;
                                var buffer = new byte[descr.DataSize];
                                stream.Read(buffer, 0, buffer.Length);
                                minidumpThread.ThreadContext = new MINIDUMP_LOCATION_DESCRIPTOR_WITH_DATA()
                                {
                                    Rva = descr.Rva,
                                    DataSize = descr.DataSize,
                                    MemoryData = buffer
                                };
                            }

                            if (minidumpThread.Stack.Memory.DataSize != 0)
                            {
                                var descr = minidumpThread.Stack.Memory;
                                stream.Position = descr.Rva;
                                var buffer = new byte[descr.DataSize];
                                stream.Read(buffer, 0, buffer.Length);
                                minidumpThread.Stack.Memory = new MINIDUMP_LOCATION_DESCRIPTOR_WITH_DATA()
                                {
                                    Rva = descr.Rva,
                                    DataSize = descr.DataSize,
                                    MemoryData = buffer
                                };
                            }
                        }
                        break;
                    case MINIDUMP_STREAM_TYPE.ModuleListStream:
                        minidump.ModuleListStream = _streamBinaryMapper.ReadObject<MINIDUMP_MODULE_LIST_STREAM>(stream);
                        foreach (var module in minidump.ModuleListStream.Modules)
                        {
                            stream.Position = module.ModuleNameRva;
                            module.Name = _streamBinaryMapper.ReadObject<MINIDUMP_STRING>(stream).Buffer;
                            minidump.Modules.Add(module);
                        }
                        break;
                    case MINIDUMP_STREAM_TYPE.MemoryListStream:
                        minidump.MemoryListStream = _streamBinaryMapper.ReadObject<MINIDUMP_MEMORY_LIST_STREAM>(stream);

                        // Fill referenced data
                        foreach (var memRange in minidump.MemoryListStream.MemoryRanges)
                        {
                            if (memRange.Memory.DataSize != 0)
                            {
                                var descr = memRange.Memory;
                                stream.Position = descr.Rva;
                                var buffer = new byte[descr.DataSize];
                                stream.Read(buffer, 0, buffer.Length);
                                memRange.Memory = new MINIDUMP_LOCATION_DESCRIPTOR_WITH_DATA()
                                {
                                    Rva = descr.Rva,
                                    DataSize = descr.DataSize,
                                    MemoryData = buffer
                                };
                            }
                        }

                        break;
                    case MINIDUMP_STREAM_TYPE.ExceptionStream:
                        minidump.ExceptionStream = _streamBinaryMapper.ReadObject<MINIDUMP_EXCEPTION_STREAM>(stream);
                        if (minidump.ExceptionStream.ThreadContext.DataSize != 0)
                        {
                            var descr = minidump.ExceptionStream.ThreadContext;
                            stream.Position = descr.Rva;
                            var buffer = new byte[descr.DataSize];
                            stream.Read(buffer, 0, buffer.Length);
                            minidump.ExceptionStream.ThreadContext = new MINIDUMP_LOCATION_DESCRIPTOR_WITH_DATA()
                            {
                                Rva = descr.Rva,
                                DataSize = descr.DataSize,
                                MemoryData = buffer
                            };
                        }
                        break;
                    case MINIDUMP_STREAM_TYPE.SystemInfoStream:
                        minidump.SystemInfoStream = _streamBinaryMapper.ReadObject<MINIDUMP_SYSTEM_INFO_STREAM>(stream);
                        stream.Position = minidump.SystemInfoStream.CSDVersionRva;
                        var servicePackString = _streamBinaryMapper.ReadObject<MINIDUMP_STRING>(stream);
                        minidump.SystemInfoServicePack = servicePackString.Buffer;
                        break;
                    case MINIDUMP_STREAM_TYPE.MiscInfoStream:
                        minidump.MiscInfoStream = _streamBinaryMapper.ReadObject<MINIDUMP_MISC_INFO_STREAM>(stream);
                        break;
                    case MINIDUMP_STREAM_TYPE.ThreadInfoListStream:
                        minidump.ThreadInfoListStream = _streamBinaryMapper.ReadObject<MINIDUMP_THREAD_INFO_LIST_STREAM>(stream);
                        break;

                    // Add support for reading Chromium minidumps
                    case MINIDUMP_STREAM_TYPE.MD_LINUX_CMD_LINE:
                        var strs = MDReadStrings(stream, directory.Location.DataSize);
                        minidump.CommandLine = strs.Length > 0 ? strs[0] : "";
                        break;
                    case MINIDUMP_STREAM_TYPE.MD_LINUX_ENVIRON:
                        minidump.EnvironmentVariables = SplitIntoDictionary('=',
                            MDReadStrings(stream, directory.Location.DataSize));
                        break;
                    case MINIDUMP_STREAM_TYPE.MD_LINUX_LSB_RELEASE:
                        minidump.LSBRelease = MDReadStrings(stream, directory.Location.DataSize);
                        break;
                    case MINIDUMP_STREAM_TYPE.MD_LINUX_PROC_STATUS:
                        minidump.ProcessStatus = SplitIntoDictionary(':',
                            MDReadStrings(stream, directory.Location.DataSize));
                        break;
                    case MINIDUMP_STREAM_TYPE.MD_LINUX_CPU_INFO:
                        minidump.CpuInfo = SplitProcs(MDReadStrings(stream, directory.Location.DataSize));
                        break;
                    case MINIDUMP_STREAM_TYPE.MD_LINUX_MAPS:
                        minidump.LinuxMaps = MDReadStrings(stream, directory.Location.DataSize);
                        break;

                }
            }

            return minidump;
        }

        /// <summary>
        /// Reads in a list of 0-separated strings from the linux-supporting variety of minidump
        /// </summary>
        /// <param name="stream">The stream to read from</param>
        /// <param name="size">The size the segment</param>
        /// <returns>The sttings</returns>
        string[] MDReadStrings(Stream stream, uint size)
        {
            // Read the bytes in
            var bytes = _streamBinaryMapper.ReadArray<byte>(stream, size);
            // Convert it to a string
            var str = Encoding.UTF8.GetString(bytes);
            // Split the string up on zero terminators
            var strs = str.Split(new char[] { (char)0, '\n' });
            // Remove the last empty on
            if (strs[strs.Length - 1].Length == 0)
                Array.Resize(ref strs, strs.Length - 1);
            // Return the list of strings
            return strs;
        }

        /// <summary>
        /// This is used to split a list of delineated key-value pairs into a convenient dictionary
        /// </summary>
        /// <param name="splitChar">The character to use to split</param>
        /// <param name="list">The list to split</param>
        /// <param name="first">The place to start scanning in the lsit</param>
        /// <param name="length">The number of items to scan.. less then zero set automatically</param>
        /// <returns>The dictionary</returns>
        static IDictionary<string, string> SplitIntoDictionary(char splitChar, string[] list, int first = 0, int length = -1)
        {
            // Create a dictionary for the items
            var ret = new Dictionary<string, string>();
            var chars = new char[] { splitChar };
            var last = length >= 0 ? first + length : list.Length;
            for (var idx = first; idx < last; idx++)
            {
                var item = list[idx];

                // Split the string up on zero terminators
                var strs = item.Split(chars, 2);
                // Add it to the dictionary
                var key = strs[0].Trim();
                if (key.Length > 0)
                    ret[key] = strs.Length > 1 ? strs[1].Trim() : "";
            }
            return ret;
        }


        /// <summary>
        /// Separate out the processor info into a more useful structure
        /// </summary>
        /// <param name="list"></param>
        /// <returns>The info for each of the processors</returns>
        static LinuxCpuInfo SplitProcs(string[] list)
        {
            // Create a structure to hold information for the processors
            var ret = new LinuxCpuInfo();

            int first = 0;
            // Scan to find the each of the processor groups of information
            for (var idx = 0; idx < list.Length; idx++)
            {
                // We're looking for the line to start with a processor identifier
                if (0 != list[idx].Length)
                    continue;
                // Convert the lines into a table of information
                if (first >= 0)
                {
                    // Convert this into a dictionary
                    var procInfo = SplitIntoDictionary(':', list, first, idx - first);

                    // store it
                    if (0 != procInfo.Count)
                        ret.ProcessorInfo.Add(procInfo);
                }
                first = idx;
            }
            if (first >= 0)
            {
                // Convert this into a dictionary
                ret. HardwareInfo = SplitIntoDictionary(':', list, first);
            }
            return ret;
        }


        private void WriteFixup(Stream stream, (Type, long) fixup, object data)
        {
            var oldPos = stream.Position;
            stream.Position = fixup.Item2;
            _streamBinaryMapper.WriteValue(stream, fixup.Item1, data);
            stream.Position = oldPos;
        }
        private void WriteDirectoryObject(Stream stream, List<MINIDUMP_DIRECTORY> directory, object data, MINIDUMP_STREAM_TYPE type)
        {
            if (data == null) return;

            var writePos = stream.Position;
            _streamBinaryMapper.WriteObject(stream, data.GetType(), data);

            directory.Add(new MINIDUMP_DIRECTORY
            {
                StreamType = type,
                Location = new MINIDUMP_LOCATION_DESCRIPTOR
                {
                    Rva = (uint)writePos,
                    DataSize = (uint)(stream.Position - writePos)
                }
            });
        }

        public void WriteMinidump(Stream stream, Minidump dump)
        {
            //#TODO clear the mapper, otherwise cannot call WriteMinidump multiple times with same mapper because it keeps map of RVA's

            dump.Header.Signature = "MDMP";
            //dump.Header.Flags = (MINIDUMP_TYPE)0x0000000000200041; //#TODO ??
            dump.Header.Flags = MINIDUMP_TYPE.MiniDumpWithDataSegs | MINIDUMP_TYPE.MiniDumpWithIndirectlyReferencedMemory; //#TODO ??
            dump.Header.CheckSum = 0;
            dump.Header.Version = 0xa07ca793; // Hardcoded MINIDUMP_VERSION, I got this from reading a mdmp created by DbgHelp
            dump.Header.TimeDateStamp = (uint)DateTimeOffset.Now.ToUnixTimeSeconds();

            // We fix StreamDirectoryRva/NumberOfStreams later
            var headerFixups = _streamBinaryMapper.WriteObject(stream, typeof(MINIDUMP_HEADER), dump.Header);
            
            // It is quite annoying, but the directory really needs to be at the top. (WinDbg will refuse to read RVA's when they are before the directory)
            List<MINIDUMP_DIRECTORY> directory = new List<MINIDUMP_DIRECTORY>();
            // We need to write the correct number of elements. Currently its just placeholders, we will write it again properly later
            for (int i = 0; i < 5; i++)
                directory.Add(new MINIDUMP_DIRECTORY(){Location = new MINIDUMP_LOCATION_DESCRIPTOR()});

            dump.Header.StreamDirectoryRva = (uint)stream.Position;
            dump.Header.NumberOfStreams = (uint)directory.Count;
            _streamBinaryMapper.WriteArray(stream, dump.Header.NumberOfStreams, typeof(MINIDUMP_DIRECTORY), directory.ToArray());

            // headerFixups
            WriteFixup(stream, headerFixups[0], dump.Header.NumberOfStreams);
            WriteFixup(stream, headerFixups[1], dump.Header.StreamDirectoryRva);

            directory.Clear(); // We will re-add the elements with proper data now

            // Write all streams
            {
                {
                    // Write referenced memory RVA's
                    foreach (var minidumpThread in dump.ThreadListStream.Threads)
                    {
                        {
                            if (minidumpThread.ThreadContext is MINIDUMP_LOCATION_DESCRIPTOR_WITH_DATA data)
                            {
                                var writePos = stream.Position;
                                stream.Write(data.MemoryData, 0, data.MemoryData.Length);
                                data.Rva = (uint)writePos;
                                data.DataSize = (uint)data.MemoryData.Length;
                            }
                        }
                        {
                            if (minidumpThread.Stack.Memory is MINIDUMP_LOCATION_DESCRIPTOR_WITH_DATA data)
                            {
                                var writePos = stream.Position;
                                stream.Write(data.MemoryData, 0, data.MemoryData.Length);
                                data.Rva = (uint)writePos;
                                data.DataSize = (uint)data.MemoryData.Length;
                            }
                        }
                    }

                    WriteDirectoryObject(stream, directory, dump.ThreadListStream, MINIDUMP_STREAM_TYPE.ThreadListStream);
                }
                

                // Not enabled because I don't know the sizes, and my test dumps didn't have this.
                //if (dump.ThreadInfoListStream != null)
                //{
                //    var writePos = stream.Position;
                //
                //
                //    dump.ThreadInfoListStream.SizeOfHeader = 5; //#TODO don't hardcode? Or atleast not magic numbers
                //    dump.ThreadInfoListStream.SizeOfEntry = 5;
                //    _streamBinaryMapper.WriteObject(stream, typeof(MINIDUMP_THREAD_INFO_LIST_STREAM), dump.ThreadInfoListStream);
                //
                //    directory.Add(new MINIDUMP_DIRECTORY
                //    {
                //        StreamType = MINIDUMP_STREAM_TYPE.ThreadInfoListStream,
                //        Location = new MINIDUMP_LOCATION_DESCRIPTOR
                //        {
                //            Rva = (uint)writePos,
                //            DataSize = (uint)(stream.Position - writePos)
                //        }
                //    });
                //}

                {
                    var writePos = stream.Position;
                    if (dump.ExceptionStream.ThreadContext is MINIDUMP_LOCATION_DESCRIPTOR_WITH_DATA data)
                    {
                        stream.Write(data.MemoryData, 0, data.MemoryData.Length);
                        data.Rva = (uint)writePos;
                        data.DataSize = (uint)data.MemoryData.Length;
                    }

                    WriteDirectoryObject(stream, directory, dump.ExceptionStream, MINIDUMP_STREAM_TYPE.ExceptionStream);
                }
                

                {
                    // We need to write the name first and fix up RVA
                    {
                        MINIDUMP_STRING str = new MINIDUMP_STRING()
                        {
                            Buffer = dump.SystemInfoServicePack
                        };
                        var memPos = stream.Position;
                        _streamBinaryMapper.WriteObject(stream, typeof(MINIDUMP_STRING), str);
                        dump.SystemInfoStream.CSDVersionRva = (uint)memPos;
                    }
                    WriteDirectoryObject(stream, directory, dump.SystemInfoStream, MINIDUMP_STREAM_TYPE.SystemInfoStream);
                }

                {
                    // Write data first, and set the correct RVA
                    foreach (var memDescriptor in dump.MemoryListStream.MemoryRanges)
                    {
                        Debug.Assert(memDescriptor.Memory is MINIDUMP_LOCATION_DESCRIPTOR_WITH_DATA); // When writing, we need to know the data
                        if (memDescriptor.Memory is MINIDUMP_LOCATION_DESCRIPTOR_WITH_DATA data)
                        {
                            var memPos = stream.Position;
                            stream.Write(data.MemoryData, 0, data.MemoryData.Length);
                            data.Rva = (uint)memPos;
                            data.DataSize = (uint)data.MemoryData.Length;
                        }
                    }
                    WriteDirectoryObject(stream, directory, dump.MemoryListStream, MINIDUMP_STREAM_TYPE.MemoryListStream);
                }

                {
                    // We need to write the names first and fix up RVA
                    foreach (var module in dump.ModuleListStream.Modules)
                    {
                        MINIDUMP_STRING str = new MINIDUMP_STRING()
                        {
                            Buffer = module.Name
                        };
                        var memPos = stream.Position;
                        _streamBinaryMapper.WriteObject(stream, typeof(MINIDUMP_STRING), str);
                        module.ModuleNameRva = (uint)memPos;
                    }
                    WriteDirectoryObject(stream, directory, dump.ModuleListStream, MINIDUMP_STREAM_TYPE.ModuleListStream);
                }

                Debug.Assert(directory.Count == dump.Header.NumberOfStreams);
                stream.Position = dump.Header.StreamDirectoryRva;
                _streamBinaryMapper.WriteArray(stream, dump.Header.NumberOfStreams, typeof(MINIDUMP_DIRECTORY), directory.ToArray());
            }


        }

    }
}