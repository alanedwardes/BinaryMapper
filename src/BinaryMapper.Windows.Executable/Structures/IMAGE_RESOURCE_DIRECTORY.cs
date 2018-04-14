using WORD = System.UInt16;
using DWORD = System.UInt32;
using BinaryMapper.Core.Attributes;

namespace BinaryMapper.Windows.Executable.Structures
{
    public class IMAGE_RESOURCE_DIRECTORY
    {
        public DWORD Characteristics;
        public DWORD TimeDateStamp;
        public WORD MajorVersion;
        public WORD MinorVersion;
        public WORD NumberOfNamedEntries;
        public WORD NumberOfIdEntries;
    }
}
