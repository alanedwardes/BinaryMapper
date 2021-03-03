using ULONG64 = System.UInt64;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// Describes a range of memory.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_memory_descriptor
    /// https://msdn.microsoft.com/en-us/library/ms680384.aspx
    /// </remarks>
    public class MINIDUMP_MEMORY_DESCRIPTOR
    {
        /// <summary>
        /// The starting address of the memory range.
        /// </summary>
        public ULONG64 StartOfMemoryRange;
        /// <summary>
        /// A <see cref="MINIDUMP_LOCATION_DESCRIPTOR"/> structure.
        /// </summary>
        public MINIDUMP_LOCATION_DESCRIPTOR Memory;
    }
}
