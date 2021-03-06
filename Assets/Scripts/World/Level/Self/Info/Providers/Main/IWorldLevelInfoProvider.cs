using ICouldGames.DefenseOfThrones.World.Level.Self.Id;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Info.Providers.Main
{
    public interface IWorldLevelInfoProvider
    {
        T GetWorldLevelInfo<T>(WorldLevelId levelId) where T : WorldLevelInfo;
        T GetDefaultWorldLevelInfo<T>() where T : WorldLevelInfo;
    }
}