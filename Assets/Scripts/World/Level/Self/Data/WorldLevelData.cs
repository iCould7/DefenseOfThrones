using System;
using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.World.Level.Self.Id;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Data
{
    [Serializable]
    public class WorldLevelData
    {
        public WorldLevelId _Id = new();
        public List<Transform> _OrderedWaypoints = new();
        public List<Transform> _TowerSlots = new();
    }
}