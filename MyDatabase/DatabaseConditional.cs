using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDatabase
{
    public struct DatabaseConditional
    {
        public string FilterName { get; set; }
        public object Value { get; set; }

        public DatabaseConditional(string filterName, object value)
        {
            FilterName = filterName;
            Value = value;
        }
    }
}
