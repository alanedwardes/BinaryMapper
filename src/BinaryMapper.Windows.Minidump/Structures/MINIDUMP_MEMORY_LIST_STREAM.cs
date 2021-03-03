using BinaryMapper.Core.Attributes;
using ULONG32 = System.UInt32;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// Contains a list of memory ranges.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_memory_list
    /// https://msdn.microsoft.com/en-us/library/ms680387.aspx
    /// </remarks>
    public class MINIDUMP_MEMORY_LIST_STREAM
    {
        /// <summary>
        /// The number of structures in the MemoryRanges array.
        /// </summary>
        public ULONG32 NumberOfMemoryRanges;
        /// <summary>
        /// An array of <see cref="MINIDUMP_MEMORY_DESCRIPTOR"/> structures.
        /// </summary>
        [ArraySize(nameof(NumberOfMemoryRanges))]
        public MINIDUMP_MEMORY_DESCRIPTOR[] MemoryRanges;
    }
}
