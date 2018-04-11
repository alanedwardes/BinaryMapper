using System;

using ULONG32 = System.UInt32;
using RVA = System.UInt32;
using USHORT = System.UInt16;
using UCHAR = System.Byte;
using BinaryMapper.Core.Attributes;

namespace BinaryMapper.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680396.aspx
    /// </summary>
    public class MINIDUMP_SYSTEM_INFO_STREAM
    {
        public PROCESSOR_ARCHITECTURE_TYPE ProcessorArchitecture;
        public USHORT ProcessorLevel;
        public USHORT ProcessorRevision;
        [Rewind]
        public USHORT Reserved0;
        public UCHAR NumberOfProcessors;
        public PRODUCT_TYPE ProductType;
        public ULONG32 MajorVersion;
        public ULONG32 MinorVersion;
        public ULONG32 BuildNumber;
        public PLATFORM_ID_TYPE PlatformId;
        public RVA CSDVersionRva;
        [Rewind]
        public USHORT Reserved1;
        public USHORT SuiteMask;
        public USHORT Reserved2;
        public CPU_INFORMATION Cpu;

        public Version VersionMarshaled => new Version((int)MajorVersion, (int)MinorVersion, (int)BuildNumber);
    }

    public enum PROCESSOR_ARCHITECTURE_TYPE : USHORT
    {
        Intel = 0,
        Mips = 1,
        Alpha = 2,
        Ppc = 3,
        Shx = 4,
        Arm = 5,
        Ia64 = 6,
        Alpha64 = 7,
        Msil = 8,
        Amd64 = 9,
        Ia32OnWin64 = 10,
    }

    public enum PRODUCT_TYPE : UCHAR
    {
        VER_NT_DOMAIN_CONTROLLER = 0x0000002,
        VER_NT_SERVER = 0x0000003,
        VER_NT_WORKSTATION = 0x0000001
    }

    public enum PLATFORM_ID_TYPE : ULONG32
    {
        VER_PLATFORM_WIN32s = 0,
        VER_PLATFORM_WIN32_WINDOWS = 1,
        VER_PLATFORM_WIN32_NT = 2
    }

    [Flags]
    public enum SUITE_TYPE : USHORT
    {
        VER_SUITE_SMALLBUSINESS = 0x00000001,
        VER_SUITE_ENTERPRISE = 0x00000002,
        VER_SUITE_BACKOFFICE = 0x00000004,
        VER_SUITE_COMMUNICATIONS = 0x00000008,
        VER_SUITE_TERMINAL = 0x00000010,
        VER_SUITE_SMALLBUSINESS_RESTRICTED = 0x00000020,
        VER_SUITE_EMBEDDEDNT = 0x00000040,
        VER_SUITE_DATACENTER = 0x00000080,
        VER_SUITE_SINGLEUSERTS = 0x00000100,
        VER_SUITE_PERSONAL = 0x00000200,
        VER_SUITE_BLADE = 0x00000400,
        VER_SUITE_EMBEDDED_RESTRICTED = 0x00000800,
        VER_SUITE_SECURITY_APPLIANCE = 0x00001000,
        VER_SUITE_STORAGE_SERVER = 0x00002000,
        VER_SUITE_COMPUTE_SERVER = 0x00004000,
        VER_SUITE_WH_SERVER = 0x00008000
    }
}
