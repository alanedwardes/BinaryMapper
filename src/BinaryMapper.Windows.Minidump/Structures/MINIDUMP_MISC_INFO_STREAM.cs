using BinaryMapper.Core;
using System;
using ULONG32 = System.UInt32;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// Contains a variety of information.
    /// </summary>
    /// <remarks>
    /// https://msdn.microsoft.com/en-us/library/ms680389.aspx
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_misc_info
    /// </remarks>
    public class MINIDUMP_MISC_INFO_STREAM
    {
        /// <summary>
        /// The size of the structure, in bytes.
        /// </summary>
        public ULONG32 SizeOfInfo;
        /// <summary>
        /// The flags that indicate the valid members of this structure. 
        /// </summary>
        public MINIDUMP_MISC_TYPE Flags1;
        /// <summary>
        /// The identifier of the process. If Flags1 does not specify MINIDUMP_MISC1_PROCESS_ID, this member is unused.
        /// </summary>
        public ULONG32 ProcessId;
        /// <summary>
        /// The creation time of the process, in time_t format. If Flags1 does not specify MINIDUMP_MISC1_PROCESS_TIMES, this member is unused.
        /// </summary>
        public ULONG32 ProcessCreateTime;
        /// <summary>
        /// The time the process has executed in user mode, in seconds. The time that each of the threads of the process has executed in user mode is determined, then all these times are summed to obtain this value. If Flags1 does not specify MINIDUMP_MISC1_PROCESS_TIMES, this member is unused.
        /// </summary>
        public ULONG32 ProcessUserTime;
        /// <summary>
        /// The time the process has executed in kernel mode, in seconds. The time that each of the threads of the process has executed in kernel mode is determined, then all these times are summed to obtain this value. If Flags1 does not specify MINIDUMP_MISC1_PROCESS_TIMES, this member is unused.
        /// </summary>
        public ULONG32 ProcessKernelTime;
        /// <summary>
        /// The maximum specified clock frequency of the system processor, in MHz. If Flags1 does not specify MINIDUMP_MISC1_PROCESSOR_POWER_INFO, this member is unused.
        /// </summary>
        public ULONG32 ProcessorMaxMhz;
        /// <summary>
        /// The processor clock frequency, in MHz. This number is the maximum specified processor clock frequency multiplied by the current processor throttle. If Flags1 does not specify MINIDUMP_MISC1_PROCESSOR_POWER_INFO, this member is unused.
        /// </summary>
        public ULONG32 ProcessorCurrentMhz;
        /// <summary>
        /// The limit on the processor clock frequency, in MHz. This number is the maximum specified processor clock frequency multiplied by the current processor thermal throttle limit. If Flags1 does not specify MINIDUMP_MISC1_PROCESSOR_POWER_INFO, this member is unused.
        /// </summary>
        public ULONG32 ProcessorMhzLimit;
        /// <summary>
        /// The maximum idle state of the processor. If Flags1 does not specify MINIDUMP_MISC1_PROCESSOR_POWER_INFO, this member is unused.
        /// </summary>
        public ULONG32 ProcessorMaxIdleState;
        /// <summary>
        /// The current idle state of the processor. If Flags1 does not specify MINIDUMP_MISC1_PROCESSOR_POWER_INFO, this member is unused.
        /// </summary>
        public ULONG32 ProcessorCurrentIdleState;

        public SizeSpan SizeOfInfoMarshaled => SizeSpan.FromBytes(SizeOfInfo);
        public DateTimeOffset ProcessCreateTimeMarshaled => DateTimeOffset.FromUnixTimeSeconds(ProcessCreateTime);
    }

    [Flags]
    public enum MINIDUMP_MISC_TYPE : ULONG32
    {
        MINIDUMP_MISC1_PROCESS_ID = 0x00000001,
        MINIDUMP_MISC1_PROCESS_TIMES = 0x00000002,
        MINIDUMP_MISC1_PROCESSOR_POWER_INFO = 0x00000004,
        MINIDUMP_MISC3_PROCESS_INTEGRITY = 0x00000010,
        MINIDUMP_MISC3_PROCESS_EXECUTE_FLAGS = 0x00000020,
        MINIDUMP_MISC3_TIMEZONE = 0x00000040,
        MINIDUMP_MISC3_PROTECTED_PROCESS = 0x00000080,
        MINIDUMP_MISC4_BUILDSTRING = 0x00000100,
        MINIDUMP_MISC5_PROCESS_COOKIE = 0x00000200
    }
}
