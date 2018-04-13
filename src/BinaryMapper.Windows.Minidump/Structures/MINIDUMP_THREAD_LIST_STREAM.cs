
using ULONG32 = System.UInt32;
using BinaryMapper.Core.Attributes;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680515.aspx
    /// </summary>
    public class MINIDUMP_THREAD_LIST_STREAM
    {
        public ULONG32 NumberOfThreads;
        [ArraySize(nameof(NumberOfThreads))]
        public MINIDUMP_THREAD[] Threads;
    }
}
