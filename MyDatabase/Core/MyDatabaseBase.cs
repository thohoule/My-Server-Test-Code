using System;
using System.IO;
using MyZip;

namespace MyDatabase.Core
{
    [Serializable]
    public abstract partial class MyDatabaseBase : MyZipArchive, IDatabaseType
    {
        //protected DatabaseFileInfo databaseInfo;

        public string VersionNumber { get; protected set; }

        public MyDatabaseBase(string folderDirectory, string fileName) : base (folderDirectory, fileName)
        {
        }

        //public MyDatabaseBase(DatabaseFileInfo databaseInfo)
        //{
        //    this.databaseInfo = databaseInfo;
        //}

        public bool TryLoadVersion(out string version)
        {
            if (File.Exists(FilePath))
            {
                //using (var connection = new MyDatabaseConnection(databaseInfo, false))
                //{
                //    //Read header for version
                //    var item = new DatabaseField("Version", DatabaseField.FieldType.TEXT);
                //    connection.Reader.ReadField("Header", ref item);
                //}
            }

            version = "";
            return false;
        }
    }
}
