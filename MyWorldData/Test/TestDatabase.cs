//using System;
//using System.Collections.Generic;
//using System.Data;
//using MyZip;
//using MyZip.Database;
//using MyZip.Utility;

//namespace MyWorldData.Test
//{
//    public partial class TestDatabase : MyDatabaseBase
//    {
//        private TestDatabaseFactory versionFactory;

//        public class TestDatabaseAttribute : MyDatabaseAttribute
//        {
//        }

//        protected override bool loadVersion(out string version)
//        {
//            using (var reader = new CompressionReader(this))
//            {
//                byte[] buffer;
//                if (reader.ReadInternalFile("Header", out buffer))
//                {
//                    var table = MyByteConverter.ToObject<DataTable>(buffer);
//                    version = (string)table.Rows[0][0];
//                }
//            }

//            version = null;
//            return false;
//        }

//        public TestDatabase(string folderDirectory, string fileName) : base(folderDirectory, fileName)
//        {
//            versionFactory = new TestDatabaseFactory(this);
//        }

//        public class TestDatabaseFactory : MyDatabaseFactory<TestDatabaseAttribute, ITestDatabaseVersion>
//        {
//            public TestDatabaseFactory(MyDatabaseBase database) : base(database) { }
//        }

//        public bool CreateDatabase(string version)
//        {
//            ITestDatabaseVersion testVersion;
//            if (versionFactory.TryGetVersion(version, out testVersion))
//            {
//                testVersion.CreateNewDatabase();
//                VersionNumber = version;
//                return true;
//            }

//            return false;
//        }

//        public bool TryLoadData(string version, out Dictionary<short, string> value)
//        {
//            ITestDatabaseVersion testVersion;
//            if (versionFactory.TryGetVersion(version, out testVersion))
//            {
//                testVersion.LoadData(this, out value);
//                return true;
//            }
//            else
//            {
//                value = null;
//                return false;
//            }
//        }

//        public interface ITestDatabaseVersion : IMyDatabaseVersion<Dictionary<short, string>>
//        {

//        }

//        [TestDatabase]
//        public class TestDatabaseVersion1 : ITestDatabaseVersion
//        {
//            private TestDatabase database;

//            public string VersionNumber { get { return "0.0.0.1"; } }
//            public string UpgradesTo {  get { return null; } }

//            public TestDatabaseVersion1(TestDatabase database)
//            {
//                this.database = database;
//            }

//            public void CreateNewDatabase()
//            {
//                using (var writer = new CompressionWriter(database, true))
//                {
//                    writer.WriteFileToArchive("Header", MyByteConverter.ToByteArray(createHeader()));
//                    writer.WriteFileToArchive("MHW_Sword_Blade", MyByteConverter.ToByteArray(createSwordBladeTable()));

//                    writer.Compress();
//                }
//            }

//            private DataTable createHeader()
//            {
//                var table = new DataTable("Header");

//                table.Columns.Add(new DataColumn("Version", typeof(string)));
//                var row = table.NewRow();
//                row[0] = VersionNumber;
//                table.Rows.Add(row);

//                return table;
//            }

//            private DataTable createSwordBladeTable()
//            {
//                var table = new DataTable("MHW_Sword_Blade");

//                table.Columns.AddRange(new DataColumn[]
//                {
//                    new DataColumn("Reach_Level", typeof(int)),
//                    new DataColumn("Slot_Level", typeof(int)),
//                    new DataColumn("Damage_Level", typeof(int)),
//                    new DataColumn("Influence_Level", typeof(float)),
//                    new DataColumn("Defence_Level", typeof(int)),
//                    new DataColumn("Weight_Level", typeof(int)),

//                    new DataColumn("Passives_1", typeof(int)),
//                    new DataColumn("Passives_2", typeof(int)),
//                    new DataColumn("Passives_3", typeof(int)),
//                    new DataColumn("Passives_4", typeof(int)),
//                    new DataColumn("Passives_5", typeof(int)),

//                    new DataColumn("Material_1", typeof(int)),
//                    new DataColumn("Material_1_Amount", typeof(int)),
//                    new DataColumn("Material_2", typeof(int)),
//                    new DataColumn("Material_2_Amount", typeof(int)),
//                    new DataColumn("Material_3", typeof(int)),
//                    new DataColumn("Material_3_Amount", typeof(int)),
//                    new DataColumn("Material_4", typeof(int)),
//                    new DataColumn("Material_4_Amount", typeof(int)),
//                    new DataColumn("Material_5", typeof(int)),
//                    new DataColumn("Material_5_Amount", typeof(int)),
//                    new DataColumn("Material_6", typeof(int)),
//                    new DataColumn("Material_6_Amount", typeof(int)),
//                    new DataColumn("Material_7", typeof(int)),
//                    new DataColumn("Material_7_Amount", typeof(int)),
//                    new DataColumn("Material_8", typeof(int)),
//                    new DataColumn("Material_8_Amount", typeof(int)),
//                    new DataColumn("Material_9", typeof(int)),
//                    new DataColumn("Material_9_Amount", typeof(int)),
//                    new DataColumn("Material_10", typeof(int)),
//                    new DataColumn("Material_10_Amount", typeof(int))
//                });

//                return table;
//            }

//            public void LoadData(MyDatabaseBase item, out object value)
//            {
//                var dictValue = new Dictionary<short, string>();
//                LoadData(item, out dictValue);
//                value = dictValue;
//            }

//            public void LoadData(MyDatabaseBase item, out Dictionary<short, string> value)
//            {
//                value = null;
//            }

//            public MyDatabaseBase Upgrade(MyDatabaseBase data)
//            {
//                throw new NotImplementedException();
//            }
//        }
//    }
//}
