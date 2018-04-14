using DWORD = System.UInt32;

namespace BinaryMapper.Windows.Executable.Structures
{
    public class IMAGE_RESOURCE_DATA_ENTRY
    {
        public DWORD Data;
        public DWORD Size;
        public DWORD CodePage;
        public DWORD Reserved;
    }
}
