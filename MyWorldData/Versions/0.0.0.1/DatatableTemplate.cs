using System;
using System.Collections.Generic;
using System.Data;
using MyZip.Database;

namespace MyWorldData
{
    public partial class WorldDatabase : MyDatabaseBase
    {
        public sealed partial class WorldDatabaseVersion_0_0_0_1 : IWorldDatabaseVersion
        {
            public static DataTable MakeDefaultHeaderTable()
            {
                var table = new DataTable("Header");

                table.Columns.Add(new DataColumn("Version", typeof(string)));

                return table;
            }

            public static DataTable MakeDefaultHeaderTable(string version)
            {
                var table = new DataTable("Header");

                table.Columns.Add(new DataColumn("Version", typeof(string)));
                var row = table.NewRow();
                row[0] = version;
                table.Rows.Add(row);

                return table;
            }
        }
    }
}
