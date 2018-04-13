
using ULONG32 = System.UInt32;
using ULONG64 = System.UInt64;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680510.aspx
    /// </summary>
    public class MINIDUMP_THREAD_INFO
    {
        public ULONG32 ThreadId;
        public ULONG32 DumpFlags;
        public ULONG32 DumpError;
        public ULONG32 ExitStatus;
        public ULONG64 CreateTime;
        public ULONG64 ExitTime;
        public ULONG64 KernelTime;
        public ULONG64 UserTime;
        public ULONG64 StartAddress;
        public ULONG64 Affinity;
    }
}
