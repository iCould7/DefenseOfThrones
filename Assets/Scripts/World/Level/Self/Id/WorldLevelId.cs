using System;
using ICouldGames.DefenseOfThrones.World.Level.Self.Enums;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Id
{
    [Serializable]
    public class WorldLevelId : IEquatable<WorldLevelId>
    {
        [SerializeField] private WorldLevelType _Type;
        [SerializeField] private int _Subtype;

        public WorldLevelType Type => _Type;
        public int Subtype => _Subtype;

        public void Copy(WorldLevelId objectToCopyFrom)
        {
            _Type = objectToCopyFrom._Type;
            _Subtype = objectToCopyFrom._Subtype;
        }

        public bool Equals(WorldLevelId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _Type == other._Type && _Subtype == other._Subtype;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((WorldLevelId) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) Type, Subtype);
        }
    }
}