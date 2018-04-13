
using ULONG32 = System.UInt32;
using BinaryMapper.Core.Attributes;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms680391.aspx
    /// </summary>
    public class MINIDUMP_MODULE_LIST_STREAM
    {
        public ULONG32 NumberOfModules;
        [ArraySize(nameof(NumberOfModules))]
        public MINIDUMP_MODULE[] Modules;
    }
}
