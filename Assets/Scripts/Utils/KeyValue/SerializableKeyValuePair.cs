using System;

namespace ICouldGames.DefenseOfThrones.Utils.KeyValue
{
    [Serializable]
    public class SerializableKeyValuePair<TKey, TValue>
    {
        public TKey _Key;
        public TValue _Value;
    }
}