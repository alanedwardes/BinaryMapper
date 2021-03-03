using ULONG32 = System.UInt32;
using ULONG64 = System.UInt64;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// Contains information for a specific thread.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_thread
    /// https://msdn.microsoft.com/en-us/library/ms680517.aspx
    /// </remarks>
    public class MINIDUMP_THREAD
    {
        /// <summary>
        /// The identifier of the thread.
        /// </summary>
        public ULONG32 ThreadId;
        /// <summary>
        /// The suspend count for the thread. If the suspend count is greater 
        /// than zero, the thread is suspended; otherwise, the thread is not
        /// suspended. The maximum value is MAXIMUM_SUSPEND_COUNT.
        /// </summary>
        public ULONG32 SuspendCount;
        /// <summary>
        /// The priority class of the thread.
        /// </summary>
        public ULONG32 PriorityClass;
        /// <summary>
        /// The priority level of the thread.
        /// </summary>
        public ULONG32 Priority;
        /// <summary>
        /// The thread environment block.
        /// </summary>
        public ULONG64 Teb;
        /// <summary>
        /// A <see cref="MINIDUMP_MEMORY_DESCRIPTOR"/> structure.
        /// </summary>
        public MINIDUMP_MEMORY_DESCRIPTOR Stack;
        /// <summary>
        /// A <see cref="MINIDUMP_LOCATION_DESCRIPTOR"/> structure.
        /// </summary>
        public MINIDUMP_LOCATION_DESCRIPTOR ThreadContext;
    }
}
