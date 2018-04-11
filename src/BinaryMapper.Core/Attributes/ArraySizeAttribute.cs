using System;

namespace BinaryMapper.Core.Attributes
{
    public sealed class ArraySizeAttribute : Attribute
    {
        public ArraySizeAttribute(uint constantSize)
        {
            ConstantSize = constantSize;
        }

        public ArraySizeAttribute(string sizePropertyName)
        {
            PropertyName = sizePropertyName;
        }

        public uint ConstantSize { get; }
        public string PropertyName { get; }
    }
}
