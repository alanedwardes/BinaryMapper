using BinaryMapper.Core.Attributes;
using ULONG32 = System.UInt32;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// The CPU information obtained from the CPUID instruction. This structure is supported only for x86 computers.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_system_info
    /// https://msdn.microsoft.com/en-us/library/ms680396.aspx
    /// </remarks>
    public sealed class CPU_INFORMATION_X86_CPUINFO
    {
        /// <summary>
        /// CPUID subfunction 0.
        /// </summary>
        [ArraySize(3)]
        public ULONG32[] VendorId;
        /// <summary>
        /// CPUID subfunction 1. Value of EAX.
        /// </summary>
        public ULONG32 VersionInformation;
        /// <summary>
        /// CPUID subfunction 1. Value of EDX.
        /// </summary>
        public ULONG32 FeatureInformation;
        /// <summary>
        /// CPUID subfunction 80000001. Value of EBX. This member is supported only if the vendor is "AuthenticAMD".
        /// </summary>
        public ULONG32 AMDExtendedCpuFeatures;
    }
}
