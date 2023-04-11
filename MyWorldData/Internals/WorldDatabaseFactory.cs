using System;
using System.Collections.Generic;
using MyZip.Database;

namespace MyWorldData
{
    public partial class WorldDatabase : MyDatabaseBase
    {
        public class WorldDatabaseFactory : MyDatabaseFactory<WorldDatabaseAttribute, IWorldDatabaseVersion>
        {
            public WorldDatabaseFactory(WorldDatabase database) : base(database) { }
        }
    }
}
