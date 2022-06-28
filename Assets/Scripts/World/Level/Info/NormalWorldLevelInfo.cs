using System;
using ICouldGames.DefenseOfThrones.World.Level.Id;

namespace ICouldGames.DefenseOfThrones.World.Level.Info
{
    [Serializable]
    public class NormalWorldLevelInfo : WorldLevelInfo
    {
        public WorldLevelId _NextLevelId;
    }
}