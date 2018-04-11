using System.IO;

namespace BinaryMapper.Core
{
    internal static class StreamExtensions
    {
        public static void NextBytes(this Stream stream, int number, out byte[] buffer)
        {
            buffer = new byte[number];
            stream.Read(buffer, 0, number);
        }
    }
}
