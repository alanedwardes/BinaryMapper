using BinaryMapper.Core;
using BinaryMapper.Windows.Executable.Structures;
using System.IO;

namespace BinaryMapper.Windows.Executable
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

            //var resourceSectionHeader = executable.ImageSectionHeaders.SingleOrDefault(x => x.Name == ".rsrc");
            //if (resourceSectionHeader != null)
            //{
            //    stream.Position = resourceSectionHeader.PointerToRawData;
            //    var directory = _streamBinaryMapper.ReadObject<IMAGE_RESOURCE_DIRECTORY>(stream);
            //    var entries = _streamBinaryMapper.ReadArray<IMAGE_RESOURCE_DIRECTORY_ENTRY>(stream, (uint)directory.NumberOfIdEntries + directory.NumberOfNamedEntries);
            //    foreach (var entry in entries)
            //    {
            //        if (entry.IsDirectory)
            //        {
            //            stream.Position = entry.DirectoryAddress;
            //            var test = _streamBinaryMapper.ReadObject<IMAGE_RESOURCE_DIRECTORY_ENTRY>(stream);
            //            stream.Position = test.OffsetToData;
            //            var test2 = _streamBinaryMapper.ReadObject<IMAGE_RESOURCE_DATA_ENTRY>(stream);
            //        }

            //        stream.Position = entry.OffsetToData;
            //        var e = _streamBinaryMapper.ReadObject<IMAGE_RESOURCE_DATA_ENTRY>(stream);
            //    }
            //}
            
            return executable;
        }
    }
}
