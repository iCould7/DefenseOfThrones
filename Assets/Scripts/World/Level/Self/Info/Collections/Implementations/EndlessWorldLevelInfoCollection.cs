using System;
using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.World.Level.Self.Id;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Info.Collections.Implementations
{
    [CreateAssetMenu(fileName = "EndlessWorldLevelInfoCollection", menuName = "Collections/WorldLevel/EndlessWorldLevelInfoCollection", order = 3)]
    public class EndlessWorldLevelInfoCollection : ScriptableObject, IWorldLevelInfoCollection
    {
        [SerializeField] private List<EndlessWorldLevelInfo> _LevelInfos;

        [NonSerialized] public Dictionary<WorldLevelId, EndlessWorldLevelInfo> LevelInfosById = new();
        private EndlessWorldLevelInfo _defaultEndlessLevel;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            if (_LevelInfos.Count > 0)
            {
                _defaultEndlessLevel = _LevelInfos[0];
            }

            LevelInfosById.Clear();
            foreach (var levelInfo in _LevelInfos)
            {
                LevelInfosById[levelInfo.Id] = levelInfo;
            }
        }

        public WorldLevelInfo GetDefaultWorldLevelInfo()
        {
            return _defaultEndlessLevel;
        }
    }
}