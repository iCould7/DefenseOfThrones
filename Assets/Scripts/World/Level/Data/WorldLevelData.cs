using System;
using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.World.Level.Types;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Data
{
    [Serializable]
    public class WorldLevelData
    {
        public WorldLevelType _Type;
        public int _Subtype;
        public List<Transform> _OrderedWaypoints = new();
        public List<Transform> _TowerSlots = new();
    }
}