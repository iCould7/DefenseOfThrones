using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.GameWorld.Paths.NeighbourUtils
{
    public class FourMainNeighboursIterator : IEnumerable<Vector2Int>, IDisposable
    {
        private static readonly Stack<FourMainNeighboursIterator> ObjectPool = new();

        private Vector2Int _mainPosition;

        private FourMainNeighboursIterator(Vector2Int mainPosition)
        {
            _mainPosition = mainPosition;
        }

        public static FourMainNeighboursIterator GetIterator(Vector2Int mainPosition)
        {
            if (ObjectPool.Count > 0)
            {
                var iterator = ObjectPool.Pop();
                iterator._mainPosition = mainPosition;
                return iterator;
            }
            else
            {
                return new FourMainNeighboursIterator(mainPosition);
            }
        }

        public void Dispose()
        {
             ObjectPool.Push(this);
        }

        public IEnumerator<Vector2Int> GetEnumerator()
        {
            foreach (var neighbourOffset in PathNeighboursConstants.FourMainNeighbourOffsets)
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