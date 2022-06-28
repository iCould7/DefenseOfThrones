using ICouldGames.DefenseOfThrones.World.Level.Self.Info;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Managers.LevelLoad
{
    public interface IWorldLevelLoadManager : IInitializable
    {
        void LoadLevel(WorldLevelInfo levelInfo);
        void UnloadActiveLevel();
    }
}