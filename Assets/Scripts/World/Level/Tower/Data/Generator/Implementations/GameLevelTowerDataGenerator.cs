using ICouldGames.DefenseOfThrones.World.Level.Tower.Enums;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Info.Providers.Main;
using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Tower.Data.Generator.Implementations
{
    public class GameLevelTowerDataGenerator : ILevelTowerDataGenerator
    {
        [Inject] private ILevelTowerInfoProvider _towerInfoProvider;

        public LevelTowerData GetTowerData(LevelTowerType towerType)
        {
            var towerInfo = _towerInfoProvider.GetLevelTowerInfo(towerType);
            var towerData = new LevelTowerData();
            towerData.LevelTowerType = towerInfo.LevelTowerType;
            towerData.AttackRate = towerInfo.AttackRate;
            towerData.AttackRange = towerInfo.AttackRange;
            towerData.Damage = Random.Range(towerInfo.MinDamage, towerInfo.MaxDamage);

            return towerData;
        }
    }
}