using ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Signals;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using ICouldGames.DefenseOfThrones.World.Level.Self.Signals;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Controllers.Main;
using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Main.Implementations.Abstract
{
    public abstract class WorldLevelController : IWorldLevelController
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private ILevelEnemyController _levelEnemyController;
        [Inject] private ILevelTowerController _levelTowerController;

        private WorldLevelData _levelData = new();
        private bool _isLevelActive = false;

        public void Init(WorldLevelProcessedData levelProcessedData)
        {
            _levelData.ProcessedData = levelProcessedData;
            _levelEnemyController.Init(_levelData);
            _levelTowerController.Init(_levelData);

            _signalBus.Subscribe<LevelEnemyDiedSignal>(OnEnemyDied);
            _signalBus.Subscribe<LevelEnemyReachedEndOfPathSignal>(OnEnemyReachedEndOfPath);

            StartLevel();
        }

        public void StartLevel()
        {
            _isLevelActive = true;
            _levelEnemyController.StartSpawningEnemies();
            _signalBus.Fire(new WorldLevelStartedSignal(_levelData));
        }

        public void RestartLevel()
        {
            foreach (var enemy in _levelData.AliveEnemies)
            {
                Object.Destroy(enemy.gameObject);
            }

            foreach (var tower in _levelData.SpawnedTowers)
            {
                Object.Destroy(tower.gameObject);
            }

            Reset();

            Init(_levelData.ProcessedData);
        }

        public WorldLevelData GetCurrentLevelData()
        {
            return _levelData;
        }

        public bool IsLevelActive()
        {
            return _isLevelActive;
        }

        public void Reset()
        {
            _isLevelActive = false;
            _levelData.ScorePoints = 0;
            _levelData.AliveEnemies.Clear();
            _levelData.SpawnedTowers.Clear();
            _levelEnemyController.Reset();
            _levelTowerController.Reset();
            _signalBus?.TryUnsubscribe<LevelEnemyDiedSignal>(OnEnemyDied);
            _signalBus?.TryUnsubscribe<LevelEnemyReachedEndOfPathSignal>(OnEnemyReachedEndOfPath);
        }

        private void OnEnemyDied(LevelEnemyDiedSignal signal)
        {
            AddScorePoints(1);
            _levelData.AliveEnemies.Remove(signal.Enemy);
        }

        private void OnEnemyReachedEndOfPath(LevelEnemyReachedEndOfPathSignal signal)
        {
            _levelData.AliveEnemies.Remove(signal.Enemy);
            RestartLevel();
        }

        private void AddScorePoints(int pointsAmount)
        {
            _levelData.ScorePoints += pointsAmount;
            _signalBus.Fire(new WorldLevelScoreUpdatedSignal(_levelData.ScorePoints));
        }
    }
}