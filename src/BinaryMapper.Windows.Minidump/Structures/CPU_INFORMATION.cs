using BinaryMapper.Core.Attributes;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680396.aspx
    /// </summary>
    public sealed class CPU_INFORMATION
    {
        /// <summary>
        /// The CPU information obtained from the CPUID instruction. This structure is supported only for x86 computers.
        /// </summary>
        [Rewind]
        public CPU_INFORMATION_X86_CPUINFO X86CpuInfo;
        /// <summary>
        /// Other CPU information. This structure is supported only for non-x86 computers.
        /// </summary>
        public CPU_INFORMATION_OTHER_CPUINFO OtherCpuInfo;
    }
}
