using System;

namespace MyDatabase.obs
{
    public abstract partial class MyDatabaseBase
    {
        [AttributeUsage(AttributeTargets.Class)]
        public abstract class MyDatabaseAttribute : Attribute
        {
            //private string version;

            //public string Version { get { return version; } }
            //public string UpgradeFrom { get; set; }

            public MyDatabaseAttribute()//string version)
            {
                //this.version = version;
            }
        }
    }
}
