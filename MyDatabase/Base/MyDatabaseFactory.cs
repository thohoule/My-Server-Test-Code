using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;

namespace MyDatabase.obs
{
    public abstract partial class MyDatabaseBase
    {
        public class MyDatabaseFactory<T> where T : MyDatabaseAttribute
        {
            private MyDatabaseBase database;
            private Dictionary<string, IDatabaseVersion> versions;

            public MyDatabaseFactory(MyDatabaseBase database)
            {
                //if (!typeof(MyDatabaseBase).IsAssignableFrom(rootOfNest))
                //    throw new NotSupportedException();

                //versions = new Dictionary<string, T>();

                //foreach (Type nestedType in rootOfNest.GetNestedTypes())
                //{
                //    //var interfaces = nestedType.GetInterfaces();

                //    var genericType = nestedType as T;

                //    if ( /*genericType != null)*/ typeof(T).IsAssignableFrom(nestedType)) //nestedType.IsAssignableFrom(typeof(T)))  //interfaces.Contains(typeof(T)) && interfaces.Contains(typeof(IDatabaseVersion)))
                //    {
                //        versions.Add("", genericType);
                //    }
                //}
                this.database = database;
                buildVersions(database.GetType());
            }

            public IDatabaseVersion GetVersion(string version)
            {
                return versions[version];
            }

            private void buildVersions(Type rootOfNest)
            {
                if (!typeof(MyDatabaseBase).IsAssignableFrom(rootOfNest))
                    throw new NotSupportedException();

                versions = new Dictionary<string, IDatabaseVersion>();

                foreach (Type nestedType in rootOfNest.GetNestedTypes())
                {
                    MemberInfo info = nestedType;
                    var attributes = info.GetCustomAttributes(typeof(T), true);
                    //var version = (IDatabaseVersion)nestedType.MakeGenericType(nestedType);

                    if (attributes.Length > 0)
                    {
                        var version = (IDatabaseVersion)Activator.CreateInstance(nestedType, database);
                        //var version = (IDatabaseVersion)nestedType.MakeGenericType(nestedType);
                        versions.Add(version.VersionNumber, version);
                    }
                }
            }
        }
    }
}
