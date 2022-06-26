using System;
using System.Collections.Generic;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.GameWorld.Design.TilemapDesign.Layers.PathLayer
{
    [Serializable]
    public class PathGridLayerData
    {
        public PathSegment StartingSegment;
        public List<PathSegment> PathSegments;

        public void Reset()
        {
            StartingSegment = null;
            PathSegments.Clear();
        }
    }

    [Serializable]
    public class PathSegment : IEquatable<PathSegment>
    {
        public RectInt Rect;

        public PathSegment(RectInt rect)
        {
            Rect = rect;
        }

        public void Reset()
        {
            Rect = default;
        }

        public bool Equals(PathSegment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Rect.Equals(other.Rect);
        }
    }
}