using ICouldGames.DefenseOfThrones.World.Level.Self.Id;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Info.Managers.Main
{
    public interface IWorldLevelInfoManager
    {
        T GetWorldLevelInfo<T>(WorldLevelId levelId) where T : WorldLevelInfo;
        T GetDefaultWorldLevelInfo<T>() where T : WorldLevelInfo;
    }
}