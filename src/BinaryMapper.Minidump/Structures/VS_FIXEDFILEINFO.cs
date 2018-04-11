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
        public DWORD dwFileFlagsMask;
        public DWORD dwFileFlags;
        public DWORD dwFileOS;
        public DWORD dwFileType;
        public DWORD dwFileSubtype;
        public DWORD dwFileDateMS;
        public DWORD dwFileDateLS;

        public bool IsSignatureValid => dwSignature == 0xFEEF04BD;
    }
}
