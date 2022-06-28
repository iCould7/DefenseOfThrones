using ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Self.Implementations.Abstract;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using NaughtyAttributes;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Processed
{
    public class ProcessedWorldLevel : MonoBehaviour
    {
        [ReadOnly] public Transform _MyTransform;
        [ReadOnly] public WorldLevelData _LevelData;
        [ReadOnly] public WorldLevelController _WorldLevelController;

    }
}