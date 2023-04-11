using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDatabase
{
    //public abstract partial class MyDatabaseBase2
    //{
    //    protected abstract Dictionary<string, MyDatabaseVersion> versionLookup { get; }
    //    protected abstract Dictionary<string, string> upgradeLookup { get; }
    //    protected DatabaseFileInfo databaseInfo;

    //    public MyDatabaseBase(string fileDirectory, string filePath) : this(new DatabaseFileInfo(fileDirectory, filePath))
    //    {
    //    }

    //    public MyDatabaseBase(DatabaseFileInfo databaseInfo)
    //    {
    //        this.databaseInfo = databaseInfo;
    //    }

    //    public abstract class MyDatabaseFactory2<T>
    //    {

    //    }

    //    [AttributeUsage(AttributeTargets.Class)]
    //    public abstract class MyDatabaseAttribute2 : Attribute
    //    {
    //        protected string version;

    //        public string Version { get { return version; } }

    //        public MyDatabaseAttribute(string version)
    //        {
    //            this.version = version;
    //        }
    //    }

    //    protected abstract class MyDatabaseVersion2
    //    {
    //        private MyDatabaseBase parent;

    //        public abstract string VersionNumber { get; }

    //        public MyDatabaseVersion(MyDatabaseBase parent)
    //        {
    //            this.parent = parent;
    //        }

    //        public virtual MyDatabaseVersion UpgradeTo(MyDatabaseVersion outDatedDatabase) { return outDatedDatabase; }
    //    }
    //}
}
