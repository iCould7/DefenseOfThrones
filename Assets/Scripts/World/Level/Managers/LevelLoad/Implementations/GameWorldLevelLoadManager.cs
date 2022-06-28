﻿using ICouldGames.DefenseOfThrones.World.Level.Components;
using ICouldGames.DefenseOfThrones.World.Level.DirectoryPath;
using ICouldGames.DefenseOfThrones.World.Level.Info;
using ICouldGames.DefenseOfThrones.World.Level.Info.Collections.Combined;
using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Managers.LevelLoad.Implementations
{
    public class GameWorldLevelLoadManager : MonoBehaviour, IWorldLevelLoadManager
    {
        [Inject] private DiContainer _diContainer;

        private CombinedWorldLevelInfoCollections _worldLevelInfoCollections;
        private Transform _myTransform;
        private ProcessedWorldLevel _activeLevel;

        public void Awake()
        {
            _myTransform = transform;
            _worldLevelInfoCollections = Resources.Load<CombinedWorldLevelInfoCollections>(CombinedWorldLevelInfoCollections.RESOURCES_PATH);
        }

        public void Initialize()
        {
            var defaultEndlessLevelInfo = _worldLevelInfoCollections.GetDefaultWorldLevelInfo<EndlessWorldLevelInfo>();
            LoadLevel(defaultEndlessLevelInfo);
        }

        public void LoadLevel(WorldLevelInfo levelInfo)
        {
            UnloadActiveLevel();
            var levelResourcesPath = WorldLevelPathUtil.GetProcessedPrefabResourcesPath(levelInfo._Id);
            _activeLevel = _diContainer.InstantiatePrefabResourceForComponent<ProcessedWorldLevel>(levelResourcesPath, _myTransform);
            _activeLevel._MyTransform.name = _activeLevel._MyTransform.name.Replace("(Clone)", "");
        }

        public void UnloadActiveLevel()
        {
            if (_activeLevel != null)
            {
                Destroy(_activeLevel);
                _activeLevel = null;
            }
        }
    }
}