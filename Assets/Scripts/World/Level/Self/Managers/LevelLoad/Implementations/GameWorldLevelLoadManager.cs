using ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Self.DirectoryPath;
using ICouldGames.DefenseOfThrones.World.Level.Self.Info;
using ICouldGames.DefenseOfThrones.World.Level.Self.Info.Providers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Self.Processed;
using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Managers.LevelLoad.Implementations
{
    public class GameWorldLevelLoadManager : IInitializable, IWorldLevelLoadManager
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private IWorldLevelInfoProvider _worldLevelInfoProvider;

        private ProcessedWorldLevel _loadedLevel;

        public void Initialize()
        {
            var defaultEndlessLevelInfo = _worldLevelInfoProvider.GetDefaultWorldLevelInfo<EndlessWorldLevelInfo>();
            LoadLevel(defaultEndlessLevelInfo);
        }

        public void LoadLevel(WorldLevelInfo levelInfo)
        {
            UnloadCurrentLevel();

            var levelResourcesPath = WorldLevelPathUtil.GetProcessedPrefabResourcesPath(levelInfo.Id);
            _loadedLevel = _diContainer.InstantiatePrefabResourceForComponent<ProcessedWorldLevel>(levelResourcesPath);
            _loadedLevel._MyTransform.name = _loadedLevel._MyTransform.name.Replace("(Clone)", "");
            _loadedLevel.Init();
        }

        public IWorldLevelController GetLoadedWorldLevelController()
        {
            return _loadedLevel.WorldLevelController;
        }

        public bool IsAnyLevelLoaded()
        {
            return _loadedLevel != null;
        }

        public void UnloadCurrentLevel()
        {
            if (IsAnyLevelLoaded())
            {
                Object.Destroy(_loadedLevel.gameObject);
                _loadedLevel = null;
            }
        }
    }
}