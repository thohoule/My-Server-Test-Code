using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDatabase.Core
{
    public interface IDatabaseType
    {
        string VersionNumber { get; }
    }
}
