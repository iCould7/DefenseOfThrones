using System;
using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.World.Level.Id;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Info.Collections
{
    public class NormalWorldLevelInfoCollection : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<NormalWorldLevelInfo> _LevelInfos;

        public NormalWorldLevelInfo FirstLevelInfo { get; private set; }
        [NonSerialized] public Dictionary<WorldLevelId, NormalWorldLevelInfo> LevelInfosById = new();

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            if (_LevelInfos.Count > 0)
            {
                FirstLevelInfo = _LevelInfos[0];
            }

            LevelInfosById.Clear();
            foreach (var levelInfo in _LevelInfos)
            {
                LevelInfosById[levelInfo._Id] = levelInfo;
            }
        }
    }
}