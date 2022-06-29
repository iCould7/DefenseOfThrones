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
        [ReadOnly] public WorldLevelData _LevelData;

        public void Init()
        {
            _worldLevelController.Init(_LevelData);
        }

        private void OnDestroy()
        {
            _worldLevelController.Reset();
        }
    }
}