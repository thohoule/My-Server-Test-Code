using System;

namespace MyDatabase.Core
{
    public abstract partial class MyDatabaseBase : IDatabaseType
    {
        public interface IDatabaseVersion : IDatabaseType
        {
            //string VersionNumber { get; }
            string UpgradesTo { get; }

            void LoadData(MyDatabaseBase item, out object value);
            MyDatabaseBase Upgrade(MyDatabaseBase data);
        }

        public interface IDatabaseVersion<T> : IDatabaseVersion
        {
            void LoadData(MyDatabaseBase item, out T value);
        }
    }
}
