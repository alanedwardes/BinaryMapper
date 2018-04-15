using System.IO;

namespace BinaryMapper.Windows.Executable
{
    public interface IExecutableMapper
    {
        Executable ReadExecutable(Stream stream);
    }
}