using BinaryMapper.Windows.Executable;
using BinaryMapper.Windows.Executable.Structures;
using System;
using System.IO;
using Xunit;

namespace BinaryMapper.Tests.Windows.Executable
{
    public class ExecutableTests
    {
        [Fact]
        public void TestMappingExecutable()
        {
            var stream = File.OpenRead("Files\\firefox.exe");

            var mapper = new ExecutableMapper();

            var executable = mapper.ReadExecutable(stream);

            Assert.Equal(COFF_MACHINE_TYPE.IMAGE_FILE_MACHINE_AMD64, executable.CoffHeader.Machine);
            Assert.Equal(COFF_CHARACTERISTICS.IMAGE_FILE_EXECUTABLE_IMAGE | COFF_CHARACTERISTICS.IMAGE_FILE_LARGE_ADDRESS_AWARE, executable.CoffHeader.Characteristics);
            Assert.Equal(new DateTimeOffset(2018, 3, 23, 16, 51, 44, TimeSpan.Zero), executable.CoffHeader.TimeDateStampMarshaled);
            Assert.Equal("MZ", executable.DosHeader.e_magic);
            Assert.Equal("PE", executable.PeHeader.PeHeader);
            Assert.Null(executable.OptionalHeader);
            Assert.Equal(new Version(), executable.OptionalHeader64.ImageVersion);
            Assert.Equal(new Version(6, 1), executable.OptionalHeader64.SubsystemVersion);
            Assert.Equal(new Version(14, 11), executable.OptionalHeader64.LinkerVersion);
            Assert.Equal(new Version(6, 1), executable.OptionalHeader64.OperatingSystemVersion);
            Assert.Equal(16, executable.OptionalHeader64.DataDirectory.Length);
            Assert.Equal(6, executable.ImageSectionHeaders.Length);
            Assert.Equal(".text", executable.ImageSectionHeaders[0].Name);
            Assert.Equal(".rdata", executable.ImageSectionHeaders[1].Name);
            Assert.Equal(".data", executable.ImageSectionHeaders[2].Name);
            Assert.Equal(".pdata", executable.ImageSectionHeaders[3].Name);
            Assert.Equal(".rsrc", executable.ImageSectionHeaders[4].Name);
            Assert.Equal(".reloc", executable.ImageSectionHeaders[5].Name);
        }
    }
}
