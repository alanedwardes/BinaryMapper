using BinaryMapper.Core;
using BinaryMapper.Windows.Executable.Structures;
using System.IO;

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
            executable.OptionalHeader = _streamBinaryMapper.ReadObject<IMAGE_OPTIONAL_HEADER>(stream);

            return executable;
        }
    }
}
