using DWORD = System.UInt32;

namespace BinaryMapper.Windows.Executable.Structures
{
    public class IMAGE_RESOURCE_DIRECTORY_ENTRY
    {
        public DWORD Name;
        public DWORD OffsetToData;
    }
}
