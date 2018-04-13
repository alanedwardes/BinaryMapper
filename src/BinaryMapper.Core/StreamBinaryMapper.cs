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

        public TStruct ReadObject<TStruct>(Stream stream)
        {
            var result = ReadObject(stream, typeof(TStruct));
            return (TStruct)result;
        }

        public TStruct[] ReadArray<TStruct>(Stream stream, uint length)
        {
            var result = ReadArray(stream, length, typeof(TStruct));
            return result.Cast<TStruct>().ToArray();
        }

        public Array ReadArray(Stream stream, uint length, Type type)
        {
            Array objects = Array.CreateInstance(type, length);
            for (var i = 0; i < length; i++)
            {
                objects.SetValue(ReadObject(stream, type), i);
            }
            return objects;
        }

        public object ReadObject(Stream stream, Type type)
        {
            var fields = type.GetFields(TypeRetrievalFlags);

            object structure = Activator.CreateInstance(type);

            foreach (var field in fields)
            {
                var positionBeforeReading = stream.Position;

                if (field.FieldType.IsPrimitive)
                {
                    var primitiveSize = field.FieldType.SizeOfPrimitiveType();
                    stream.NextBytes(primitiveSize, out var array);
                    field.SetValue(structure, array.ToPrimitiveObject(field.FieldType));
                }
                else if (field.FieldType == typeof(string))
                {
                    var characterArrayAttribute = field.GetCustomAttribute<CharacterArrayAttribute>(true);
                    if (characterArrayAttribute == null)
                    {
                        throw new InvalidOperationException($"Required string attribute CharacterArrayAttribute not set on property {field.Name}");
                    }

                    var arrayNumberField = type.GetField(characterArrayAttribute.LengthPropertyName, TypeRetrievalFlags);
                    var arrayLength = Convert.ToInt32(arrayNumberField.GetValue(structure));
                    stream.NextBytes(arrayLength, out var buffer);
                    Encoding encoding;
                    switch (characterArrayAttribute.CharacterType)
                    {
                        case CharacterType.WCHAR:
                            encoding = Encoding.Unicode;
                            break;
                        default:
                            throw new NotImplementedException($"Character type {CharacterType.WCHAR} not implemented.");
                    }
                    field.SetValue(structure, encoding.GetString(buffer));
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
