using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Data.Generator;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Enums;
using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Tower.Controllers.Main.Implementations
{
    public class LevelTowerController : ILevelTowerController
    {
        [Inject] private ILevelTowerDataGenerator _levelTowerDataGenerator;

        private WorldLevelData _worldLevelData;

        public void Init(WorldLevelData levelData)
        {
            _worldLevelData = levelData;
        }

        public void SpawnTower(LevelTowerType levelTowerType)
        {
            var towerData = _levelTowerDataGenerator.GetTowerData(levelTowerType);

            Debug.Log("SpawnedTower!");
        }

        public void Reset()
        {

        }
    }
}