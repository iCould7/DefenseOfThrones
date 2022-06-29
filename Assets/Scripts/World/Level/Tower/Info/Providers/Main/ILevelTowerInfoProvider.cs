using ICouldGames.DefenseOfThrones.World.Level.Tower.Enums;

namespace ICouldGames.DefenseOfThrones.World.Level.Tower.Info.Providers.Main
{
    public interface ILevelTowerInfoProvider
    {
        LevelTowerInfo GetLevelTowerInfo(LevelTowerType towerType);
    }
}