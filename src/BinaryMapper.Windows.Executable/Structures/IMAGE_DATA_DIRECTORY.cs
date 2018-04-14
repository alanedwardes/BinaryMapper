using DWORD = System.UInt32;

namespace BinaryMapper.Windows.Executable.Structures
{
    public class IMAGE_DATA_DIRECTORY
    {
        public DWORD VirtualAddress;
        public DWORD Size;
    }
}