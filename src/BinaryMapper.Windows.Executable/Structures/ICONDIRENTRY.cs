using BinaryMapper.Core;
using BYTE = System.Byte;
using DWORD = System.UInt32;
using WORD = System.UInt16;

namespace BinaryMapper.Windows.Executable.Structures
{
    public class ICONDIRENTRY
    {
        public BYTE bWidth;
        public BYTE bHeight;
        public BYTE bColorCount;
        public BYTE bReserved;
        public WORD wPlanes;
        public WORD wBitCount;
        public DWORD dwBytesInRes;
        public DWORD dwImageOffset;

        public SizeSpan dwBytesInResMarshaled => SizeSpan.FromBytes(dwBytesInRes);
    }
}
