using System;
using System.Collections.Generic;
using System.IO;
using Mono.Data.Sqlite;

namespace MyDatabase
{
    public partial class MyDatabaseConnection : IDisposable
    {
        public class CommandReader
        {
            private MyDatabaseConnection databaseConnection;
            protected SqliteConnection connection { get { return databaseConnection.connection; } }

            public CommandReader(MyDatabaseConnection databaseConnection)
            {
                this.databaseConnection = databaseConnection;
            }

            public bool ReadField(string tableName, ref DatabaseField value)
            {
                using (var reader = new SqliteCommand(
                    string.Format(DatabaseFormatter.SelectFromFormat, value.Name), connection).ExecuteReader())
                {
                    if (reader.Read())
                    {
                        switch (value.Type)
                        {
                            case DatabaseField.FieldType.TEXT:
                                value.Value = reader.GetString(0);
                                return true;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                }

                return false;
            }
        }
    }
}
