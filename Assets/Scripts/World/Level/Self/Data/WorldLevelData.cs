using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.WorldObjects;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Data
{
    public class WorldLevelData
    {
        public WorldLevelProcessedData ProcessedData;
        public List<LevelEnemyComponent> AliveEnemies = new();
        public float ScorePoints;
    }
}