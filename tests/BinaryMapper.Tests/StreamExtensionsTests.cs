using System.IO;
using Xunit;
using BinaryMapper.Core;

namespace BinaryMapper.Tests
{
    public class StreamExtensionsTests
    {
        [Fact]
        public void TestNextBytes()
        {
            var ms = new MemoryStream(new byte[] { 1, 2 });
            Assert.Equal(0, ms.Position);

            ms.NextBytes(1, out var buffer1);
            Assert.Equal(1, buffer1[0]);
            Assert.Equal(1, ms.Position);

            ms.NextBytes(1, out var buffer2);
            Assert.Equal(2, buffer2[0]);
            Assert.Equal(2, ms.Position);
        }
    }
}
