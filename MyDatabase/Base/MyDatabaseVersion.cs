using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDatabase.obs
{
    public abstract partial class MyDatabaseBase
    {
        public interface IDatabaseVersion
        {
            string VersionNumber { get; }
            string UpgradeTo { get; }

            void CreateDatabase();
        }

        public interface IDatabaseVersion2<T>
        {
            string VersionNumber { get; }
            string UpgradesTo { get; }

            void LoadData(T item);
            void Upgrade(IDatabaseType data);
        }
        //public class MyDatabaseVersion
        //{
        //}
    }
}
