using System;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Controllers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Enums;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ICouldGames.DefenseOfThrones.UI.LevelTower
{
    public class SpawnLevelTowerButton : MonoBehaviour
    {
        [Inject] private ILevelTowerController _levelTowerController;

        [SerializeField] private Button _SpawnButton;

        private void Start()
        {
            _SpawnButton.onClick.AddListener(SpawnTower);
        }

        private void OnDestroy()
        {
            _SpawnButton.onClick.RemoveListener(SpawnTower);
        }

        private void SpawnTower()
        {
            _levelTowerController.SpawnTower(LevelTowerType.MachineGun);
        }
    }
}