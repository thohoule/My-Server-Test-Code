using System.Collections.Generic;
using System.Data;
using MyZip.Database;
using MyZip.Utility;

namespace MyWorldData
{
    public partial class WorldDatabase : MyDatabaseBase
    {
        private WorldDatabaseFactory versionFactory;

        public WorldDatabase(string folderDirectory, string fileName) :
            base (folderDirectory, fileName)
        {
            versionFactory = new WorldDatabaseFactory(this);
        }

        protected override bool loadVersion(out string version)
        {
            using (var reader = new CompressionReader(this))
            {
                byte[] buffer;
                if (reader.ReadInternalFile("Header", out buffer))
                {
                    var table = MyByteConverter.ToObject<DataTable>(buffer);
                    version = (string)table.Rows[0][0];
                }
            }

            version = null;
            return false;
        }

        public bool CreateDatabase(string version)
        {
            IWorldDatabaseVersion dataVersion;
            if (versionFactory.TryGetVersion(version, out dataVersion))
            {
                dataVersion.CreateNewDatabase();
                VersionNumber = version;
                return true;
            }

            return false;
        }

        public bool TryLoadData(string version, out Dictionary<ushort, int> value)
        {
            IWorldDatabaseVersion dataVersion;
            if (versionFactory.TryGetVersion(version, out dataVersion))
            {
                dataVersion.LoadData(this, out value);
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }
    }
}
