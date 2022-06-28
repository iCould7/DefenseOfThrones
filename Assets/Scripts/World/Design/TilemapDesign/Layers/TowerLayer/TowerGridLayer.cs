#if UNITY_EDITOR

using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.Path.NeighbourUtils;
using ICouldGames.DefenseOfThrones.World.Design.TilemapDesign.Layers.PathLayer;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Design.TilemapDesign.Layers.TowerLayer
{
    [ExecuteAlways]
    public class TowerGridLayer : MonoBehaviour
    {
        [SerializeField] private Transform _MyTransform;
        [SerializeField] private PathGridLayer _PathGridLayer;
        [SerializeField] private WorldLevelDesignRoot _LevelDesignRoot;

        private HashSet<Vector2Int> _towerSlotPositions = new();

        public Transform MyTransform => _MyTransform;
        public HashSet<Vector2Int> TowerSlotPositions => _towerSlotPositions;

        private void OnEnable()
        {
            if(_PathGridLayer.IsReady && _LevelDesignRoot.IsReady)
            {
                UpdateTowerSlotPositions();
            }

            _PathGridLayer.OnUpdatePathInfo -= UpdateTowerSlotPositions;
            _PathGridLayer.OnUpdatePathInfo += UpdateTowerSlotPositions;
        }

        private void OnDestroy()
        {
            if (_PathGridLayer != null)
            {
                _PathGridLayer.OnUpdatePathInfo -= UpdateTowerSlotPositions;
            }
        }

        private void UpdateTowerSlotPositions()
        {
            _towerSlotPositions.Clear();
            foreach (var segment in _PathGridLayer.OrderedReachableSegments)
            {
                using (var neighbourIterator = FourMainNeighboursIterator.GetIterator(segment._Rect.position))
                {
                    foreach (var neighbourPos in neighbourIterator)
                    {
                        if (_LevelDesignRoot.IsPositionInPlayArea(neighbourPos)
                            && !_PathGridLayer.ReachableSegmentsByPos.ContainsKey(neighbourPos))
                        {
                            _towerSlotPositions.Add(neighbourPos);
                        }
                    }
                }
            }
        }
    }
}

#endif