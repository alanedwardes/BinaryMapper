using WORD = System.UInt16;
using ULONG = System.UInt32;
using UCHAR = System.Byte;
using BinaryMapper.Core.Attributes;

namespace BinaryMapper.Windows.Executable.Structures
{
    public class IMAGE_OPTIONAL_HEADER
    {
        public WORD Magic;
        public UCHAR MajorLinkerVersion;
        UCHAR MinorLinkerVersion;
        public ULONG SizeOfCode;
        public ULONG SizeOfInitializedData;
        public ULONG SizeOfUninitializedData;
        public ULONG AddressOfEntryPoint;
        public ULONG BaseOfCode;
        public ULONG BaseOfData;
        public ULONG ImageBase;
        public ULONG SectionAlignment;
        public ULONG FileAlignment;
        public WORD MajorOperatingSystemVersion;
        public WORD MinorOperatingSystemVersion;
        public WORD MajorImageVersion;
        public WORD MinorImageVersion;
        public WORD MajorSubsystemVersion;
        public WORD MinorSubsystemVersion;
        public ULONG Win32VersionValue;
        public ULONG SizeOfImage;
        public ULONG SizeOfHeaders;
        public ULONG CheckSum;
        public WORD Subsystem;
        public WORD DllCharacteristics;
        public ULONG SizeOfStackReserve;
        public ULONG SizeOfStackCommit;
        public ULONG SizeOfHeapReserve;
        public ULONG SizeOfHeapCommit;
        public ULONG LoaderFlags;
        public ULONG NumberOfRvaAndSizes;
        [ArraySize(16)]
        public IMAGE_DATA_DIRECTORY[] DataDirectory;
    }
}
