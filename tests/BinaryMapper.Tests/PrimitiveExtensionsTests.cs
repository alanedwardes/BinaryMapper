using Xunit;
using BinaryMapper.Core;
using System;

namespace BinaryMapper.Tests
{
    public class PrimitiveExtensionsTests
    {
        [Fact]
        public void TestBytesToSByte()
        {
            var bytes = BitConverter.GetBytes((sbyte)45);
            Assert.Equal((sbyte)45, bytes.ToPrimitiveObject(typeof(sbyte)));
        }

        [Fact]
        public void TestBytesToByte()
        {
            Assert.Equal((byte)1, new byte[] { 1 }.ToPrimitiveObject(typeof(byte)));
        }

        [Fact]
        public void TestBytesToInt16()
        {
            var bytes = BitConverter.GetBytes((short)45);
            Assert.Equal((short)45, bytes.ToPrimitiveObject(typeof(short)));
        }

        [Fact]
        public void TestBytesToUInt16()
        {
            var bytes = BitConverter.GetBytes((ushort)45);
            Assert.Equal((ushort)45, bytes.ToPrimitiveObject(typeof(ushort)));
        }

        [Fact]
        public void TestBytesToInt32()
        {
            var bytes = BitConverter.GetBytes(45);
            Assert.Equal(45, bytes.ToPrimitiveObject(typeof(int)));
        }

        [Fact]
        public void TestBytesToUInt32()
        {
            var bytes = BitConverter.GetBytes((uint)45);
            Assert.Equal((uint)45, bytes.ToPrimitiveObject(typeof(uint)));
        }

        [Fact]
        public void TestBytesToInt64()
        {
            var bytes = BitConverter.GetBytes((long)45);
            Assert.Equal((long)45, bytes.ToPrimitiveObject(typeof(long)));
        }

        [Fact]
        public void TestBytesToUInt64()
        {
            var bytes = BitConverter.GetBytes((ulong)45);
            Assert.Equal((ulong)45, bytes.ToPrimitiveObject(typeof(ulong)));
        }

        [Fact]
        public void TestBytesToChar()
        {
            var bytes = BitConverter.GetBytes('z');
            Assert.Equal('z', bytes.ToPrimitiveObject(typeof(char)));
        }

        [Fact]
        public void TestBytesToSingle()
        {
            var bytes = BitConverter.GetBytes(10f);
            Assert.Equal(10f, bytes.ToPrimitiveObject(typeof(float)));
        }

        [Fact]
        public void TestBytesToDouble()
        {
            var bytes = BitConverter.GetBytes((double)10);
            Assert.Equal((double)10, bytes.ToPrimitiveObject(typeof(double)));
        }

        [Fact]
        public void TestBytesToBoolean()
        {
            var bytes = BitConverter.GetBytes(true);
            Assert.True((bool)bytes.ToPrimitiveObject(typeof(bool)));
        }

        [Fact]
        public void TestBytesNotImplementedException() => Assert.Throws<NotImplementedException>(() => new byte[0].ToPrimitiveObject(typeof(TimeSpan)));

        [Fact]
        public void TestSizeOfSByte() => Assert.Equal(sizeof(sbyte), typeof(sbyte).SizeOfPrimitiveType());

        [Fact]
        public void TestSizeOfByte() => Assert.Equal(sizeof(byte), typeof(byte).SizeOfPrimitiveType());

        [Fact]
        public void TestSizeOfInt16() => Assert.Equal(sizeof(short), typeof(short).SizeOfPrimitiveType());

        [Fact]
        public void TestSizeOfUInt16() => Assert.Equal(sizeof(ushort), typeof(ushort).SizeOfPrimitiveType());

        [Fact]
        public void TestSizeOfInt32() => Assert.Equal(sizeof(int), typeof(int).SizeOfPrimitiveType());

        [Fact]
        public void TestSizeOfUInt32() => Assert.Equal(sizeof(uint), typeof(uint).SizeOfPrimitiveType());

        [Fact]
        public void TestSizeOfInt64() => Assert.Equal(sizeof(long), typeof(long).SizeOfPrimitiveType());

        [Fact]
        public void TestSizeOfUInt64() => Assert.Equal(sizeof(ulong), typeof(ulong).SizeOfPrimitiveType());

        [Fact]
        public void TestSizeOfChar() => Assert.Equal(sizeof(char), typeof(char).SizeOfPrimitiveType());

        [Fact]
        public void TestSizeOfSingle() => Assert.Equal(sizeof(float), typeof(float).SizeOfPrimitiveType());

        [Fact]
        public void TestSizeOfDouble() => Assert.Equal(sizeof(double), typeof(double).SizeOfPrimitiveType());

        [Fact]
        public void TestSizeOfBoolean() => Assert.Equal(sizeof(bool), typeof(bool).SizeOfPrimitiveType());

        [Fact]
        public void TestSizeOfNotImplemented() => Assert.Throws<NotImplementedException>(() => typeof(TimeSpan).SizeOfPrimitiveType());
    }
}
