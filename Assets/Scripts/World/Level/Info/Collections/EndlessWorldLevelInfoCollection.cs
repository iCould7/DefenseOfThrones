using System;
using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.World.Level.Id;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Info.Collections
{
    public class EndlessWorldLevelInfoCollection : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<EndlessWorldLevelInfo> _LevelInfos;

        public EndlessWorldLevelInfo DefaultEndlessLevel { get; private set; }
        [NonSerialized] public Dictionary<WorldLevelId, EndlessWorldLevelInfo> LevelInfosById = new();


        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            if (_LevelInfos.Count > 0)
            {
                DefaultEndlessLevel = _LevelInfos[0];
            }

            LevelInfosById.Clear();
            foreach (var levelInfo in _LevelInfos)
            {
                LevelInfosById[levelInfo._Id] = levelInfo;
            }
        }
    }
}