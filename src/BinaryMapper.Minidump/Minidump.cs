using BinaryMapper.Structures;
using System.Collections.Generic;

namespace BinaryMapper.Minidump
{
    public class Minidump
    {
        public MINIDUMP_HEADER Header { get; set; }

        public MINIDUMP_THREAD_INFO_LIST_STREAM ThreadInfoListStream { get; set; }
        public MINIDUMP_THREAD_LIST_STREAM ThreadListStream { get; set; }
        public MINIDUMP_SYSTEM_INFO_STREAM SystemInfoStream { get; set; }
        public MINIDUMP_MISC_INFO_STREAM MiscInfoStream { get; set; }
        public MINIDUMP_EXCEPTION_STREAM ExceptionStream { get; set; }
        public MINIDUMP_MODULE_LIST_STREAM ModuleListStream { get; set; }
        public MINIDUMP_MEMORY_LIST_STREAM MemoryListStream { get; set; }

        public string SystemInfoServicePack { get; set; }
        public IDictionary<string, MINIDUMP_MODULE> Modules { get; set; } = new Dictionary<string, MINIDUMP_MODULE>();
    }
}
