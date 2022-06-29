using ICouldGames.DefenseOfThrones.World.Level.Self.Managers.LevelLoad;
using ICouldGames.DefenseOfThrones.World.Level.Self.Signals;
using TMPro;
using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.HUD.LevelScore
{
    public class LevelScorePointsArea : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private IWorldLevelLoadManager _levelLoadManager;

        [SerializeField] private TextMeshProUGUI _ScoreText;

        private void Start()
        {
            InitFirstText();

            _signalBus.Subscribe<WorldLevelScoreUpdatedSignal>(OnScoreUpdate);
            _signalBus.Subscribe<WorldLevelStartedSignal>(OnLevelStart);
        }

        private void InitFirstText()
        {
            if(_levelLoadManager.IsAnyLevelLoaded())
            {
                var levelController = _levelLoadManager.GetLoadedWorldLevelController();
                if (levelController.IsLevelActive())
                {
                    UpdateScore(levelController.GetCurrentLevelData().ScorePoints);
                }
            }
        }

        private void OnDestroy()
        {
            _signalBus?.TryUnsubscribe<WorldLevelScoreUpdatedSignal>(OnScoreUpdate);
            _signalBus?.TryUnsubscribe<WorldLevelStartedSignal>(OnLevelStart);
        }

        private void UpdateScore(int scorePoints)
        {
            _ScoreText.text = "Score: " + scorePoints;
        }

        private void OnScoreUpdate(WorldLevelScoreUpdatedSignal signal)
        {
            UpdateScore(signal.NewScorePoints);
        }

        private void OnLevelStart(WorldLevelStartedSignal signal)
        {
            UpdateScore(signal.LevelData.ScorePoints);
        }

    }
}