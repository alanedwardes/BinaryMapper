using ULONG32 = System.UInt32;
using ULONG64 = System.UInt64;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// Contains thread state information.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_thread_info
    /// https://msdn.microsoft.com/en-us/library/ms680510.aspx
    /// </remarks>
    public class MINIDUMP_THREAD_INFO
    {
        /// <summary>
        /// The identifier of the thread.
        /// </summary>
        public ULONG32 ThreadId;
        /// <summary>
        /// The flags that indicate the thread state.
        /// </summary>
        public ULONG32 DumpFlags;
        /// <summary>
        /// An HRESULT value that indicates the dump status.
        /// </summary>
        public ULONG32 DumpError;
        /// <summary>
        /// The thread termination status code.
        /// </summary>
        public ULONG32 ExitStatus;
        /// <summary>
        /// The time when the thread was created, in 100-nanosecond intervals since January 1, 1601 (UTC).
        /// </summary>
        public ULONG64 CreateTime;
        /// <summary>
        /// The time when the thread exited, in 100-nanosecond intervals since January 1, 1601 (UTC).
        /// </summary>
        public ULONG64 ExitTime;
        /// <summary>
        /// The time executed in kernel mode, in 100-nanosecond intervals.
        /// </summary>
        public ULONG64 KernelTime;
        /// <summary>
        /// The time executed in user mode, in 100-nanosecond intervals.
        /// </summary>
        public ULONG64 UserTime;
        /// <summary>
        /// The starting address of the thread.
        /// </summary>
        public ULONG64 StartAddress;
        /// <summary>
        /// The processor affinity mask.
        /// </summary>
        public ULONG64 Affinity;
    }
}
