using BinaryMapper.Core;
using BinaryMapper.Core.Attributes;
using ULONG = System.UInt32;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// Contains a list of threads.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_thread_info_list
    /// https://msdn.microsoft.com/en-us/library/ms680506.aspx
    /// </remarks>
    public class MINIDUMP_THREAD_INFO_LIST_STREAM
    {
        /// <summary>
        /// The size of the header data for the stream, in bytes. This is generally sizeof(MINIDUMP_THREAD_INFO_LIST).
        /// </summary>
        public ULONG SizeOfHeader;
        /// <summary>
        /// The size of each entry following the header, in bytes. This is generally sizeof(MINIDUMP_THREAD_INFO).
        /// </summary>
        public ULONG SizeOfEntry;
        /// <summary>
        /// The number of entries in the stream. These are generally MINIDUMP_THREAD_INFO structures. The entries follow the header.
        /// </summary>
        public ULONG NumberOfEntries;

        /// <summary>
        /// The array of thread information
        /// </summary>
        [ArraySize(nameof(NumberOfEntries))]
        public MINIDUMP_THREAD_INFO[] ThreadInfo;

        public SizeSpan SizeOfEntryMarshaled => SizeSpan.FromBytes(SizeOfEntry);
    }
}
