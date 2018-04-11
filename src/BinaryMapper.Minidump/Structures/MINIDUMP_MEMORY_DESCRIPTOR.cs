using ULONG64 = System.UInt64;

namespace BinaryMapper.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680384.aspx
    /// </summary>
    public class MINIDUMP_MEMORY_DESCRIPTOR
    {
        public ULONG64 StartOfMemoryRange;
        public MINIDUMP_LOCATION_DESCRIPTOR Memory;
    }
}
