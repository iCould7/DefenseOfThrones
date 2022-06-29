using ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Self.Info;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Managers.LevelLoad
{
    public interface IWorldLevelLoadManager
    {
        void LoadLevel(WorldLevelInfo levelInfo);
        void UnloadCurrentLevel();
        IWorldLevelController GetLoadedWorldLevelController();
        bool IsAnyLevelLoaded();
    }
}