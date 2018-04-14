using BinaryMapper.Core;
using BinaryMapper.Windows.Executable.Structures;
using System;
using System.IO;
using System.Linq;

namespace BinaryMapper.Windows.Minidump
{
    public class ExecutableMapper : IExecutableMapper
    {
        private readonly IStreamBinaryMapper _streamBinaryMapper;

        public ExecutableMapper()
        {
            _streamBinaryMapper = new StreamBinaryMapper();
        }

        public ExecutableMapper(IStreamBinaryMapper streamBinaryMapper)
        {
            _streamBinaryMapper = streamBinaryMapper;
        }

        public Executable ReadExecutable(Stream stream)
        {
            var executable = new Executable
            {
                DosHeader = _streamBinaryMapper.ReadObject<IMAGE_DOS_HEADER>(stream)
            };

            stream.Position = executable.DosHeader.e_lfanew;
            executable.PeHeader = _streamBinaryMapper.ReadObject<IMAGE_PE_HEADER>(stream);
            executable.CoffHeader = _streamBinaryMapper.ReadObject<COFF_HEADER>(stream);

            // Peek at the value of the image optional header's "magic"
            // value to determine which version of the header to parse.
            long beforeHeaderPeekPosition = stream.Position;
            IMAGE_OPTIONAL_HDR_MAGIC headerMagic = _streamBinaryMapper.ReadValue<IMAGE_OPTIONAL_HDR_MAGIC>(stream);
            stream.Position = beforeHeaderPeekPosition;

            if (headerMagic == IMAGE_OPTIONAL_HDR_MAGIC.IMAGE_NT_OPTIONAL_HDR64_MAGIC)
            {
                executable.OptionalHeader64 = _streamBinaryMapper.ReadObject<IMAGE_OPTIONAL_HEADER64>(stream);
            }
            else
            {
                executable.OptionalHeader = _streamBinaryMapper.ReadObject<IMAGE_OPTIONAL_HEADER>(stream);
            }

            executable.ImageSectionHeaders = _streamBinaryMapper.ReadArray<IMAGE_SECTION_HEADER>(stream, executable.CoffHeader.NumberOfSections);

            var resourceSectionHeader = executable.ImageSectionHeaders.SingleOrDefault(x => x.Name == ".rsrc");
            if (resourceSectionHeader != null)
            {
                stream.Position = resourceSectionHeader.PointerToRawData;
                var directory = _streamBinaryMapper.ReadObject<IMAGE_RESOURCE_DIRECTORY>(stream);
                var entries = _streamBinaryMapper.ReadArray<IMAGE_RESOURCE_DIRECTORY_ENTRY>(stream, (uint)directory.NumberOfIdEntries + directory.NumberOfNamedEntries);
                foreach (var entry in entries)
                {
                }
            }
            
            return executable;
        }
    }
}
