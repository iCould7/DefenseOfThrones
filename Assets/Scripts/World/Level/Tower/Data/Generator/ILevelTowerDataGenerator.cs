using ICouldGames.DefenseOfThrones.World.Level.Tower.Enums;

namespace ICouldGames.DefenseOfThrones.World.Level.Tower.Data.Generator
{
    public interface ILevelTowerDataGenerator
    {
        LevelTowerData GetTowerData(LevelTowerType towerType);
    }
}