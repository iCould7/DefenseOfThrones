using System;
using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.Utils.KeyValue;
using ICouldGames.DefenseOfThrones.World.Level.Self.Id;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Info.Collections
{
    [CreateAssetMenu(fileName = "EndlessLevelEnemiesInfoCollection", menuName = "Collections/LevelEnemies/EndlessLevelEnemiesInfoCollection", order = 3)]
    public class EndlessLevelEnemiesInfoCollection : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<SerializableKeyValuePair<WorldLevelId, EndlessLevelEnemiesInfo>> _EnemiesInfo;

        [NonSerialized] public Dictionary<WorldLevelId, EndlessLevelEnemiesInfo> EnemiesInfosByLevelId = new();

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            EnemiesInfosByLevelId.Clear();
            foreach (var keyValuePair in _EnemiesInfo)
            {
                EnemiesInfosByLevelId[keyValuePair._Key] = keyValuePair._Value;
            }
        }
    }
}