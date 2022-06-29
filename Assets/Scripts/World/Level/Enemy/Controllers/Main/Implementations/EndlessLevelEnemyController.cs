using System;
using System.Collections;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main.Implementations.Abstract;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Info;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main.Implementations
{
    public class EndlessLevelEnemyController : LevelEnemyController
    {
        private EndlessLevelEnemiesInfo _enemiesInfo;
        private int _spawnedEnemyCount = 0;
        private float _nextSpawnWaitTime = 0f;
        private WaitForSeconds _spawnIncreasePeriodWait;
        private bool _canSpawnNextEnemy = false;

        public override void Init(WorldLevelData levelData)
        {
            base.Init(levelData);
            _enemiesInfo = LevelEnemiesInfoProvider.GetLevelEnemiesInfo<EndlessLevelEnemiesInfo>(LevelData._Id);
            _nextSpawnWaitTime = 1 / _enemiesInfo.BaseSpawnRate;
            _spawnIncreasePeriodWait = new WaitForSeconds(_enemiesInfo.SpawnIncreasePeriod);
        }

        public override void StartSpawningEnemies()
        {
            _canSpawnNextEnemy = true;
            EverlastingMono.StartCoroutine(StartSpawningEnemiesCoroutine());
            EverlastingMono.StartCoroutine(StartSpawnRateIncreaseCoroutine());
        }

        public override void SpawnEnemy()
        {
            _spawnedEnemyCount++;
            _canSpawnNextEnemy = false;
            EverlastingMono.StartCoroutine(WaitNextSpawnCoroutine());

            Debug.Log("Endless enemy spawned! --> " + _spawnedEnemyCount);
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
            _spawnedEnemyCount = 0;
            _nextSpawnWaitTime = 0f;
            _canSpawnNextEnemy = false;
            _enemiesInfo = default;
            _spawnIncreasePeriodWait = default;
        }
    }
}