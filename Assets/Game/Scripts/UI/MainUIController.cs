using Game.Scripts.Infrastructure;
using Game.Scripts.Infrastructure.Model;
using Game.Scripts.Model;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class MainUIController : MonoBehaviour, IUpdateUIEvents, IEndGameEvents
    {
        [SerializeField] private ScoreView scoreView;
        [SerializeField] private HealthView healthView;
        
        [Header("End Game views: ")]
        [SerializeField] private GameWinView gameWinView;
        [SerializeField] private GameOverView gameOverView;

        private void OnEnable()
        {
            // gameOverView.gameObject.SetActive(false);
            // gameWinView.gameObject.SetActive(false);
            EventBus.Subscribe(this);
        }
        
        private void OnDisable()
        {
            EventBus.Unsubscribe(this);
        }

        public void UpdateHealth(int health)
        {
            healthView.UpdateHealth(health);
        }

        public void UpdateScore(int score)
        {
            scoreView.UpdateScore(score);
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
