using BinaryMapper.Windows.Minidump.Structures;
using System.Collections.Generic;

namespace BinaryMapper.Windows.Minidump
{
    public class Minidump
    {
        /// <summary>
        /// Contains header information for the minidump file.
        /// </summary>
        public MINIDUMP_HEADER Header { get; set; }

        /// <summary>
        /// The stream contains thread state information. 
        /// For more information, see <see cref="MINIDUMP_THREAD_INFO_LIST_STREAM"/>.
        /// </summary>
        public MINIDUMP_THREAD_INFO_LIST_STREAM ThreadInfoListStream { get; set; }
        /// <summary>
        /// The stream contains thread information. 
        /// For more information, see <see cref="MINIDUMP_THREAD_LIST_STREAM"/>.
        /// </summary>
        public MINIDUMP_THREAD_LIST_STREAM ThreadListStream { get; set; }
        /// <summary>
        /// The stream contains general system information.
        /// For more information, see <see cref="MINIDUMP_SYSTEM_INFO_STREAM"/>.
        /// </summary>
        public MINIDUMP_SYSTEM_INFO_STREAM SystemInfoStream { get; set; }
        /// <summary>
        /// Contains a variety of information.
        /// </summary>
        public MINIDUMP_MISC_INFO_STREAM MiscInfoStream { get; set; }
        public MINIDUMP_EXCEPTION_STREAM ExceptionStream { get; set; }
        /// <summary>
        /// Contains a list of modules.
        /// </summary>
        public MINIDUMP_MODULE_LIST_STREAM ModuleListStream { get; set; }
        public MINIDUMP_MEMORY_LIST_STREAM MemoryListStream { get; set; }

        public string SystemInfoServicePack { get; set; }

        /// <summary>
        /// The list of modules in the minidump.
        /// For more information, see <see cref="MINIDUMP_MODULE"/>.
        /// </summary>
        /// <remarks>
        /// Several modules may share the same name.
        /// </remarks>
        public IList<MINIDUMP_MODULE> Modules { get; set; } = new List<MINIDUMP_MODULE>();

        /// <summary>
        /// The command line given to the executable
        /// </summary>
        public string cmdLine;

        /// <summary>
        /// The environment variables -- key-value pairs -- given to the executable
        /// </summary>
        public IDictionary<string, string> EnvironmentVariables  { get; set; } = new Dictionary<string, string>();
        public string[] LSBRelease;
        public string[] linuxMaps;
        /// <summary>
        /// Information about the process, and the kernels management of it
        /// </summary>
        public IDictionary<string, string> procStatus;
        /// <summary>
        /// The type of processor, number of CPUs/cores present, and their features
        /// </summary>
        public Linux_CPUInfo CPUInfo;
    }


    /// <summary>
    /// This is a class to hold information about CPU as might be provided
    /// by a linux system.
    /// </summary>
    public class Linux_CPUInfo
    {
        /// <summary>
        /// Information about each of the processors
        /// </summary>
        public List<IDictionary<string, string>> processorInfo =
            new List<IDictionary<string, string>>();

        /// <summary>
        /// Information about the CPU
        /// </summary>
        public IDictionary<string, string> hardwareInfo;
    }
}
