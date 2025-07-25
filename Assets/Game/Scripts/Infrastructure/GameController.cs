using System;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Infrastructure;
using Game.Scripts.Model;
using Game.Scripts.Spawner;
using UnityEngine;

namespace Game.Scripts
{
    public class GameController : MonoBehaviour, IRestartGameEvents
    {
        [SerializeField] private int figuresCount;
        [SerializeField] private int startHealth = 100;
        [SerializeField] private int startScore = 0;
        
        private Model.Model _model;

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
            _model = new Model.Model(figuresCount, startHealth, startScore);
            UpdateUI();
        }

        public void RestartGame()
        {
            _model.ResetModel(figuresCount, startHealth, startScore);
            UpdateUI();
            Debug.Log("Eventbus.Subscribers.Count: " + EventBus.SubscribersList.Count);
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