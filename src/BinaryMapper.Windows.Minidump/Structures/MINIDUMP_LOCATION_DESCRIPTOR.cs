using BinaryMapper.Core;
using RVA = System.UInt32;
using ULONG32 = System.UInt32;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// Contains information describing the location of a data stream within a minidump file.
    /// This structure uses 32-bit locations for RVAs in the first 4GB.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_location_descriptor
    /// https://msdn.microsoft.com/en-us/library/ms680383.aspx
    /// </remarks>
    public class MINIDUMP_LOCATION_DESCRIPTOR
    {
        /// <summary>
        /// The size of the data stream, in bytes.
        /// </summary>
        public ULONG32 DataSize;
        /// <summary>
        /// The relative virtual address (RVA) of the data. This is the byte offset of the data stream from the beginning of the minidump file.
        /// </summary>
        public RVA Rva;

        public SizeSpan DataSizeMarshaled => SizeSpan.FromBytes(DataSize);
    }
}
