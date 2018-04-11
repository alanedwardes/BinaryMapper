using System;

using ULONG32 = System.UInt32;
using RVA = System.UInt32;
using ULONG64 = System.UInt64;

namespace BinaryMapper.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms680378.aspx
    /// </summary>
    public sealed class MINIDUMP_HEADER
    {
        public ULONG32 Signature;
        public ULONG32 Version;
        public ULONG32 NumberOfStreams;
        public RVA StreamDirectoryRva;
        public ULONG32 CheckSum;
        public ULONG32 TimeDateStamp;
        public MINIDUMP_TYPE Flags;

        public DateTimeOffset TimeDateStampMarshaled => DateTimeOffset.FromUnixTimeSeconds(TimeDateStamp);
    }

    public enum MINIDUMP_TYPE : ULONG64
    {
        MiniDumpNormal = 0x00000000,
        MiniDumpWithDataSegs = 0x00000001,
        MiniDumpWithFullMemory = 0x00000002,
        MiniDumpWithHandleData = 0x00000004,
        MiniDumpFilterMemory = 0x00000008,
        MiniDumpScanMemory = 0x00000010,
        MiniDumpWithUnloadedModules = 0x00000020,
        MiniDumpWithIndirectlyReferencedMemory = 0x00000040,
        MiniDumpFilterModulePaths = 0x00000080,
        MiniDumpWithProcessThreadData = 0x00000100,
        MiniDumpWithPrivateReadWriteMemory = 0x00000200,
        MiniDumpWithoutOptionalData = 0x00000400,
        MiniDumpWithFullMemoryInfo = 0x00000800,
        MiniDumpWithThreadInfo = 0x00001000,
        MiniDumpWithCodeSegs = 0x00002000,
        MiniDumpWithoutAuxiliaryState = 0x00004000,
        MiniDumpWithFullAuxiliaryState = 0x00008000,
        MiniDumpWithPrivateWriteCopyMemory = 0x00010000,
        MiniDumpIgnoreInaccessibleMemory = 0x00020000,
        MiniDumpWithTokenInformation = 0x00040000,
        MiniDumpWithModuleHeaders = 0x00080000,
        MiniDumpFilterTriage = 0x00100000,
        MiniDumpValidTypeFlags = 0x001fffff
    }
}
