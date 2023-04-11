using System.Collections.Generic;
using MyZip.Database;

namespace MyWorldData
{
    public partial class WorldDatabase : MyDatabaseBase
    {
        public interface IWorldDatabaseVersion : IMyDatabaseVersion<Dictionary<ushort, int>>
        {
        }
    }
}
