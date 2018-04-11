using System;
using System.IO;

namespace BinaryMapper.Core
{
    public interface IStreamBinaryMapper
    {
        Array ReadArray(Stream stream, uint length, Type type);
        TStruct[] ReadArray<TStruct>(Stream stream, uint length);
        object ReadObject(Stream stream, Type type);
        TStruct ReadObject<TStruct>(Stream stream);
    }
}