using System;

namespace BinaryMapper.Core
{
    internal static class PrimitiveExtensions
    {
        public static object ToPrimitiveObject(this byte[] bytes, Type type)
        {
            if (type == typeof(SByte))
            {
                return (sbyte)bytes[0];
            }
            else if (type == typeof(Byte))
            {
                return bytes[0];
            }
            else if (type == typeof(Int16))
            {
                return BitConverter.ToInt16(bytes, 0);
            }
            else if (type == typeof(UInt16))
            {
                return BitConverter.ToUInt16(bytes, 0);
            }
            else if (type == typeof(Int32))
            {
                return BitConverter.ToInt32(bytes, 0);
            }
            else if (type == typeof(UInt32))
            {
                return BitConverter.ToUInt32(bytes, 0);
            }
            else if (type == typeof(Int64))
            {
                return BitConverter.ToInt64(bytes, 0);
            }
            else if (type == typeof(UInt64))
            {
                return BitConverter.ToUInt64(bytes, 0);
            }
            else if (type == typeof(Char))
            {
                return BitConverter.ToChar(bytes, 0);
            }
            else if (type == typeof(Single))
            {
                return BitConverter.ToSingle(bytes, 0);
            }
            else if (type == typeof(Double))
            {
                return BitConverter.ToDouble(bytes, 0);
            }
            else if (type == typeof(Boolean))
            {
                return BitConverter.ToBoolean(bytes, 0);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static int SizeOfPrimitiveType(this Type type)
        {
            if (type == typeof(SByte))
            {
                return sizeof(SByte);
            }
            else if (type == typeof(Byte))
            {
                return sizeof(Byte);
            }
            else if (type == typeof(Int16))
            {
                return sizeof(Int16);
            }
            else if (type == typeof(UInt16))
            {
                return sizeof(UInt16);
            }
            else if (type == typeof(Int32))
            {
                return sizeof(Int32);
            }
            else if (type == typeof(UInt32))
            {
                return sizeof(UInt32);
            }
            else if (type == typeof(Int64))
            {
                return sizeof(Int64);
            }
            else if (type == typeof(UInt64))
            {
                return sizeof(UInt64);
            }
            else if (type == typeof(Char))
            {
                return sizeof(Char);
            }
            else if (type == typeof(Single))
            {
                return sizeof(Single);
            }
            else if (type == typeof(Double))
            {
                return sizeof(Double);
            }
            else if (type == typeof(Decimal))
            {
                return sizeof(Decimal);
            }
            else if (type == typeof(Boolean))
            {
                return sizeof(Boolean);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
