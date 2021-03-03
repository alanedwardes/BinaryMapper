using BinaryMapper.Core;
using BinaryMapper.Windows.Minidump.Structures;
using System;
using System.Collections.Generic;
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
                        break;
                    case MINIDUMP_STREAM_TYPE.ModuleListStream:
                        minidump.ModuleListStream = _streamBinaryMapper.ReadObject<MINIDUMP_MODULE_LIST_STREAM>(stream);
                        foreach (var module in minidump.ModuleListStream.Modules)
                        {
                            stream.Position = module.ModuleNameRva;
                            module._name = _streamBinaryMapper.ReadObject<MINIDUMP_STRING>(stream).Buffer;
                            minidump.Modules.Add(module);
                        }
                        break;
                    case MINIDUMP_STREAM_TYPE.MemoryListStream:
                        minidump.MemoryListStream = _streamBinaryMapper.ReadObject<MINIDUMP_MEMORY_LIST_STREAM>(stream);
                        break;
                    case MINIDUMP_STREAM_TYPE.ExceptionStream:
                        minidump.ExceptionStream = _streamBinaryMapper.ReadObject<MINIDUMP_EXCEPTION_STREAM>(stream);
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
                        minidump.cmdLine = strs.Length > 0 ? strs[0] : "";
                        break;
                    case MINIDUMP_STREAM_TYPE.MD_LINUX_ENVIRON:
                        minidump.environ = SplitIntoDictionary('=',
                            MDReadStrings(stream, directory.Location.DataSize));
                        break;
                    case MINIDUMP_STREAM_TYPE.MD_LINUX_LSB_RELEASE:
                        minidump.LSBRelease = MDReadStrings(stream, directory.Location.DataSize);
                        break;
                    case MINIDUMP_STREAM_TYPE.MD_LINUX_PROC_STATUS:
                        minidump.procStatus = SplitIntoDictionary(':',
                            MDReadStrings(stream, directory.Location.DataSize));
                        break;
                    case MINIDUMP_STREAM_TYPE.MD_LINUX_CPU_INFO:
                        minidump.CPUInfo = SplitProcs(MDReadStrings(stream, directory.Location.DataSize));
                        break;
                    case MINIDUMP_STREAM_TYPE.MD_LINUX_MAPS:
                        minidump.linuxMaps = MDReadStrings(stream, directory.Location.DataSize);
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
        static Linux_CPUInfo SplitProcs(string[] list)
        {
            // Create a structure to hold information for the processors
            var ret = new Linux_CPUInfo();

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
                        ret.processorInfo.Add(procInfo);
                }
                first = idx;
            }
            if (first >= 0)
            {
                // Convert this into a dictionary
                ret. hardwareInfo = SplitIntoDictionary(':', list, first);
            }
            return ret;
        }
    }
}