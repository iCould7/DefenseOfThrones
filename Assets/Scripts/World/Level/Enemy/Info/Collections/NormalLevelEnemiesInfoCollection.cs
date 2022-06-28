using System;
using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.Utils.KeyValue;
using ICouldGames.DefenseOfThrones.World.Level.Self.Id;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Info.Collections
{
    [CreateAssetMenu(fileName = "NormalLevelEnemiesInfoCollection", menuName = "Collections/LevelEnemies/NormalLevelEnemiesInfoCollection", order = 2)]
    public class NormalLevelEnemiesInfoCollection : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<SerializableKeyValuePair<WorldLevelId, NormalLevelEnemiesInfo>> _EnemiesInfo;

        [NonSerialized] public Dictionary<WorldLevelId, NormalLevelEnemiesInfo> EnemiesInfosByLevelId = new();

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