using System;
using System.Collections.Generic;
using System.IO;
using Mono.Data.Sqlite;

namespace MyDatabase
{
    public partial class MyDatabaseConnection : IDisposable
    {
        public class CommandWriter
        {
            private MyDatabaseConnection databaseConnection;
            protected SqliteConnection connection { get { return databaseConnection.connection; } }

            public CommandWriter(MyDatabaseConnection databaseConnection)
            {
                this.databaseConnection = databaseConnection;
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

                    for (int index = 0; index < fields.Length; index++)
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
        }
    }
}
