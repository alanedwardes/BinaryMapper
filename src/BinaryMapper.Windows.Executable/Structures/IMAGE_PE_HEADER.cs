using System;
using System.Text;
using LONG = System.Int32;

namespace BinaryMapper.Windows.Executable.Structures
{
    public class IMAGE_PE_HEADER
    {
        public LONG PeHeader;

        public string PeHeaderMarshaled => Encoding.ASCII.GetString(BitConverter.GetBytes(PeHeader));
    }
}
