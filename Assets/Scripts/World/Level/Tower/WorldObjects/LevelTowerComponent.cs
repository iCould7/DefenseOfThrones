using ICouldGames.DefenseOfThrones.World.Level.Tower.Data;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Tower.WorldObjects
{
    public class LevelTowerComponent : MonoBehaviour
    {
        [SerializeField] private Transform _MyTransform;

        private LevelTowerData _towerData;

        public const string RESOURCES_PATH = "WorldObjects/LevelTower";

        public void Init(LevelTowerData towerData, Vector3 position)
        {
            _towerData = towerData;
            _MyTransform.position = position;
        }
    }
}