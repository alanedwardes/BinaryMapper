using BinaryMapper.Core.Attributes;
using ULONG32 = System.UInt32;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// Contains a list of threads.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_thread_list
    /// https://msdn.microsoft.com/en-us/library/ms680515.aspx
    /// </remarks>
    public class MINIDUMP_THREAD_LIST_STREAM
    {
        /// <summary>
        /// The number of structures in the Threads array.
        /// </summary>
        public ULONG32 NumberOfThreads;
        /// <summary>
        /// An array of <see cref="MINIDUMP_THREAD"/> structures.
        /// </summary>
        [ArraySize(nameof(NumberOfThreads))]
        public MINIDUMP_THREAD[] Threads;
    }
}
