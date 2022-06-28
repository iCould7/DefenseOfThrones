using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.World.Level.Self.Id;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Info.Collections.Combined
{
    [CreateAssetMenu(fileName = "CombinedLevelEnemiesInfoCollection", menuName = "Collections/LevelEnemies/CombinedLevelEnemiesInfoCollections", order = 1)]
    public class CombinedLevelEnemiesInfoCollections : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private NormalLevelEnemiesInfoCollection _NormalLevelEnemiesInfoCollection;
        [SerializeField] private EndlessLevelEnemiesInfoCollection _EndlessLevelEnemiesInfoCollection;

        private Dictionary<WorldLevelId, LevelEnemiesInfo> _levelEnemiesInfosByLevelId = new();

        public const string RESOURCES_PATH = "LevelEnemiesInfos/CombinedLevelEnemiesInfoCollections";

        public T GetLevelEnemiesInfo<T>(WorldLevelId levelId) where T : LevelEnemiesInfo
        {
            return (T) _levelEnemiesInfosByLevelId[levelId];
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _NormalLevelEnemiesInfoCollection.FillEnemiesInfosByLevelId();
            _EndlessLevelEnemiesInfoCollection.FillEnemiesInfosByLevelId();
            FillLevelEnemiesInfos();
        }

        private void FillLevelEnemiesInfos()
        {
            _levelEnemiesInfosByLevelId.Clear();
            foreach (var (levelId, enemiesInfo) in _NormalLevelEnemiesInfoCollection.EnemiesInfosByLevelId)
            {
                _levelEnemiesInfosByLevelId[levelId] = enemiesInfo;
            }

            foreach (var (levelId, enemiesInfo) in _EndlessLevelEnemiesInfoCollection.EnemiesInfosByLevelId)
            {
                _levelEnemiesInfosByLevelId[levelId] = enemiesInfo;
            }
        }
    }
}