using System;
using System.Collections.Generic;
using System.Reflection;

namespace MyDatabase.Core
{
    public abstract partial class MyDatabaseBase : IDatabaseType
    {
        public abstract class MyDatabaseFactory<T, U> where T : MyDatabaseAttribute where U : IDatabaseVersion
        {
            private MyDatabaseBase database;
            private Dictionary<string, U> versions;

            public MyDatabaseFactory(MyDatabaseBase database)
            {
                this.database = database;
                buildVersions(database.GetType());
            }

            public U GetVersion(string version)
            {
                return versions[version];
            }

            public bool TryGetVersion(string version, out U value)
            {
                return versions.TryGetValue(version, out value);
            }

            private void buildVersions(Type rootOfNest)
            {
                if (!typeof(MyDatabaseBase).IsAssignableFrom(rootOfNest))
                    throw new NotSupportedException();

                versions = new Dictionary<string, U>();

                foreach (Type nestedType in rootOfNest.GetNestedTypes())
                {
                    MemberInfo info = nestedType;
                    var attributes = info.GetCustomAttributes(typeof(T), true);

                    if (attributes.Length > 0)
                    {
                        var version = (U)Activator.CreateInstance(nestedType, database);
                        versions.Add(version.VersionNumber, version);
                    }
                }
            }
        }
    }
}
