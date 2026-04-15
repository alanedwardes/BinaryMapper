using BinaryMapper.Core.Attributes;
using BinaryMapper.Core.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                if (type.IsPrimitive || type.IsEnum)
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
            var unwrapped = type.IsEnum ? Enum.GetUnderlyingType(type) : type;
            var primitiveSize = unwrapped.SizeOfPrimitiveType();
            stream.NextBytes(primitiveSize, out var array);
            return array.ToPrimitiveObject(unwrapped);
        }

        public object ReadObject(Stream stream, Type type)
        {
            var fields = type.GetFields(TypeRetrievalFlags).Where(x => x.GetCustomAttribute<NonSerializedAttribute>() == null);

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

                    var str = encoding.GetString(buffer);
                    if (characterArrayAttribute.TrimNullTerminator)
                    {
                        str = str.TrimEnd('\0');
                    }

                    field.SetValue(structure, str);
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




        public void WriteArray(Stream stream, uint length, Type type, Array objects)
        {
            foreach (var o in objects)
            {
                if (type.IsPrimitive || type.IsEnum)
                    WriteValue(stream, type, o);
                else
                {
                    var rvaList = WriteObject(stream, type, o);
                    Debug.Assert(rvaList.Count == 0); // We don't support that, we would need to forward them
                }
            }
        }

        public void WriteValue(Stream stream, Type type, object value)
        {
            var unwrapped = type.IsEnum ? Enum.GetUnderlyingType(type) : type;
            var primitiveSize = unwrapped.SizeOfPrimitiveType();

            var data = unwrapped.ObjectToBytes(value);
            Debug.Assert(data.Length == primitiveSize);
            stream.Write(data, 0, data.Length);
        }

        //! Returns list of all RVA pointers
        public List<(Type, long)> WriteObject(Stream stream, Type type, object structure)
        {
            List<(Type, long)> rvaList = new List<(Type, long)>();
            var fields = type.GetFields(TypeRetrievalFlags).Where(x => x.GetCustomAttribute<NonSerializedAttribute>() == null).ToArray();

            // We need to iterate twice, first set lengths, and then write data (Otherwise we would only set the length when we get to the data element, after the length was already written)

            foreach (var field in fields)
            {
                if (field.FieldType == typeof(string))
                {
                    var characterArrayAttribute = field.GetCustomAttribute<CharacterArrayAttribute>(true);
                    if (characterArrayAttribute == null)
                    {
                        throw new InvalidOperationException($"Required string attribute CharacterArrayAttribute not set on property {field.Name}");
                    }

                    var stringValue = (string)field.GetValue(structure);

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

                    var dataWriteBytes = encoding.GetBytes(stringValue);

                    if (!string.IsNullOrWhiteSpace(characterArrayAttribute.PropertyName))
                    {
                        var arrayNumberField = type.GetField(characterArrayAttribute.PropertyName, TypeRetrievalFlags);
                        if (arrayNumberField == null)
                        {
                            throw new InvalidOperationException($"Referenced string size property {characterArrayAttribute.PropertyName} not found");
                        }

                        arrayNumberField.SetValue(structure, Convert.ChangeType(dataWriteBytes.Length, arrayNumberField.FieldType));
                    }
                }
                else if (field.FieldType.IsArray)
                {
                    var sizeAttribute = field.GetCustomAttribute<ArraySizeAttribute>(true);
                    if (sizeAttribute == null)
                    {
                        throw new InvalidOperationException($"Required array attribute ArraySizeAttribute not set on property {field.Name}");
                    }

                    var arrayValue = (Array)field.GetValue(structure);

                    if (!string.IsNullOrWhiteSpace(sizeAttribute.PropertyName))
                    {
                        var arrayNumberField = type.GetField(sizeAttribute.PropertyName, TypeRetrievalFlags);
                        if (arrayNumberField == null)
                        {
                            throw new InvalidOperationException($"Referenced array size property {sizeAttribute.PropertyName} not found");
                        }

                        arrayNumberField.SetValue(structure, Convert.ChangeType(arrayValue.Length, arrayNumberField.FieldType));
                    }
                }
            }
            
            foreach (var field in fields)
            {
                if (field.GetCustomAttribute<NonSerializedAttribute>() != null)
                    continue;

                var positionBeforeWriting = stream.Position;

                if (field.FieldType.IsPrimitive || field.FieldType.IsEnum)
                {
                    WriteValue(stream, field.FieldType, field.GetValue(structure));
                }
                else if (field.FieldType == typeof(string))
                {
                    var characterArrayAttribute = field.GetCustomAttribute<CharacterArrayAttribute>(true);
                    if (characterArrayAttribute == null)
                    {
                        throw new InvalidOperationException($"Required string attribute CharacterArrayAttribute not set on property {field.Name}");
                    }

                    var stringValue = (string)field.GetValue(structure);

                    Encoding encoding;
                    byte[] nullChar;
                    switch (characterArrayAttribute.CharacterType)
                    {
                        case CharacterType.WCHAR:
                            encoding = Encoding.Unicode;
                            nullChar = new byte[] {0, 0};
                            break;
                        case CharacterType.CHAR:
                            encoding = Encoding.UTF8;
                            nullChar = new byte[] {0};
                            break;
                        default:
                            throw new NotImplementedException($"Character type {characterArrayAttribute.CharacterType} not implemented.");
                    }

                    var dataWriteBytes = encoding.GetBytes(stringValue);


                    var stringLength = characterArrayAttribute.ConstantLength;
                    if (!string.IsNullOrWhiteSpace(characterArrayAttribute.PropertyName))
                    {
                        // We have set the value in first fields iteration
                        stringLength = dataWriteBytes.Length;
                    }



                    stream.Write(dataWriteBytes, 0, dataWriteBytes.Length);
                    if (dataWriteBytes.Length < stringLength) // Somehow the string we have is shorter than what we should be writing (constant length string and our input is too short
                    {
                        Debug.Assert(characterArrayAttribute.TrimNullTerminator); // This should only happen if we expect null chars
                        // Fill up rest with null chars

                        for (int i = dataWriteBytes.Length; i < stringLength; i++)
                            stream.Write(nullChar, 0, nullChar.Length);
                    }
                    else if (!string.IsNullOrWhiteSpace(characterArrayAttribute.PropertyName)) // Not for static lengths
                    {
                        stream.Write(nullChar, 0, nullChar.Length); // Strings seem to always have null terminator, even though we provide the length. And the null terminator is also not included in length. This is unexpected, but WinDbg breaks without doing this
                    }
                }
                else if (field.FieldType.IsArray)
                {
                    var sizeAttribute = field.GetCustomAttribute<ArraySizeAttribute>(true);
                    if (sizeAttribute == null)
                    {
                        throw new InvalidOperationException($"Required array attribute ArraySizeAttribute not set on property {field.Name}");
                    }

                    var arrayValue = (Array) field.GetValue(structure);


                    var arrayLength = sizeAttribute.ConstantSize;
                    if (!string.IsNullOrWhiteSpace(sizeAttribute.PropertyName))
                    {
                        // We have set the value in first fields iteration
                        arrayLength = (uint)arrayValue.Length;
                    }

                    WriteArray(stream, arrayLength, field.FieldType.GetElementType(), arrayValue);
                    //field.SetValue(structure, WriteArray(stream, arrayLength, field.FieldType.GetElementType()));
                }
                else
                {
                    rvaList.AddRange(WriteObject(stream, field.FieldType, field.GetValue(structure)));
                }

                var rvaAttrib = field.GetCustomAttribute<FixupAttribute>(true);
                if (rvaAttrib != null)
                {
                    rvaList.Add((field.FieldType, positionBeforeWriting));
                }

                // This attribute is for dealing with unions
                if (field.GetCustomAttribute<RewindAttribute>() != null)
                {
                    stream.Position = positionBeforeWriting;
                }
            }

            return rvaList;
        }

    }
}
