using System.IO;

namespace BinaryMapper.Windows.Minidump
{
    public interface IExecutableMapper
    {
        Executable ReadExecutable(Stream stream);
    }
}