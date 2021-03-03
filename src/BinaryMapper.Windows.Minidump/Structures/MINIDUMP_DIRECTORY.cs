using ULONG32 = System.UInt32;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// Contains the information needed to access a specific data stream in a minidump file.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_directory
    /// https://msdn.microsoft.com/en-us/library/ms680365.aspx
    /// </remarks>
    public class MINIDUMP_DIRECTORY
    {
        /// <summary>
        /// The type of data stream.  This member can be one of the values in
        /// the <see cref="MINIDUMP_STREAM_TYPE"/> enumeration.
        /// </summary>
        public MINIDUMP_STREAM_TYPE StreamType;
        /// <summary>
        /// A <see cref="MINIDUMP_LOCATION_DESCRIPTOR"/> structure that specifies
        /// the location of the data stream.
        /// </summary>
        public MINIDUMP_LOCATION_DESCRIPTOR Location;
    }

    /// <summary>
    /// Represents the type of a minidump data stream.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ne-minidumpapiset-minidump_stream_type
    /// </remarks>
    public enum MINIDUMP_STREAM_TYPE : ULONG32
    {
        /// <summary>
        /// Reserved. Do not use this enumeration value.
        /// </summary>
        UnusedStream = 0,
        /// <summary>
        /// Reserved. Do not use this enumeration value.
        /// </summary>
        ReservedStream0 = 1,
        /// <summary>
        /// Reserved. Do not use this enumeration value.
        /// </summary>
        ReservedStream1 = 2,
        /// <summary>
        /// The stream contains thread information. 
        /// </summary>
        ThreadListStream = 3,
        /// <summary>
        /// The stream contains module information.
        /// See also <see cref="MINIDUMP_MODULE_LIST_STREAM"/> and <see cref="MINIDUMP_MODULE"/>
        /// </summary>
        ModuleListStream = 4,
        /// <summary>
        /// The stream contains memory allocation information. 
        /// </summary>
        MemoryListStream = 5,
        /// <summary>
        /// The stream contains exception information.
        /// </summary>
        ExceptionStream = 6,
        /// <summary>
        /// The stream contains general system information.
        /// </summary>
        SystemInfoStream = 7,
        /// <summary>
        /// The stream contains extended thread information. 
        /// </summary>
        ThreadExListStream = 8,
        /// <summary>
        /// The stream contains memory allocation information. 
        /// </summary>
        Memory64ListStream = 9,
        /// <summary>
        /// The stream contains an ANSI string used for documentation purposes.
        /// </summary>
        CommentStreamA = 10,
        /// <summary>
        /// The stream contains a Unicode string used for documentation purposes.
        /// </summary>
        CommentStreamW = 11,
        /// <summary>
        /// The stream contains high-level information about the active operating system handles. 
        /// </summary>
        HandleDataStream = 12,
        /// <summary>
        /// The stream contains function table information.
        /// </summary>
        FunctionTableStream = 13,
        /// <summary>
        /// The stream contains module information for the unloaded modules. 
        /// </summary>
        UnloadedModuleListStream = 14,
        /// <summary>
        /// The stream contains miscellaneous information. 
        /// </summary>
        MiscInfoStream = 15,
        /// <summary>
        /// The stream contains memory region description information. It corresponds to the information that would be returned for the process from the VirtualQuery function.
        /// </summary>
        MemoryInfoListStream = 16,
        /// <summary>
        /// The stream contains thread state information. 
        /// </summary>
        ThreadInfoListStream = 17,
        /// <summary>
        /// This stream contains operation list information. 
        /// </summary>
        HandleOperationListStream = 18,
        TokenStream,
        JavaScriptDataStream,
        SystemMemoryInfoStream,
        ProcessVmCountersStream,
        IptTraceStream,
        ThreadNamesStream,
        ceStreamNull,
        ceStreamSystemInfo,
        ceStreamException,
        ceStreamModuleList,
        ceStreamProcessList,
        ceStreamThreadList,
        ceStreamThreadContextList,
        ceStreamThreadCallStackList,
        ceStreamMemoryVirtualList,
        ceStreamMemoryPhysicalList,
        ceStreamBucketParameters,
        ceStreamProcessModuleMap,
        ceStreamDiagnosisList,
        LastReservedStream = 0xffff,

        // From:
        // https://chromium.googlesource.com/breakpad/breakpad/+/master/src/google_breakpad/common/minidump_format.h
        /* Breakpad extension types.  0x4767 = "Gg" */
        MD_BREAKPAD_INFO_STREAM = 0x47670001,  /* MDRawBreakpadInfo  */
        MD_ASSERTION_INFO_STREAM = 0x47670002,  /* MDRawAssertionInfo */
        /* These are additional minidump stream values which are specific to
         * the linux breakpad implementation. */
        /// <summary>
        /// The text from /proc/cpuinfo
        /// </summary>
        MD_LINUX_CPU_INFO = 0x47670003,
        /// <summary>
        /// The text from /proc/$x/status
        /// </summary>
        MD_LINUX_PROC_STATUS = 0x47670004,
        /// <summary>
        /// The text from /etc/lsb-release
        /// </summary>
        MD_LINUX_LSB_RELEASE = 0x47670005,
        /// <summary>
        /// The text from /proc/$x/cmdline
        /// </summary>
        MD_LINUX_CMD_LINE = 0x47670006,
        /// <summary>
        /// The text from /proc/$x/environ
        /// </summary>
        MD_LINUX_ENVIRON = 0x47670007,
        /// <summary>
        /// The text from /proc/$x/auxv
        /// </summary>
        MD_LINUX_AUXV = 0x47670008,
        /// <summary>
        /// The text from /proc/$x/maps
        /// </summary>
        MD_LINUX_MAPS = 0x47670009,
        MD_LINUX_DSO_DEBUG = 0x4767000A,  /* MDRawDebug{32,64}  */
    }
}
