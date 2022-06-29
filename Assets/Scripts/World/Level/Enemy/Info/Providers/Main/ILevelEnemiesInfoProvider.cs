using ICouldGames.DefenseOfThrones.World.Level.Self.Id;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Info.Providers.Main
{
    public interface ILevelEnemiesInfoProvider
    {
        T GetLevelEnemiesInfo<T>(WorldLevelId levelId) where T : LevelEnemiesInfo;
    }
}