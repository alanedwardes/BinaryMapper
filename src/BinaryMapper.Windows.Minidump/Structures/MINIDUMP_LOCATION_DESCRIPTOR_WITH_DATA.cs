using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryMapper.Windows.Minidump.Structures
{
    public class MINIDUMP_LOCATION_DESCRIPTOR_WITH_DATA : MINIDUMP_LOCATION_DESCRIPTOR
    {
        /// <summary>
        /// The data stored in this location.
        /// </summary>
        [NonSerialized]
        public byte[] MemoryData;
    }
}
