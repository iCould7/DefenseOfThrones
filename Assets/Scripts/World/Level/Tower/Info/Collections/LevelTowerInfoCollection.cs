using System;
using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Enums;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Tower.Info.Collections
{
    [CreateAssetMenu(fileName = "LevelTowerInfoCollection", menuName = "Collections/LevelTowers/LevelTowerInfoCollection", order = 1)]
    public class LevelTowerInfoCollection : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<LevelTowerInfo> _TowerInfos = new();

        [NonSerialized] public Dictionary<LevelTowerType, LevelTowerInfo> TowerInfosByType = new();

        public const string RESOURCES_PATH = "LevelTowerInfos/LevelTowerInfoCollection";

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            TowerInfosByType.Clear();
            foreach (var towerInfo in _TowerInfos)
            {
                TowerInfosByType[towerInfo.LevelTowerType] = towerInfo;
            }
        }
    }
}