using BinaryMapper.Core;
using BinaryMapper.Core.Attributes;
using BinaryMapper.Core.Enums;
using System;
using RVA = System.UInt32;
using ULONG32 = System.UInt32;
using ULONG64 = System.UInt64;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// Contains header information for the minidump file.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_header
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms680378.aspx
    /// </remarks>
    public sealed class MINIDUMP_HEADER
    {
        /// <summary>
        /// The signature. 
        /// </summary>
        [CharacterArray(CharacterType.CHAR, 4)]
        public string Signature;
        /// <summary>
        /// The version of the minidump format. The low-order word is
        /// <see cref="MINIDUMP_VERSION"/>. The high-order word is an internal value that is implementation specific.
        /// </summary>
        public ULONG32 Version;
        /// <summary>
        /// The number of streams in the minidump directory.
        /// </summary>
        public ULONG32 NumberOfStreams;
        /// <summary>
        /// The base RVA of the minidump directory. The directory is an array of
        /// <see cref="MINIDUMP_DIRECTORY"/> structures.
        /// </summary>
        public RVA StreamDirectoryRva;
        /// <summary>
        /// The checksum for the minidump file. This member can be zero.
        /// </summary>
        public ULONG32 CheckSum;
        /// <summary>
        /// Time and date, in time_t format.
        /// </summary>
        public ULONG32 TimeDateStamp;
        /// <summary>
        /// One or more values from the MINIDUMP_TYPE enumeration type.
        /// </summary>
        public MINIDUMP_TYPE Flags;

        public DateTimeOffset TimeDateStampMarshaled => DateTimeOffset.FromUnixTimeSeconds(TimeDateStamp);
        public ushort VersionMarshaled => Version.LowWord();
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
