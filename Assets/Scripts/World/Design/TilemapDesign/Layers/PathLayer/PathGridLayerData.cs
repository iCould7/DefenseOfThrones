using System;
using System.Collections.Generic;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Design.TilemapDesign.Layers.PathLayer
{
    [Serializable]
    public class PathGridLayerData
    {
        public PathSegment _StartingSegment;
        public List<PathSegment> _PathSegments;

        public void Reset()
        {
            _StartingSegment = null;
            _PathSegments.Clear();
        }
    }

    [Serializable]
    public class PathSegment : IEquatable<PathSegment>
    {
        public RectInt _Rect;

        public PathSegment(RectInt rect)
        {
            _Rect = rect;
        }

        public void Reset()
        {
            _Rect = default;
        }

        public bool Equals(PathSegment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _Rect.Equals(other._Rect);
        }
    }
}