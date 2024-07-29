using BinaryMapper.Core.Attributes;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680396.aspx
    /// Note the order is switched to the C definition, because for our [Rewind] attribute, we need the smaller structure to come first
    /// </summary>
    public sealed class CPU_INFORMATION
    {
        /// <summary>
        /// Other CPU information. This structure is supported only for non-x86 computers.
        /// </summary>
        [Rewind]
        public CPU_INFORMATION_OTHER_CPUINFO OtherCpuInfo;
        /// <summary>
        /// The CPU information obtained from the CPUID instruction. This structure is supported only for x86 computers.
        /// </summary>
        public CPU_INFORMATION_X86_CPUINFO X86CpuInfo;
    }
}
