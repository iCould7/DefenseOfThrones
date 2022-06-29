using ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Signals;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using ICouldGames.DefenseOfThrones.World.Level.Self.Signals;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Controllers.Main;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Main.Implementations.Abstract
{
    public abstract class WorldLevelController : IWorldLevelController
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private ILevelEnemyController _levelEnemyController;
        [Inject] private ILevelTowerController _levelTowerController;

        private WorldLevelData _levelData = new();

        public void Init(WorldLevelProcessedData levelProcessedData)
        {
            _levelData.ProcessedData = levelProcessedData;
            _levelEnemyController.Init(_levelData);
            _levelTowerController.Init(_levelData);

            _signalBus.Subscribe<LevelEnemyDiedSignal>(OnEnemyDied);

            StartLevel();
        }

        public void StartLevel()
        {
            _levelEnemyController.StartSpawningEnemies();
        }

        public void Reset()
        {
            _levelData.ScorePoints = 0;
            _levelData.AliveEnemies.Clear();
            _levelEnemyController.Reset();
            _levelTowerController.Reset();
            _signalBus?.TryUnsubscribe<LevelEnemyDiedSignal>(OnEnemyDied);
        }

        private void OnEnemyDied(LevelEnemyDiedSignal signal)
        {
            AddScorePoints(1);
            _levelData.AliveEnemies.Remove(signal.Enemy);
        }

        private void AddScorePoints(int pointsAmount)
        {
            _levelData.ScorePoints += pointsAmount;
            _signalBus.Fire(new WorldLevelScoreUpdatedSignal(_levelData.ScorePoints));
        }
    }
}