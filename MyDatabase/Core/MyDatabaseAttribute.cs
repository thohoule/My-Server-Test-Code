using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDatabase.Core
{
    public abstract partial class MyDatabaseBase
    {
        [AttributeUsage(AttributeTargets.Class)]
        public abstract class MyDatabaseAttribute : Attribute
        {
            public MyDatabaseAttribute()
            {
            }
        }
    }
}
