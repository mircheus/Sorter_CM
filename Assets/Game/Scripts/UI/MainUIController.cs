using Game.Scripts.Infrastructure;
using Game.Scripts.Infrastructure.Model;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class MainUIController : MonoBehaviour, IUpdateUIEvents
    {
        [SerializeField] private FiguresCountView figuresCountView;
        [SerializeField] private HealthView healthView;

        private void OnEnable()
        {
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
            figuresCountView.UpdateScore(score);
        }
    }
}
