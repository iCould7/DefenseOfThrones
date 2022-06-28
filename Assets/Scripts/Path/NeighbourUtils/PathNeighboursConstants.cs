using System.Collections.Generic;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.Path.NeighbourUtils
{
    public static class PathNeighboursConstants
    {
        public static readonly List<Vector2Int> FourMainNeighbourOffsets = new()
        {
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right,
            Vector2Int.up,
        };
    }
}