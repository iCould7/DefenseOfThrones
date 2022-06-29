using ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Controllers.Main;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Main.Implementations.Abstract
{
    public abstract class WorldLevelController : IWorldLevelController
    {
        [Inject] private ILevelEnemyController _levelEnemyController;
        [Inject] private ILevelTowerController _levelTowerController;

        private WorldLevelData _levelData;

        public void Init(WorldLevelData levelData)
        {
            _levelData = levelData;
            _levelEnemyController.Init(_levelData);
            _levelTowerController.Init(_levelData);

            StartLevel();
        }

        public void StartLevel()
        {
            _levelEnemyController.StartSpawningEnemies();
        }

        public void Reset()
        {
            _levelEnemyController.Reset();
            _levelTowerController.Reset();
        }
    }
}