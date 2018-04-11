using BinaryMapper.Core.Attributes;

namespace BinaryMapper.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680396.aspx
    /// </summary>
    public sealed class CPU_INFORMATION
    {
        [Rewind]
        public CPU_INFORMATION_X86_CPUINFO X86CpuInfo;
        public CPU_INFORMATION_OTHER_CPUINFO OtherCpuInfo;
    }
}
