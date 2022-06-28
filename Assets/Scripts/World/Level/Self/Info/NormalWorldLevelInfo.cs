using System;
using ICouldGames.DefenseOfThrones.World.Level.Self.Id;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Info
{
    [Serializable]
    public class NormalWorldLevelInfo : WorldLevelInfo
    {
        public WorldLevelId _NextLevelId;
    }
}