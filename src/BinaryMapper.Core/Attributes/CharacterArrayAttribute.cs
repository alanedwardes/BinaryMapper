using BinaryMapper.Core.Enums;
using System;

namespace BinaryMapper.Core.Attributes
{
    public sealed class CharacterArrayAttribute : Attribute
    {
        public CharacterArrayAttribute(CharacterType characterType, int constantLength)
        {
            CharacterType = characterType;
            ConstantLength = constantLength;
        }

        public int ConstantLength { get; set; }

        public CharacterArrayAttribute(CharacterType characterType, string lengthPropertyName)
        {
            CharacterType = characterType;
            PropertyName = lengthPropertyName;
        }

        public CharacterType CharacterType { get; }
        public string PropertyName { get; set; }
    }
}
