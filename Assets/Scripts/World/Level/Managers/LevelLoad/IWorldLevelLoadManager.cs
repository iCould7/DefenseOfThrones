using ICouldGames.DefenseOfThrones.World.Level.Info;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Managers.LevelLoad
{
    public interface IWorldLevelLoadManager : IInitializable
    {
        void LoadLevel(WorldLevelInfo levelInfo);
        void UnloadActiveLevel();
    }
}