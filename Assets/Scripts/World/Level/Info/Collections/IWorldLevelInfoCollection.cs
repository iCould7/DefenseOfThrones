using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Info.Collections
{
    public interface IWorldLevelInfoCollection : ISerializationCallbackReceiver
    {
        WorldLevelInfo GetDefaultWorldLevelInfo();
    }
}