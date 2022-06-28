using ICouldGames.DefenseOfThrones.World.Level.Self.Id;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Info.Managers.Main
{
    public interface ILevelEnemiesInfoManager
    {
        T GetLevelEnemiesInfo<T>(WorldLevelId levelId) where T : LevelEnemiesInfo;
    }
}