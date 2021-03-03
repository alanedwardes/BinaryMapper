using ULONG32 = System.UInt32;
using BinaryMapper.Core.Attributes;

namespace BinaryMapper.Windows.Minidump.Structures
{
    /// <summary>
    /// Contains a list of modules.
    /// </summary>
    /// <remarks>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms680391.aspx
    /// https://docs.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_module_list
    /// </remarks>
    public class MINIDUMP_MODULE_LIST_STREAM
    {
        /// <summary>
        /// The number of modules
        /// </summary>
        public ULONG32 NumberOfModules;

        /// <summary>
        /// The modules in the dump file.  See <see cref="MINIDUMP_MODULE"/>
        /// </summary>
        [ArraySize(nameof(NumberOfModules))]
        public MINIDUMP_MODULE[] Modules;
    }
}
