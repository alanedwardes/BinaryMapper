using System;

using ULONG32 = System.UInt32;
using RVA = System.UInt32;
using ULONG64 = System.UInt64;

namespace BinaryMapper.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms680392.aspx
    /// </summary>
    public class MINIDUMP_MODULE
    {
        public ULONG64 BaseOfImage;
        public ULONG32 SizeOfImage;
        public ULONG32 CheckSum;
        public ULONG32 TimeDateStamp;
        public RVA ModuleNameRva;
        public VS_FIXEDFILEINFO VersionInfo;
        public MINIDUMP_LOCATION_DESCRIPTOR CvRecord;
        public MINIDUMP_LOCATION_DESCRIPTOR MiscRecord;
        public ULONG64 Reserved0;
        public ULONG64 Reserved1;

        public DateTimeOffset TimeDateStampMarshaled => DateTimeOffset.FromUnixTimeSeconds(TimeDateStamp);
    }
}
