using BinaryMapper.Core.Attributes;
using ULONG64 = System.UInt64;

namespace BinaryMapper.Windows.Minidump.Structures
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
