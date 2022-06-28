using ICouldGames.DefenseOfThrones.World.Level.Enemy.Info.Collections.Combined;
using ICouldGames.DefenseOfThrones.World.Level.Self.Id;
using Unity.VisualScripting;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Info.Managers.Main.Implementations
{
    public class GameLevelEnemiesInfoProvider : IInitializable, ILevelEnemiesInfoProvider
    {
        private CombinedLevelEnemiesInfoCollections _levelEnemiesInfoCollections;

        public void Initialize()
        {
            _levelEnemiesInfoCollections = Resources.Load<CombinedLevelEnemiesInfoCollections>(CombinedLevelEnemiesInfoCollections.RESOURCES_PATH);
        }

        public T GetLevelEnemiesInfo<T>(WorldLevelId levelId) where T : LevelEnemiesInfo
        {
            return _levelEnemiesInfoCollections.GetLevelEnemiesInfo<T>(levelId);
        }
    }
}