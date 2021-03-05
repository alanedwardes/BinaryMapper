using BinaryMapper.Core;
using System;
using RVA = System.UInt32;
using ULONG32 = System.UInt32;
using ULONG64 = System.UInt64;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// Contains information for a specific module.
    /// </summary>
    /// <remarks>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms680392.aspx
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_module
    /// </remarks>
    public class MINIDUMP_MODULE
    {
        /// <summary>
        /// The base address of the module executable image in memory.
        /// </summary>
        public ULONG64 BaseOfImage;
        /// <summary>
        /// The size of the module executable image in memory, in bytes.
        /// </summary>
        public ULONG32 SizeOfImage;
        /// <summary>
        /// The checksum value of the module executable image.
        /// </summary>
        public ULONG32 CheckSum;
        /// <summary>
        /// The timestamp value of the module executable image, in time_t format.
        /// </summary>
        public ULONG32 TimeDateStamp;
        /// <summary>
        /// An RVA to a <see cref="MINIDUMP_STRING"/> structure that specifies the name of the module.
        /// See <see cref="MINIDUMP_MODULE(Name)"/> for a friendly accessor.
        /// </summary>
        public RVA ModuleNameRva;
        /// <summary>
        /// A <see cref="VS_FIXEDFILEINFO"/> structure that specifies the version of the module.
        /// </summary>
        public VS_FIXEDFILEINFO VersionInfo;
        /// <summary>
        /// A <see cref="MINIDUMP_LOCATION_DESCRIPTOR"/> structure that specifies the CodeView record of the module.
        /// </summary>
        public MINIDUMP_LOCATION_DESCRIPTOR CvRecord;
        /// <summary>
        /// A <see cref="MINIDUMP_LOCATION_DESCRIPTOR"/> structure that specifies the miscellaneous record of the module.
        /// </summary>
        public MINIDUMP_LOCATION_DESCRIPTOR MiscRecord;
        /// <summary>
        /// Reserved for future use.
        /// </summary>
        public ULONG64 Reserved0;
        /// <summary>
        /// Reserved for future use.
        /// </summary>
        public ULONG64 Reserved1;

        public SizeSpan SizeOfImageMarshaled => SizeSpan.FromBytes(SizeOfImage);
        public DateTimeOffset TimeDateStampMarshaled => DateTimeOffset.FromUnixTimeSeconds(TimeDateStamp);

        /// <summary>
        /// The name of the module
        /// </summary>
        public string Name { get; internal set;}
    }
}
