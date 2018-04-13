using ULONG = System.UInt32;
using BinaryMapper.Core.Attributes;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680506.aspx
    /// </summary>
    public class MINIDUMP_THREAD_INFO_LIST_STREAM
    {
        public ULONG SizeOfHeader;
        public ULONG SizeOfEntry;
        public ULONG NumberOfEntries;
        [ArraySize(nameof(NumberOfEntries))]
        public MINIDUMP_THREAD_INFO[] ThreadInfo;
    }
}
