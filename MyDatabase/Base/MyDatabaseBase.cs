using System;
using System.IO;

namespace MyDatabase.obs
{
    public abstract partial class MyDatabaseBase
    {
        protected DatabaseFileInfo databaseInfo;

        public MyDatabaseBase(string fileDirectory, string filePath) : this(new DatabaseFileInfo(fileDirectory, filePath))
        {
        }

        public MyDatabaseBase(DatabaseFileInfo databaseInfo)
        {
            this.databaseInfo = databaseInfo;
        }

        public bool TryLoadVersion(out string version)
        {
            if (File.Exists(databaseInfo.FilePath))
            {
                using (var connection = new MyDatabaseConnection(databaseInfo, false))
                {
                    //Read header for version
                    var item = new DatabaseField("Version", DatabaseField.FieldType.TEXT);
                    connection.Reader.ReadField("Header", ref item);
                }
            }

            version = "";
            return false;
        }

        public interface IDatabaseType
        {

        }
    }
}
