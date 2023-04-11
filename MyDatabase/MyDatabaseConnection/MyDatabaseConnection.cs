using System;
using System.Collections.Generic;
using System.IO;
using Mono.Data.Sqlite;

namespace MyDatabase
{
    public partial class MyDatabaseConnection : IDisposable
    {
        private bool isDisposed;
        private bool overwrite;
        private DatabaseFileInfo databaseInfo;
        private string temporaryFilePath;
        private SqliteConnection connection;

        public bool IsConnected { get; private set; }
        public CommandWriter Writer { get; private set; }
        public CommandReader Reader { get; private set; }

        public MyDatabaseConnection(DatabaseFileInfo databaseInfo, bool overwrite)
        {
            if (databaseInfo == null)
                throw new ArgumentNullException();

            this.databaseInfo = databaseInfo;

            this.overwrite = overwrite;
            if (overwrite)
                createDatabase();
            else
                tryLoad();

            Writer = new CommandWriter(this);
            Reader = new CommandReader(this);
        }

        ~MyDatabaseConnection()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (isDisposed)
                return;

            if (IsConnected)
            {
                connection.Close();
                connection.Dispose();
                switchFiles();
            }

            isDisposed = true;
        }

        private void createDatabase()
        {
            if (!Directory.Exists(databaseInfo.FolderDirectory))
            {
                Directory.CreateDirectory(databaseInfo.FolderDirectory);
            }

            var temporaryName = string.Format("{0}.db",Guid.NewGuid().ToString());
            temporaryFilePath = Path.Combine(databaseInfo.FolderDirectory, temporaryName);

            //File.Create(temporaryFilePath).Close();
            SqliteConnection.CreateFile(temporaryFilePath);
            connection = new SqliteConnection("URI=file:" + temporaryFilePath);
            connection.Open();

            IsConnected = true;
        }

        private void tryLoad()
        {
            if (File.Exists(databaseInfo.FilePath))
            {
                var temporaryName = Guid.NewGuid().ToString();
                temporaryFilePath = Path.Combine(databaseInfo.FolderDirectory, temporaryName);
                File.Copy(databaseInfo.FolderDirectory, temporaryFilePath);

                connection = new SqliteConnection("URI=file:" + temporaryFilePath);
                connection.Open();

                IsConnected = true;
            }
            else
            {
                overwrite = true;
                createDatabase();
            }
        }

        private void switchFiles()
        {
            if (File.Exists(databaseInfo.FilePath))
                File.Delete(databaseInfo.FilePath);
            File.Move(temporaryFilePath, databaseInfo.FilePath);
        }
    }
}
