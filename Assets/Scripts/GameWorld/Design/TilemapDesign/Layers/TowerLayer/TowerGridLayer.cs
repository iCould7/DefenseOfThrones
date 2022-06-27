#if UNITY_EDITOR

using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.GameWorld.Design.TilemapDesign.Layers.PathLayer;
using ICouldGames.DefenseOfThrones.GameWorld.Paths.NeighbourUtils;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.GameWorld.Design.TilemapDesign.Layers.TowerLayer
{
    public class TowerGridLayer : MonoBehaviour
    {
        [SerializeField] private PathGridLayer PathGridLayer;
        [SerializeField] private GameWorldDesignRoot DesignRoot;

        private HashSet<Vector2Int> _towerSlotPositions = new();

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
                foreach (var neighbourPos in PathNeighbours.GetFourMainNeighbours(segment.Rect.position))
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

#endif