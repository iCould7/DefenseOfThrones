using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.Extensions.System.Collections.Generic;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Data.Generator;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Enums;
using ICouldGames.DefenseOfThrones.World.Level.Tower.WorldObjects;
using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Tower.Controllers.Main.Implementations
{
    public class LevelTowerController : ILevelTowerController
    {
        [Inject] private ILevelTowerDataGenerator _levelTowerDataGenerator;
        [Inject] private DiContainer _diContainer;

        private WorldLevelData _worldLevelData;
        private List<Transform> _towerSlots;
        private int _spawnedTowersCount = 0;
        private LevelTowerComponent _levelTowerPrefab;

        public void Init(WorldLevelData worldLevelData)
        {
            _worldLevelData = worldLevelData;
            _towerSlots = _worldLevelData.ProcessedData._TowerSlots;
            _towerSlots.Shuffle();
            _levelTowerPrefab = Resources.Load<LevelTowerComponent>(LevelTowerComponent.RESOURCES_PATH);
        }

        public void SpawnTower(LevelTowerType levelTowerType)
        {
            if (_spawnedTowersCount == _towerSlots.Count)
            {
                return;
            }

            var towerData = _levelTowerDataGenerator.GetTowerData(levelTowerType);
            var levelTower = _diContainer.InstantiatePrefabForComponent<LevelTowerComponent>(_levelTowerPrefab);
            levelTower.Init(_towerSlots[_spawnedTowersCount].position, towerData, _worldLevelData.AliveEnemies);
            _worldLevelData.SpawnedTowers.Add(levelTower);
            _spawnedTowersCount++;
        }

        public void Reset()
        {
            _spawnedTowersCount = 0;
        }
    }
}