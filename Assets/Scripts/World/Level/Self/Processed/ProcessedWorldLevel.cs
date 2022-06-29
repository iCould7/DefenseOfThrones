using ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Processed
{
    public class ProcessedWorldLevel : MonoBehaviour
    {
        [Inject] public IWorldLevelController WorldLevelController;

        [ReadOnly] public Transform _MyTransform;
        [ReadOnly] public WorldLevelProcessedData _LevelProcessedData;

        public void Init()
        {
            WorldLevelController.Init(_LevelProcessedData);
        }

        private void OnDestroy()
        {
            WorldLevelController.Reset();
        }
    }
}