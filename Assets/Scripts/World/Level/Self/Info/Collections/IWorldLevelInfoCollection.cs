using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Info.Collections
{
    public interface IWorldLevelInfoCollection : ISerializationCallbackReceiver
    {
        WorldLevelInfo GetDefaultWorldLevelInfo();
    }
}