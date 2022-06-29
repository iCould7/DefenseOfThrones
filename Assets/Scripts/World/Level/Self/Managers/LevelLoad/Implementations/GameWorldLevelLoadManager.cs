using ICouldGames.DefenseOfThrones.World.Level.Self.DirectoryPath;
using ICouldGames.DefenseOfThrones.World.Level.Self.Info;
using ICouldGames.DefenseOfThrones.World.Level.Self.Info.Managers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Self.Processed;
using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Managers.LevelLoad.Implementations
{
    public class GameWorldLevelLoadManager : IInitializable, IWorldLevelLoadManager
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private IWorldLevelInfoProvider _worldLevelInfoProvider;

        private ProcessedWorldLevel _activeLevel;

        public void Initialize()
        {
            var defaultEndlessLevelInfo = _worldLevelInfoProvider.GetDefaultWorldLevelInfo<EndlessWorldLevelInfo>();
            LoadLevel(defaultEndlessLevelInfo);
        }

        public void LoadLevel(WorldLevelInfo levelInfo)
        {
            UnloadActiveLevel();

            var levelResourcesPath = WorldLevelPathUtil.GetProcessedPrefabResourcesPath(levelInfo.Id);
            _activeLevel = _diContainer.InstantiatePrefabResourceForComponent<ProcessedWorldLevel>(levelResourcesPath);
            _activeLevel._MyTransform.name = _activeLevel._MyTransform.name.Replace("(Clone)", "");
            _activeLevel.Init();
        }

        public void UnloadActiveLevel()
        {
            if (_activeLevel != null)
            {
                Object.Destroy(_activeLevel.gameObject);
                _activeLevel = null;
            }
        }
    }
}