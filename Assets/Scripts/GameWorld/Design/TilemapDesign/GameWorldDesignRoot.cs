using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.GameWorld.Design.TilemapDesign.Layers.PathLayer;
using ICouldGames.DefenseOfThrones.GameWorld.Design.TilemapDesign.Layers.TowerLayer;
using ICouldGames.DefenseOfThrones.GameWorld.Paths.NeighbourUtils;
using UnityEngine;

#if UNITY_EDITOR

namespace ICouldGames.DefenseOfThrones.GameWorld.Design.TilemapDesign
{
    public class GameWorldDesignRoot : MonoBehaviour
    {
        [SerializeField] private PathGridLayer PathGridLayer;
        [SerializeField] private TowerGridLayer TowerGridLayer;

        public bool IsPositionInPlayArea(Vector2Int position)
        {
            var playAreaPositions = new HashSet<Vector2Int>();
            foreach (var segment in PathGridLayer.OrderedReachableSegments)
            {
                playAreaPositions.Add(segment.Rect.position);
                foreach (var neighbour in PathNeighbours.GetFourMainNeighbours(segment.Rect.position))
                {
                    playAreaPositions.Add(neighbour);
                }
            }

            return playAreaPositions.Contains(position);
        }
    }
}

#endif