using System.IO;

namespace BinaryMapper.Minidump
{
    public interface IMinidumpMapper
    {
        Minidump ReadMinidump(Stream stream);
    }
}