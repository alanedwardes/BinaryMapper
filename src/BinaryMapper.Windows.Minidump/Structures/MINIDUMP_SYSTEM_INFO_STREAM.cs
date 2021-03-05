using BinaryMapper.Core.Attributes;
using System;
using RVA = System.UInt32;
using UCHAR = System.Byte;
using ULONG32 = System.UInt32;
using USHORT = System.UInt16;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// Contains processor and operating system information.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_system_info
    /// https://msdn.microsoft.com/en-us/library/ms680396.aspx
    /// </remarks>
    public class MINIDUMP_SYSTEM_INFO_STREAM
    {
        /// <summary>
        /// The system's processor architecture. 
        /// </summary>
        public PROCESSOR_ARCHITECTURE_TYPE ProcessorArchitecture;
        /// <summary>
        /// The system's architecture-dependent processor level.
        /// </summary>
        public USHORT ProcessorLevel;
        /// <summary>
        /// The architecture-dependent processor revision.
        /// </summary>
        public USHORT ProcessorRevision;
        /// <summary>
        /// This member is reserved for future use and must be zero.
        /// </summary>
        [Rewind]
        public USHORT Reserved0;
        /// <summary>
        /// The number of processors in the system.
        /// </summary>
        public UCHAR NumberOfProcessors;
        /// <summary>
        /// Any additional information about the system. This member can be one of the following values.
        /// </summary>
        public PRODUCT_TYPE ProductType;
        /// <summary>
        /// The major version number of the operating system. 
        /// </summary>
        public ULONG32 MajorVersion;
        /// <summary>
        /// The minor version number of the operating system.
        /// </summary>
        public ULONG32 MinorVersion;
        /// <summary>
        /// The build number of the operating system.
        /// </summary>
        public ULONG32 BuildNumber;
        /// <summary>
        /// The operating system platform.
        /// </summary>
        public PLATFORM_ID_TYPE PlatformId;
        /// <summary>
        /// An RVA (from the beginning of the dump) to a MINIDUMP_STRING that describes the latest Service Pack installed on the system. If no Service Pack has been installed, the string is empty.
        /// </summary>
        public RVA CSDVersionRva;
        /// <summary>
        /// This member is reserved for future use.
        /// </summary>
        [Rewind]
        public USHORT Reserved1;
        /// <summary>
        /// The bit flags that identify the product suites available on the system.
        /// </summary>
        public SUITE_TYPE SuiteMask;
        /// <summary>
        /// This member is reserved for future use.
        /// </summary>
        public USHORT Reserved2;
        public CPU_INFORMATION Cpu;

        public Version VersionMarshaled => new Version((int)MajorVersion, (int)MinorVersion, (int)BuildNumber);
        public Version ProcessorRevisionMarshaled => new Version((byte)((ProcessorRevision >> 8) & 0xFF), (byte)ProcessorRevision);
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
