using BinaryMapper.Core.Attributes;
using ULONG64 = System.UInt64;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// Other CPU information. This structure is supported only for non-x86 computers.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_system_info
    /// https://msdn.microsoft.com/en-us/library/ms680396.aspx
    /// </remarks>
    public sealed class CPU_INFORMATION_OTHER_CPUINFO
    {
        /// <summary>
        /// For a list of possible values, see the IsProcessorFeaturePresent function.
        /// </summary>
        [ArraySize(2)]
        public ULONG64[] ProcessorFeatures;
    }
}
