using System;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Info
{
    [Serializable]
    public class EndlessLevelEnemiesInfo : LevelEnemiesInfo
    {
        [SerializeField] private float _BaseSpawnRate;
        [SerializeField] private float _SpawnIncreasePeriod;
        [SerializeField] private float _SpawnIncreaseCoefficient;

        public float BaseSpawnRate => _BaseSpawnRate;
        public float SpawnIncreasePeriod => _SpawnIncreasePeriod;
        public float SpawnIncreaseCoefficient => _SpawnIncreaseCoefficient;
    }
}