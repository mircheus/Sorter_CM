using System;
using Game.Scripts.Infrastructure.Model;
using Game.Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts.Infrastructure
{
    public class GameController : MonoBehaviour, IRestartGameEvents
    {
        [SerializeField] private int figuresCount;
        [SerializeField] private int startHealth = 100;
        [SerializeField] private int startScore = 0;
            // var model = new Model.Model(figuresCount, startHealth, startScore);
        private IModel _model;

        public int FiguresCount => _model.FiguresCount;

        private void OnEnable()
        {
            EventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(this);
        }

        // [Inject]
        // public void Construct(IModel model)
        // {
        //     _model = model;
        //     Debug.Log("Injected Model in GameController");
        // }

        private void Start()
        {
            _model = new Model.Model(figuresCount, startHealth, startScore);
            UpdateInitialUI();
        }

        private void UpdateInitialUI()
        {
            EventBus.RaiseEvent<IUpdateUIEvents>(ui => ui.UpdateHealth(_model.Health));
            EventBus.RaiseEvent<IUpdateUIEvents>(ui => ui.UpdateScore(_model.Score));
        }

        public void RestartGame()
        {
            IDisposable modelDisposable = _model as IDisposable;
            if (modelDisposable != null) modelDisposable.Dispose(); // TODO: сделать Dispose через отдельный сервис
            SceneManager.LoadScene(Constants.GameLevel); // TODO: Сделать перезагрузку уровня через reset а не через загрузку сцены
        }
    }
}