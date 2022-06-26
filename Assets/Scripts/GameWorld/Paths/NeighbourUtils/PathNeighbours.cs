using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.GameWorld.Paths.NeighbourUtils
{
    public static class PathNeighbours
    {
        public static readonly List<Vector2Int> FourMainNeighbourOffsets = new()
        {
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right,
            Vector2Int.up,
        };

        private static readonly FourMainNeighboursIterator FourMainNeighboursIterator = new ();

        public static FourMainNeighboursIterator GetFourMainNeighbours(Vector2Int mainPosition)
        {
            FourMainNeighboursIterator.SetMainPosition(mainPosition);
            return FourMainNeighboursIterator;
        }
    }

    public class FourMainNeighboursIterator : IEnumerable<Vector2Int>
    {
        private Vector2Int _mainPosition;

        public FourMainNeighboursIterator(){}

        public FourMainNeighboursIterator(Vector2Int mainPosition)
        {
            _mainPosition = mainPosition;
        }

        public void SetMainPosition(Vector2Int mainPosition)
        {
            _mainPosition = mainPosition;
        }

        public IEnumerator<Vector2Int> GetEnumerator()
        {
            foreach (var neighbourOffset in PathNeighbours.FourMainNeighbourOffsets)
            {
                yield return _mainPosition + neighbourOffset;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}