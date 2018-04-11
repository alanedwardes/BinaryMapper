
using ULONG32 = System.UInt32;
using ULONG64 = System.UInt64;

namespace BinaryMapper.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680517.aspx
    /// </summary>
    public class MINIDUMP_THREAD
    {
        public ULONG32 ThreadId;
        public ULONG32 SuspendCount;
        public ULONG32 PriorityClass;
        public ULONG32 Priority;
        public ULONG64 Teb;
        public MINIDUMP_MEMORY_DESCRIPTOR Stack;
        public MINIDUMP_LOCATION_DESCRIPTOR ThreadContext;
    }
}
