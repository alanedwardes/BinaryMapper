
using ULONG32 = System.UInt32;
using BinaryMapper.Core.Attributes;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680396.aspx
    /// </summary>
    public sealed class CPU_INFORMATION_X86_CPUINFO
    {
        [ArraySize(3)]
        public ULONG32[] VendorId;
        public ULONG32 VersionInformation;
        public ULONG32 FeatureInformation;
        public ULONG32 AMDExtendedCpuFeatures;
    }
}
