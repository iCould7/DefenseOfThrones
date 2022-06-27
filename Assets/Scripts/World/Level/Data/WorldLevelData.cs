using System;
using ICouldGames.DefenseOfThrones.World.Level.Types;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Data
{
    [Serializable]
    public class WorldLevelData
    {
        [SerializeField] private WorldLevelType _Type;
        [SerializeField] private int _Subtype;

        public WorldLevelType Type => _Type;
        public int Subtype => _Subtype;
    }
}