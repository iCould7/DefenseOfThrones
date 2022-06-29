using ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Processed
{
    public class ProcessedWorldLevel : MonoBehaviour
    {
        [Inject] private IWorldLevelController _worldLevelController;

        [ReadOnly] public Transform _MyTransform;
        [ReadOnly] public WorldLevelProcessedData _LevelProcessedData;

        public void Init()
        {
            _worldLevelController.Init(_LevelProcessedData);
        }

        private void OnDestroy()
        {
            _worldLevelController.Reset();
        }
    }
}