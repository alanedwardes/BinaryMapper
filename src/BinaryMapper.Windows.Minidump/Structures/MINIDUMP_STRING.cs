using ULONG32 = System.UInt32;
using BinaryMapper.Core.Attributes;
using BinaryMapper.Core.Enums;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680395.aspx
    /// </summary>
    public class MINIDUMP_STRING
    {
        public ULONG32 Length;
        [CharacterArray(CharacterType.WCHAR, nameof(Length))]
        public string Buffer;
    }
}
