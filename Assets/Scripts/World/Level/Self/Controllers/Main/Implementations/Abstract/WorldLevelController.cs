using ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Main.Implementations.Abstract
{
    public abstract class WorldLevelController : IWorldLevelController
    {
        [Inject] private ILevelEnemyController _levelEnemyController;

        private WorldLevelData _levelData;

        public void Init(WorldLevelData levelData)
        {
            _levelData = levelData;
            _levelEnemyController.Init(levelData);
            StartLevel();
        }

        public void StartLevel()
        {
            _levelEnemyController.StartSpawningEnemies();
        }

        public void Deactivate()
        {

        }
    }
}