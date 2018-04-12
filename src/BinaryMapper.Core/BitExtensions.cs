using DWORD = System.UInt32;
using WORD = System.UInt16;

namespace BinaryMapper.Core
{
    public static class BitExtensions
    {
        /// <summary>
        /// See LOWORD in WINDEF.H
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static WORD LowWord(this DWORD value) => (WORD)value;
        /// <summary>
        /// See HIWORD in WINDEF.H
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static WORD HighWord(this DWORD value) => (WORD)((value >> 16) & 0xFFFF);
    }
}
