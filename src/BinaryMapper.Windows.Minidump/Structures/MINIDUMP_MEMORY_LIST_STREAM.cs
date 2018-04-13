
using ULONG32 = System.UInt32;
using BinaryMapper.Core.Attributes;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680387.aspx
    /// </summary>
    public class MINIDUMP_MEMORY_LIST_STREAM
    {
        public ULONG32 NumberOfMemoryRanges;
        [ArraySize(nameof(NumberOfMemoryRanges))]
        public MINIDUMP_MEMORY_DESCRIPTOR[] MemoryRanges;
    }
}
