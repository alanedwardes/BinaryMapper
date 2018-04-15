using BinaryMapper.Core.Attributes;
using WORD = System.UInt16;

namespace BinaryMapper.Windows.Executable.Structures
{
    public class ICONDIR
    {
        public WORD idReserved;
        public WORD idType;
        public WORD idCount;
        [ArraySize(nameof(idCount))]
        public ICONDIRENTRY[] idEntries;
    }
}
