using ICouldGames.DefenseOfThrones.Utils.Reset;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Enums;

namespace ICouldGames.DefenseOfThrones.World.Level.Tower.Controllers.Main
{
    public interface ILevelTowerController : IResettable
    {
        void Init(WorldLevelData worldLevelData);
        void SpawnTower(LevelTowerType towerType);
    }
}