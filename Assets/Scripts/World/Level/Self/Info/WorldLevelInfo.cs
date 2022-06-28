using System;
using ICouldGames.DefenseOfThrones.World.Level.Self.Id;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Info
{
    [Serializable]
    public class WorldLevelInfo
    {
        [SerializeField] private WorldLevelId _Id;

        public WorldLevelId Id => _Id;
    }
}