using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.WorldObjects;
using ICouldGames.DefenseOfThrones.World.Level.Tower.WorldObjects;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Data
{
    public class WorldLevelData
    {
        public WorldLevelProcessedData ProcessedData;
        public List<LevelEnemyComponent> AliveEnemies = new();
        public List<LevelTowerComponent> SpawnedTowers = new();
        public int ScorePoints;
    }
}