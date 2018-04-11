using ULONG32 = System.UInt32;
using ULONG64 = System.UInt64;
using BinaryMapper.Core.Attributes;

namespace BinaryMapper.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms680367.aspx
    /// </summary>
    public class MINIDUMP_EXCEPTION
    {
        public EXCEPTION_CODE_TYPE ExceptionCode;
        public ULONG32 ExceptionFlags;
        public ULONG64 ExceptionRecord;
        public ULONG64 ExceptionAddress;
        public ULONG32 NumberParameters;
        public ULONG32 __unusedAlignment;
        [ArraySize(nameof(NumberParameters))]
        public ULONG64[] ExceptionInformation;
    }

    public enum EXCEPTION_CODE_TYPE : ULONG32
    {
        EXCEPTION_ACCESS_VIOLATION = 0xc0000005,
        EXCEPTION_ARRAY_BOUNDS_EXCEEDED = 0xc000008c,
        EXCEPTION_BREAKPOINT = 0x80000003,
        EXCEPTION_DATATYPE_MISALIGNMENT = 0x80000002,
        EXCEPTION_FLT_DENORMAL_OPERAND = 0xc000008d,
        EXCEPTION_FLT_DIVIDE_BY_ZERO = 0xc000008e,
        EXCEPTION_FLT_INEXACT_RESULT = 0xc000008f,
        EXCEPTION_FLT_INVALID_OPERATION = 0xc0000090,
        EXCEPTION_FLT_OVERFLOW = 0xc0000091,
        EXCEPTION_FLT_STACK_CHECK = 0xc0000092,
        EXCEPTION_FLT_UNDERFLOW = 0xc0000093,
        EXCEPTION_GUARD_PAGE = 0x80000001,
        EXCEPTION_ILLEGAL_INSTRUCTION = 0xc000001d,
        EXCEPTION_IN_PAGE_ERROR = 0xc0000006,
        EXCEPTION_INT_DIVIDE_BY_ZERO = 0xc0000094,
        EXCEPTION_INT_OVERFLOW = 0xc0000095,
        EXCEPTION_INVALID_DISPOSITION = 0xc0000026,
        EXCEPTION_INVALID_HANDLE = 0xc0000008,
        EXCEPTION_NONCONTINUABLE_EXCEPTION = 0xc0000025,
        EXCEPTION_PRIV_INSTRUCTION = 0xc0000096,
        EXCEPTION_SINGLE_STEP = 0x80000004,
        EXCEPTION_STACK_OVERFLOW = 0xc00000fd
    }
}
