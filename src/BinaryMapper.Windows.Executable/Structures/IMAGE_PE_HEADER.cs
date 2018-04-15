using BinaryMapper.Core.Attributes;
using BinaryMapper.Core.Enums;

namespace BinaryMapper.Windows.Executable.Structures
{
    public class IMAGE_PE_HEADER
    {
        [CharacterArray(CharacterType.CHAR, 4, TrimNullTerminator = true)]
        public string PeHeader;
    }
}
