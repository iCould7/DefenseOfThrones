using ICouldGames.DefenseOfThrones.World.Level.Components;
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
            var levelResourcesPath = WorldLevelPathUtil.GetProcessedPrefabResourcesPath(levelInfo._Id);
            var loadedLevel = _diContainer.InstantiatePrefabResourceForComponent<ProcessedWorldLevel>(levelResourcesPath, _myTransform);
            loadedLevel._MyTransform.name = loadedLevel._MyTransform.name.Replace("(Clone)", "");
        }
    }
}