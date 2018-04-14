using System;
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
    }
}