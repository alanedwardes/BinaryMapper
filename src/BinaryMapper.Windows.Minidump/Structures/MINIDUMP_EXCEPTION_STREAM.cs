
using ULONG32 = System.UInt32;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680368.aspx
    /// </summary>
    public class MINIDUMP_EXCEPTION_STREAM
    {
        public ULONG32 ThreadId;
        public ULONG32 __alignment;
        public MINIDUMP_EXCEPTION ExceptionRecord;
        public MINIDUMP_LOCATION_DESCRIPTOR ThreadContext;
    }
}
