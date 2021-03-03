using ULONG32 = System.UInt32;
using BinaryMapper.Core.Attributes;
using BinaryMapper.Core.Enums;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// Describes a string.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_string
    /// https://msdn.microsoft.com/en-us/library/ms680395.aspx
    /// </remarks>
    public class MINIDUMP_STRING
    {
        /// <summary>
        /// The size of the string in the Buffer member, in bytes. This size does not include the null-terminating character.
        /// </summary>
        public ULONG32 Length;
        /// <summary>
        /// The null-terminated string.
        /// </summary>
        [CharacterArray(CharacterType.WCHAR, nameof(Length))]
        public string Buffer;
    }
}
