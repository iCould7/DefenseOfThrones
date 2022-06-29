using System.Collections;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main.Implementations.Abstract;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Info;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.WorldObjects;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main.Implementations
{
    public class EndlessLevelEnemyController : LevelEnemyController
    {
        private EndlessLevelEnemiesInfo _enemiesInfo;
        private float _nextSpawnWaitTime = 0f;
        private WaitForSeconds _spawnIncreasePeriodWait;
        private bool _canSpawnNextEnemy = false;
        private LevelEnemyComponent _levelEnemyPrefab;
        private Coroutine _spawnEnemiesCoroutine;
        private Coroutine _spawnRateCoroutine;
        private Coroutine _waitNextSpawnCoroutine;

        public override void Init(WorldLevelData worldLevelData)
        {
            base.Init(worldLevelData);
            _enemiesInfo = LevelEnemiesInfoProvider.GetLevelEnemiesInfo<EndlessLevelEnemiesInfo>(WorldLevelData.ProcessedData._Id);
            _nextSpawnWaitTime = 1 / _enemiesInfo.BaseSpawnRate;
            _spawnIncreasePeriodWait = new WaitForSeconds(_enemiesInfo.SpawnIncreasePeriod);
            _levelEnemyPrefab = Resources.Load<LevelEnemyComponent>(LevelEnemyComponent.RESOURCES_PATH);
        }

        public override void StartSpawningEnemies()
        {
            _canSpawnNextEnemy = true;
            _spawnEnemiesCoroutine = EverlastingMono.StartCoroutine(StartSpawningEnemiesCoroutine());
            _spawnRateCoroutine = EverlastingMono.StartCoroutine(StartSpawnRateIncreaseCoroutine());
        }

        public override void SpawnEnemy()
        {
            //TODO: Use object factory with zenject pooling
            var levelEnemy = DiContainer.InstantiatePrefabForComponent<LevelEnemyComponent>(_levelEnemyPrefab);
            levelEnemy.Init(_enemiesInfo.MoveSpeed, _enemiesInfo.Hp, WorldLevelData.ProcessedData._OrderedWaypoints);
            WorldLevelData.AliveEnemies.Add(levelEnemy);
            levelEnemy.StartMove();

            _canSpawnNextEnemy = false;
            _waitNextSpawnCoroutine = EverlastingMono.StartCoroutine(WaitNextSpawnCoroutine());
        }

        private IEnumerator WaitNextSpawnCoroutine()
        {
            yield return new WaitForSeconds(_nextSpawnWaitTime);
            _canSpawnNextEnemy = true;
        }

        private IEnumerator StartSpawnRateIncreaseCoroutine()
        {
            while(!IsEnemySpawnsDepleted())
            {
                yield return _spawnIncreasePeriodWait;
                _nextSpawnWaitTime /= _enemiesInfo.SpawnIncreaseCoefficient;
            }
        }

        public override bool IsEnemySpawnsDepleted()
        {
            return false;
        }

        public override bool CanSpawnNextEnemy()
        {
            return _canSpawnNextEnemy;
        }

        public override void Reset()
        {
            EverlastingMono.StopCoroutine(_spawnEnemiesCoroutine);
            EverlastingMono.StopCoroutine(_spawnRateCoroutine);
            EverlastingMono.StopCoroutine(_waitNextSpawnCoroutine);
            _nextSpawnWaitTime = 0f;
            _canSpawnNextEnemy = false;
            _enemiesInfo = default;
            _spawnIncreasePeriodWait = default;
        }
    }
}