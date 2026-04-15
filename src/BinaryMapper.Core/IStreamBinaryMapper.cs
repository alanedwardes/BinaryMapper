using System;
using System.Collections.Generic;
using System.IO;

namespace BinaryMapper.Core
{
    public interface IStreamBinaryMapper
    {
        Array ReadArray(Stream stream, uint length, Type type);
        TObject[] ReadArray<TObject>(Stream stream, uint length);
        object ReadObject(Stream stream, Type type);
        TObject ReadObject<TObject>(Stream stream);
        object ReadValue(Stream stream, Type type);
        TValue ReadValue<TValue>(Stream stream);

        void WriteArray(Stream stream, uint length, Type type, Array objects);
        void WriteValue(Stream stream, Type type, object value);
        List<(Type, long)> WriteObject(Stream stream, Type type, object structure);
    }
}