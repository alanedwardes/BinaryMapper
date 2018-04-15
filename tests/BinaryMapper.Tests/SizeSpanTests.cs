using BinaryMapper.Core;
using Xunit;

namespace BinaryMapper.Tests
{
    public class SizeSpanTests
    {
        [Fact]
        public void TestSizeSpan()
        {
            var span = new SizeSpan(1024 * 1024 * 1024);

            Assert.Equal(1073741824u, span.Bytes);
            Assert.Equal(1048576, span.Kibibytes);
            Assert.Equal(1024, span.Mebibytes);
            Assert.Equal(1, span.Gibibytes);
            Assert.Equal(0.0009765625, span.Tebibytes);
        }
    }
}
