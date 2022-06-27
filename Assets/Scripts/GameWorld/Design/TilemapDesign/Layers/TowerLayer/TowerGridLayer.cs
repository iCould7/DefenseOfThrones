#if UNITY_EDITOR

using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.GameWorld.Design.TilemapDesign.Layers.PathLayer;
using ICouldGames.DefenseOfThrones.GameWorld.Paths.NeighbourUtils;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.GameWorld.Design.TilemapDesign.Layers.TowerLayer
{
    [ExecuteAlways]
    public class TowerGridLayer : MonoBehaviour
    {
        public Transform MyTransform;
        [SerializeField] private PathGridLayer PathGridLayer;
        [SerializeField] private GameWorldDesignRoot DesignRoot;

        private HashSet<Vector2Int> _towerSlotPositions = new();

        public HashSet<Vector2Int> TowerSlotPositions => _towerSlotPositions;

        private void OnEnable()
        {
            if(PathGridLayer.IsReady)
            {
                UpdateTowerSlotPositions();
            }

            PathGridLayer.OnUpdatePathInfo += UpdateTowerSlotPositions;
        }

        private void OnDestroy()
        {
            if (PathGridLayer != null)
            {
                PathGridLayer.OnUpdatePathInfo -= UpdateTowerSlotPositions;
            }
        }

        private void UpdateTowerSlotPositions()
        {
            _towerSlotPositions.Clear();
            foreach (var segment in PathGridLayer.OrderedReachableSegments)
            {
                using (var neighbourIterator = FourMainNeighboursIterator.GetIterator(segment.Rect.position))
                {
                    foreach (var neighbourPos in neighbourIterator)
                    {
                        if (DesignRoot.IsPositionInPlayArea(neighbourPos)
                            && !PathGridLayer.ReachableSegmentsByPos.ContainsKey(neighbourPos))
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