
using ULONG32 = System.UInt32;
using RVA = System.UInt32;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680383.aspx
    /// </summary>
    public class MINIDUMP_LOCATION_DESCRIPTOR
    {
        public ULONG32 DataSize;
        public RVA Rva;
    }
}
