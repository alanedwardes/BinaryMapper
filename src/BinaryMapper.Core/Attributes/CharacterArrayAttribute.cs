using BinaryMapper.Core.Enums;
using System;

namespace BinaryMapper.Core.Attributes
{
    public sealed class CharacterArrayAttribute : Attribute
    {
        public CharacterArrayAttribute(CharacterType characterType, string lengthPropertyName)
        {
            CharacterType = characterType;
            LengthPropertyName = lengthPropertyName;
        }

        public CharacterType CharacterType { get; }
        public string LengthPropertyName { get; set; }
    }
}
