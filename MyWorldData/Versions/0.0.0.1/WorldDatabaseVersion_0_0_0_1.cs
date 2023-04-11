using System;
using System.Collections.Generic;
using MyZip.Database;
using MyZip.Utility;

namespace MyWorldData
{
    public partial class WorldDatabase : MyDatabaseBase
    {
        [WorldDatabase]
        public sealed partial class WorldDatabaseVersion_0_0_0_1 : IWorldDatabaseVersion
        {
            private WorldDatabase database;

            public string VersionNumber { get { return "0.0.0.1"; } }
            public string UpgradesTo { get { return null; } }

            public WorldDatabaseVersion_0_0_0_1(WorldDatabase database)
            {
                this.database = database;
            }

            public void CreateNewDatabase()
            {
                using (var writer = new CompressionWriter(database, true))
                {
                    writer.WriteFileToArchive("Header", MyByteConverter.ToByteArray(MakeDefaultHeaderTable(VersionNumber)));
                    //writer.WriteFileToArchive("MHW_Sword_Blade", MyByteConverter.ToByteArray(createSwordBladeTable()));

                    writer.Compress();
                }
            }

            public void LoadData(MyDatabaseBase item, out object value)
            {
                var dictValue = new Dictionary<ushort, int>();
                LoadData(item, out dictValue);
                value = dictValue;
            }

            public void LoadData(MyDatabaseBase item, out Dictionary<ushort, int> value)
            {
                value = null;
            }

            public MyDatabaseBase Upgrade(MyDatabaseBase data)
            {
                if (UpgradesTo == null)
                    return data;


                throw new NotImplementedException();
            }
        }
    }
}
