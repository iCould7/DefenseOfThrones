using System;
using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.World.Level.Self.Id;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Info.Collections.Implementations
{
    [Serializable]
    public class NormalWorldLevelInfoCollection : IWorldLevelInfoCollection
    {
        [SerializeField] private List<NormalWorldLevelInfo> _LevelInfos;

        [NonSerialized] public Dictionary<WorldLevelId, NormalWorldLevelInfo> LevelInfosById = new();
        private NormalWorldLevelInfo _firstLevelInfo;

        public void InitFields()
        {
            if (_LevelInfos.Count > 0)
            {
                _firstLevelInfo = _LevelInfos[0];
            }

            LevelInfosById.Clear();
            foreach (var levelInfo in _LevelInfos)
            {
                LevelInfosById[levelInfo.Id] = levelInfo;
            }
        }

        public WorldLevelInfo GetDefaultWorldLevelInfo()
        {
            return _firstLevelInfo;
        }
    }
}