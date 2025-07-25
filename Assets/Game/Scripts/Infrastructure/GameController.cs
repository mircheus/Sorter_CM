using Game.Scripts.Events;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Spawner;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure
{
    public class GameController : MonoBehaviour, IRestartGameEvents
    {
        private int _figuresCount = 10;
        private int _startHealth = 100;
        private int _startScore = 0;
        
        private Model _model;

        [Inject]
        public void Construct(LevelSettings settings)
        {
            _figuresCount = Random.Range(settings.FiguresToSortMin, settings.FiguresToSortMax);
            _startHealth = settings.HealthPoints;
            _startScore = settings.Score;
            _model = new Model(_figuresCount, _startHealth, _startScore);
        }

        private void OnEnable()
        {
            EventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(this);
        }
        
        private void Start()
        {
            UpdateUI();
        }

        public void RestartGame()
        {
            _model.ResetModel(_figuresCount, _startHealth, _startScore);
            UpdateUI();
            EventBus.RaiseEvent<IResettable>(resettable => resettable.ResetState());
            EventBus.RaiseEvent<IPausable>(pausable => pausable.Resume());
        }

        private void UpdateUI()
        {
            EventBus.RaiseEvent<IUpdateUIEvents>(ui => ui.UpdateHealth(_model.Health));
            EventBus.RaiseEvent<IUpdateUIEvents>(ui => ui.UpdateScore(_model.Score));
        }
    }
}