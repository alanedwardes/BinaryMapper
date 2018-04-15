using Xunit;
using BinaryMapper.Core;

namespace BinaryMapper.Tests
{
    public class BitExtensionsTests
    {
        [Fact]
        public void TestGetWordFromDword()
        {
            Assert.Equal(ushort.MaxValue, 0x0000FFFFu.LowWord());
            Assert.Equal(ushort.MinValue, 0x0000FFFFu.HighWord());
            Assert.Equal(ushort.MaxValue, 0xFFFF0000u.HighWord());
            Assert.Equal(ushort.MinValue, 0xFFFF0000u.LowWord());
        }
    }
}
