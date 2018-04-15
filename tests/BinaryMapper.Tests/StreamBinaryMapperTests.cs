using BinaryMapper.Core;
using System;
using System.IO;
using Xunit;

namespace BinaryMapper.Tests
{
    public class StreamBinaryMapperTests
    {
        public enum TestEnum : uint
        {
            One = 1,
            Two = 2
        }

        public void TestReadIntegerValue()
        {
            var mapper = new StreamBinaryMapper();

            var ms = new MemoryStream(BitConverter.GetBytes(48u));

            Assert.Equal(48u, mapper.ReadValue(ms, typeof(uint)));

            ms.Position = 0;

            Assert.Equal(48u, mapper.ReadValue<uint>(ms));
        }

        public void TestReadingEnumValue()
        {
            var mapper = new StreamBinaryMapper();

            var ms = new MemoryStream(BitConverter.GetBytes((uint)TestEnum.Two));

            Assert.Equal(TestEnum.Two, mapper.ReadValue<TestEnum>(ms));
        }
    }
}
