using System;
using System.IO;
using Mono.Data.Sqlite;

namespace MyDatabase
{
    public partial class DatabaseFileInfo
    {
        [Obsolete]
        public class MyDatabaseReader : IDisposable
        {
            private bool isDisposed;
            private DatabaseFileInfo databaseInfo;

            public SqliteConnection Connection { get; private set; }
            public bool IsConnected { get; private set; }

            public MyDatabaseReader(DatabaseFileInfo databaseInfo)
            {
                if (databaseInfo == null)
                    throw new ArgumentNullException();

                this.databaseInfo = databaseInfo;
                openConnection();
            }

            ~MyDatabaseReader()
            {
                Dispose();
            }

            public void Dispose()
            {
                if (isDisposed)
                    return;

                if (IsConnected)
                {
                    Connection.Close();
                    Connection.Dispose();
                }

                isDisposed = true;
            }

            private void openConnection()
            {
                if (!File.Exists(databaseInfo.FilePath))
                    throw new FileNotFoundException();

                Connection = new SqliteConnection("URI=file:" + databaseInfo.FilePath);
                Connection.Open();
            }
        }
    }
}
