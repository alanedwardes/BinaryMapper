using ULONG64 = System.UInt64;
using BinaryMapper.Core.Attributes;

namespace BinaryMapper.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680396.aspx
    /// </summary>
    public sealed class CPU_INFORMATION_OTHER_CPUINFO
    {
        [ArraySize(2)]
        public ULONG64[] ProcessorFeatures;
    }
}
