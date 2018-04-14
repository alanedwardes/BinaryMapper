using BinaryMapper.Core.Attributes;
using BinaryMapper.Core.Enums;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BinaryMapper.Core
{
    public sealed class StreamBinaryMapper : IStreamBinaryMapper
    {
        private BindingFlags TypeRetrievalFlags = BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField;

        public TObject ReadObject<TObject>(Stream stream) => (TObject)ReadObject(stream, typeof(TObject));

        public TObject[] ReadArray<TObject>(Stream stream, uint length) => ReadArray(stream, length, typeof(TObject)).Cast<TObject>().ToArray();

        public TValue ReadValue<TValue>(Stream stream) => (TValue)ReadValue(stream, typeof(TValue));

        public Array ReadArray(Stream stream, uint length, Type type)
        {
            Array objects = Array.CreateInstance(type, length);
            for (var i = 0; i < length; i++)
            {
                if (type.IsPrimitive)
                {
                    objects.SetValue(ReadValue(stream, type), i);
                }
                else
                {
                    objects.SetValue(ReadObject(stream, type), i);
                }
            }
            return objects;
        }

        public object ReadValue(Stream stream, Type type)
        {
            var unwrapped = type;
            if (unwrapped.IsEnum)
            {
                unwrapped = Enum.GetUnderlyingType(unwrapped);
            }

            var primitiveSize = unwrapped.SizeOfPrimitiveType();
            stream.NextBytes(primitiveSize, out var array);
            return array.ToPrimitiveObject(unwrapped);
        }

        public object ReadObject(Stream stream, Type type)
        {
            var fields = type.GetFields(TypeRetrievalFlags);

            object structure = Activator.CreateInstance(type);

            foreach (var field in fields)
            {
                var positionBeforeReading = stream.Position;

                if (field.FieldType.IsPrimitive || field.FieldType.IsEnum)
                {
                    field.SetValue(structure, ReadValue(stream, field.FieldType));
                }
                else if (field.FieldType == typeof(string))
                {
                    var characterArrayAttribute = field.GetCustomAttribute<CharacterArrayAttribute>(true);
                    if (characterArrayAttribute == null)
                    {
                        throw new InvalidOperationException($"Required string attribute CharacterArrayAttribute not set on property {field.Name}");
                    }

                    var stringLength = characterArrayAttribute.ConstantLength;
                    if (!string.IsNullOrWhiteSpace(characterArrayAttribute.PropertyName))
                    {
                        var arrayNumberField = type.GetField(characterArrayAttribute.PropertyName, TypeRetrievalFlags);
                        if (arrayNumberField == null)
                        {
                            throw new InvalidOperationException($"Referenced string size property {characterArrayAttribute.PropertyName} not found");
                        }
                        stringLength = Convert.ToInt32(arrayNumberField.GetValue(structure));
                    }

                    stream.NextBytes(stringLength, out var buffer);
                    Encoding encoding;
                    switch (characterArrayAttribute.CharacterType)
                    {
                        case CharacterType.WCHAR:
                            encoding = Encoding.Unicode;
                            break;
                        case CharacterType.CHAR:
                            encoding = Encoding.UTF8;
                            break;
                        default:
                            throw new NotImplementedException($"Character type {characterArrayAttribute.CharacterType} not implemented.");
                    }
                    field.SetValue(structure, encoding.GetString(buffer.Where(x => x > 0).ToArray()));
                }
                else if (field.FieldType.IsArray)
                {
                    var sizeAttribute = field.GetCustomAttribute<ArraySizeAttribute>(true);
                    if (sizeAttribute == null)
                    {
                        throw new InvalidOperationException($"Required array attribute ArraySizeAttribute not set on property {field.Name}");
                    }

                    var arrayLength = sizeAttribute.ConstantSize;
                    if (!string.IsNullOrWhiteSpace(sizeAttribute.PropertyName))
                    {
                        var arrayNumberField = type.GetField(sizeAttribute.PropertyName, TypeRetrievalFlags);
                        if (arrayNumberField == null)
                        {
                            throw new InvalidOperationException($"Referenced array size property {sizeAttribute.PropertyName} not found");
                        }
                        arrayLength = Convert.ToUInt32(arrayNumberField.GetValue(structure));
                    }

                    field.SetValue(structure, ReadArray(stream, arrayLength, field.FieldType.GetElementType()));
                }
                else
                {
                    field.SetValue(structure, ReadObject(stream, field.FieldType));
                }

                // This attribute is for dealing with unions
                if (field.GetCustomAttribute<RewindAttribute>() != null)
                {
                    stream.Position = positionBeforeReading;
                }
            }

            return structure;
        }
    }
}
