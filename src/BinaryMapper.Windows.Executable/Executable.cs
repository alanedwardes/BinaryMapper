using BinaryMapper.Windows.Executable.Structures;

namespace BinaryMapper.Windows.Minidump
{
    public class Executable
    {
        public IMAGE_DOS_HEADER DosHeader { get; set; }
        public IMAGE_PE_HEADER PeHeader { get; set; }
        public COFF_HEADER CoffHeader { get; set; }
        public IMAGE_OPTIONAL_HEADER OptionalHeader { get; set; }
    }
}
