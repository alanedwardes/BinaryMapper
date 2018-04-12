using BinaryMapper.Core;
using BinaryMapper.Structures;
using System;
using System.IO;

namespace BinaryMapper.Minidump
{
    public class MinidumpMapper : IMinidumpMapper
    {
        private const uint MD_HEADER_SIGNATURE = 0x504d444d;
        private const uint MD_HEADER_VERSION = 0x0000a793;

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

            // Only the low-order word is MINIDUMP_VERSION
            var lowerVersion = minidump.Header.Version & 0xffff;
            if (minidump.Header.Signature != MD_HEADER_SIGNATURE || lowerVersion != MD_HEADER_VERSION)
            {
                throw new FormatException("Unknown dump signature or version.");
            }

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
                            var moduleNameString = _streamBinaryMapper.ReadObject<MINIDUMP_STRING>(stream);
                            minidump.Modules.Add(moduleNameString.Buffer, module);
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
                }
            }

            return minidump;
        }
    }
}
