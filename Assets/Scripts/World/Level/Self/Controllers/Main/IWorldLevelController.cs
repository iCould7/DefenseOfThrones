using ICouldGames.DefenseOfThrones.Utils.Deactivate;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Main
{
    public interface IWorldLevelController : IDeactivatable
    {
        void Init(WorldLevelData levelData);
        void StartLevel();
    }
}