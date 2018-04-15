using BinaryMapper.Core;
using System;

using ULONG32 = System.UInt32;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680389.aspx
    /// </summary>
    public class MINIDUMP_MISC_INFO_STREAM
    {
        public ULONG32 SizeOfInfo;
        public MINIDUMP_MISC_TYPE Flags1;
        public ULONG32 ProcessId;
        public ULONG32 ProcessCreateTime;
        public ULONG32 ProcessUserTime;
        public ULONG32 ProcessKernelTime;
        public ULONG32 ProcessorMaxMhz;
        public ULONG32 ProcessorCurrentMhz;
        public ULONG32 ProcessorMhzLimit;
        public ULONG32 ProcessorMaxIdleState;
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
