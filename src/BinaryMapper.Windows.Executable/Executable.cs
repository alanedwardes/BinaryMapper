using BinaryMapper.Windows.Executable.Structures;

namespace BinaryMapper.Windows.Executable
{
    public class Executable
    {
        public IMAGE_DOS_HEADER DosHeader { get; set; }
        public IMAGE_PE_HEADER PeHeader { get; set; }
        public COFF_HEADER CoffHeader { get; set; }

        public IMAGE_OPTIONAL_HEADER OptionalHeader { get; set; }
        public IMAGE_OPTIONAL_HEADER64 OptionalHeader64 { get; set; }

        public IMAGE_SECTION_HEADER[] ImageSectionHeaders { get; set; }
    }
}
