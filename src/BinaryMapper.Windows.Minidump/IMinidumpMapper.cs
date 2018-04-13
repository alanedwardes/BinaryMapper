using System.IO;

namespace BinaryMapper.Windows.Minidump
{
    public interface IMinidumpMapper
    {
        Minidump ReadMinidump(Stream stream);
    }
}