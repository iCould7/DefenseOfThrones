using ICouldGames.DefenseOfThrones.Utils.Reset;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Main
{
    public interface IWorldLevelController : IResettable
    {
        void Init(WorldLevelProcessedData levelProcessedData);
        void StartLevel();
        void RestartLevel();
    }
}