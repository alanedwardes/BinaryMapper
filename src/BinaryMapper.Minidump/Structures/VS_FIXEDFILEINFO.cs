using BinaryMapper.Core;
using System;
using DWORD = System.UInt32;

namespace BinaryMapper.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms646997.aspx
    /// </summary>
    public class VS_FIXEDFILEINFO
    {
        public DWORD dwSignature;
        public DWORD dwStrucVersion;
        public DWORD dwFileVersionMS;
        public DWORD dwFileVersionLS;
        public DWORD dwProductVersionMS;
        public DWORD dwProductVersionLS;
        public dwFileFlags dwFileFlagsMask;
        public dwFileFlags dwFileFlags;
        public dwFileOS dwFileOS;
        public dwFileType dwFileType;
        public DWORD dwFileSubtype;
        public DWORD dwFileDateMS;
        public DWORD dwFileDateLS;

        public bool IsSignatureValid => dwSignature == 0xFEEF04BD;
        public Version StructureVersion => new Version(dwStrucVersion.HighWord(), dwStrucVersion.LowWord());
        public Version FileVersion => new Version(dwFileVersionMS.HighWord(), dwFileVersionMS.LowWord(), dwFileVersionLS.HighWord(), dwFileVersionLS.LowWord());
        public Version ProductVersion => new Version(dwProductVersionMS.HighWord(), dwProductVersionMS.LowWord(), dwProductVersionLS.HighWord(), dwProductVersionLS.LowWord());
    }

    [Flags]
    public enum dwFileFlags : DWORD
    {
        VS_FF_DEBUG = 0x00000001,
        VS_FF_INFOINFERRED = 0x00000010,
        VS_FF_PATCHED = 0x00000004,
        VS_FF_PRERELEASE = 0x00000002,
        VS_FF_PRIVATEBUILD = 0x00000008,
        VS_FF_SPECIALBUILD = 0x00000020
    }

    [Flags]
    public enum dwFileOS : DWORD
    {
        VOS_DOS = 0x00010000,
        VOS_NT = 0x00040000,
        VOS__WINDOWS16 = 0x00000001,
        VOS__WINDOWS32 = 0x00000004,
        VOS_OS216 = 0x00020000,
        VOS_OS232 = 0x00030000,
        VOS__PM16 = 0x00000002,
        VOS__PM32 = 0x00000003,
        VOS_UNKNOWN = 0x00000000
    }

    public enum dwFileType : DWORD
    {
        VFT_APP = 0x00000001,
        VFT_DLL = 0x00000002,
        VFT_DRV = 0x00000003,
        VFT_FONT = 0x00000004,
        VFT_STATIC_LIB = 0x00000007,
        VFT_UNKNOWN = 0x00000000,
        VFT_VXD = 0x00000005
    }
}
