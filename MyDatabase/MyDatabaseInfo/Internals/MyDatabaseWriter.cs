using System;
using System.Collections.Generic;
using System.IO;
using Mono.Data.Sqlite;

namespace MyDatabase
{
    public partial class DatabaseFileInfo
    {
        [Obsolete]
        public class MyDatabaseWriter : IDisposable
        {
            private bool isDisposed;
            private bool overwrite;
            private DatabaseFileInfo databaseInfo;
            private string temporaryFilePath;
            private SqliteConnection connection;

            public bool IsConnected { get; private set; }

            public MyDatabaseWriter(DatabaseFileInfo databaseInfo, bool overwrite)
            {
                if (databaseInfo == null)
                    throw new ArgumentNullException();

                this.databaseInfo = databaseInfo;

                this.overwrite = overwrite;
                if (overwrite)
                    createDatabase();
                else
                    tryLoad();
            }

            ~MyDatabaseWriter()
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

            public void CreateTable(string tableName, params DatabaseField[] fields)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format(DatabaseFormatter.CreateTableFormat, tableName, DatabaseFormatter.BuildFieldNames(fields));
                    command.ExecuteNonQuery();
                }
            }

            public void InsertFields(string tableName, params DatabaseField[] fields)
            {
                if (fields.Length == 0)
                    return;

                using (var command = connection.CreateCommand())
                {
                    string fieldsName;
                    string[] fieldArgs;
                    DatabaseFormatter.BuildInsertFields(out fieldsName, out fieldArgs, fields);
                    command.CommandText = string.Format(DatabaseFormatter.InsertIntoFormat, tableName, fieldsName, fieldArgs);

                    for(int index = 0; index < fields.Length; index++)
                    {
                        command.Parameters.AddWithValue(fieldArgs[index], fields[index].Value);
                    }

                    command.ExecuteNonQuery();
                }
            }

            public void UpdateField(string tableName, DatabaseField field, params DatabaseConditional[] conditionals)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format(DatabaseFormatter.UpdateInfoFormat, tableName,
                        string.Format(DatabaseFormatter.AssignStatement, field.Name, field.Name),
                        DatabaseFormatter.BuildConditional(conditionals));

                    command.Parameters.AddWithValue("@" + field.Name, field.Value);
                    
                    for (int index = 0; index < conditionals.Length; index++)
                    {
                        command.Parameters.AddWithValue("@" + conditionals[index].FilterName, conditionals[index].Value);
                    }

                    command.ExecuteNonQuery();
                }
            }

            private void createDatabase()
            {
                if (!Directory.Exists(databaseInfo.FilePath))
                {
                    Directory.CreateDirectory(databaseInfo.FilePath);
                }

                var temporaryName = Guid.NewGuid().ToString();
                temporaryFilePath = Path.Combine(databaseInfo.FilePath, temporaryName);

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
                    temporaryFilePath = Path.Combine(databaseInfo.FilePath, temporaryName);
                    File.Copy(databaseInfo.FilePath, temporaryFilePath);

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
}
