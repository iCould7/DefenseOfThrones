using ICouldGames.DefenseOfThrones.World.Level.Self.Id;
using ICouldGames.DefenseOfThrones.World.Level.Self.Info.Collections.Combined;
using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Info.Providers.Main.Implementations
{
    public class GameWorldLevelInfoProvider : IInitializable, IWorldLevelInfoProvider
    {
        private CombinedWorldLevelInfoCollections _worldLevelInfoCollections;

        public void Initialize()
        {
            _worldLevelInfoCollections = Resources.Load<CombinedWorldLevelInfoCollections>(CombinedWorldLevelInfoCollections.RESOURCES_PATH);
        }

        public T GetWorldLevelInfo<T>(WorldLevelId levelId) where T : WorldLevelInfo
        {
            return _worldLevelInfoCollections.GetWorldLevelInfo<T>(levelId);
        }

        public T GetDefaultWorldLevelInfo<T>() where T : WorldLevelInfo
        {
            return _worldLevelInfoCollections.GetDefaultWorldLevelInfo<T>();
        }
    }
}