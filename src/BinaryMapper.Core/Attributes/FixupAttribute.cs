using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryMapper.Core.Attributes
{
    /// <summary>
    /// Marks values that need to be fixed-up. Their value is only known after other data after it, is written
    /// </summary>
    public class FixupAttribute : Attribute
    {
    }
}
