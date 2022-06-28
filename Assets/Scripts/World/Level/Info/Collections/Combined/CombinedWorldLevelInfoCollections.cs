using System;
using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.World.Level.Id;
using ICouldGames.DefenseOfThrones.World.Level.Info.Collections.Implementations;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Info.Collections.Combined
{
    [CreateAssetMenu(fileName = "CombinedWorldLevelInfoCollections", menuName = "Collections/CombinedWorldLevelInfoCollections", order = 1)]
    public class CombinedWorldLevelInfoCollections : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private NormalWorldLevelInfoCollection _NormalWorldLevelInfoCollection;
        [SerializeField] private EndlessWorldLevelInfoCollection _EndlessWorldLevelInfoCollection;

        private Dictionary<WorldLevelId, WorldLevelInfo> _worldLevelInfosById = new();
        private Dictionary<Type, IWorldLevelInfoCollection> _infoCollectionsByLevelInfoType = new();

        public const string RESOURCES_PATH = "WorldLevelInfos/CombinedWorldLevelInfoCollections";

        public T GetWorldLevelInfo<T>(WorldLevelId levelId) where T : WorldLevelInfo
        {
            return (T) _worldLevelInfosById[levelId];
        }

        public T GetDefaultWorldLevelInfo<T>() where T : WorldLevelInfo
        {
            return (T) _infoCollectionsByLevelInfoType[typeof(T)].GetDefaultWorldLevelInfo();
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            FillWorldLevelInfos();
            FillInfoCollectionsByLevelInfoType();
        }

        private void FillWorldLevelInfos()
        {
            _worldLevelInfosById.Clear();
            foreach (var (id, levelInfo) in _NormalWorldLevelInfoCollection.LevelInfosById)
            {
                _worldLevelInfosById[id] = levelInfo;
            }

            foreach (var (id, levelInfo) in _EndlessWorldLevelInfoCollection.LevelInfosById)
            {
                _worldLevelInfosById[id] = levelInfo;
            }
        }

        private void FillInfoCollectionsByLevelInfoType()
        {
            _infoCollectionsByLevelInfoType.Clear();
            _infoCollectionsByLevelInfoType.Add(typeof(NormalWorldLevelInfo), _NormalWorldLevelInfoCollection);
            _infoCollectionsByLevelInfoType.Add(typeof(EndlessWorldLevelInfo), _EndlessWorldLevelInfoCollection);
        }
    }
}