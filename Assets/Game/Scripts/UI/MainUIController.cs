using Game.Scripts.Infrastructure;
using Game.Scripts.Infrastructure.Model;
using Game.Scripts.Model;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class MainUIController : MonoBehaviour, IUpdateUIEvents, IEndGameEvents
    {
        [Header("UI views: ")]
        [SerializeField] private ScoreView scoreView;
        [SerializeField] private HealthView healthView;
        
        [Header("End Game views: ")]
        [SerializeField] private GameWinView gameWinView;
        [SerializeField] private GameOverView gameOverView;

        private void OnEnable()
        {
            gameWinView.RestartButtonClicked += OnRestartClicked;
            gameOverView.RestartButtonClicked += OnRestartClicked;
            EventBus.Subscribe(this);
        }
        
        private void OnDisable()
        {
            gameWinView.RestartButtonClicked -= OnRestartClicked;
            gameOverView.RestartButtonClicked -= OnRestartClicked;
            EventBus.Unsubscribe(this);
        }

        private void OnRestartClicked()
        {
            EventBus.RaiseEvent<IRestartGameEvents>(e => e.RestartGame());
        }

        public void UpdateHealth(int health)
        {
            healthView.UpdateHealth(health);
        }

        public void UpdateScore(int score)
        {
            scoreView.UpdateScore(score);
        }

        public void UpdateFiguresCount(int figuresCount)
        {
            // Debug.Log("FiguresCount: " + figuresCount);
        }

        public void OnGameWin(int score)
        {
            gameWinView.SetScore(score);
            gameWinView.gameObject.SetActive(true);
            gameWinView.Show();
        }

        public void OnGameOver()
        {
            gameOverView.gameObject.SetActive(true);
            gameOverView.Show();
        }
    }
}
