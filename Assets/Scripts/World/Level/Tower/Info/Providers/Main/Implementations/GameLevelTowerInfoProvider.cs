using ICouldGames.DefenseOfThrones.World.Level.Tower.Enums;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Info.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Tower.Info.Providers.Main.Implementations
{
    public class GameLevelTowerInfoProvider : IInitializable, ILevelTowerInfoProvider
    {
        private LevelTowerInfoCollection _towerInfoCollection;

        public void Initialize()
        {
            _towerInfoCollection = Resources.Load<LevelTowerInfoCollection>(LevelTowerInfoCollection.RESOURCES_PATH);
        }

        public LevelTowerInfo GetLevelTowerInfo(LevelTowerType towerType)
        {
            return _towerInfoCollection.TowerInfosByType[towerType];
        }
    }
}