using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.World.Design.TilemapDesign.Layers.PathLayer;
using ICouldGames.DefenseOfThrones.World.Level.Types;
using ICouldGames.DefenseOfThrones.World.Paths.NeighbourUtils;
using UnityEngine;

#if UNITY_EDITOR

namespace ICouldGames.DefenseOfThrones.World.Design.TilemapDesign
{
    public class WorldDesignRoot : MonoBehaviour
    {
        [SerializeField] private PathGridLayer _PathGridLayer;
        [SerializeField] private WorldLevelType _LevelType = WorldLevelType.Normal;
        [SerializeField] private int _LevelSubtype = 1;

        public bool IsPositionInPlayArea(Vector2Int position)
        {
            var playAreaPositions = new HashSet<Vector2Int>();
            foreach (var segment in _PathGridLayer.OrderedReachableSegments)
            {
                playAreaPositions.Add(segment._Rect.position);
                using (var neighbourIterator = FourMainNeighboursIterator.GetIterator(segment._Rect.position))
                {
                    foreach (var neighbour in neighbourIterator)
                    {
                        playAreaPositions.Add(neighbour);
                    }
                }
            }

            return playAreaPositions.Contains(position);
        }
    }
}

#endif