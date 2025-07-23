using Game.Scripts.Model;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure
{
    public class GameController : MonoBehaviour
    {
        private IModel _model;
        
        [Inject]
        public void Construct(IModel model)
        {
            _model = model;
            Debug.Log("Injected Model in GameController");
        }

        private void Start()
        {
            UpdateInitialUI();
        }

        private void UpdateInitialUI()
        {
            EventBus.RaiseEvent<IUpdateUIEvents>(ui => ui.UpdateHealth(_model.Health));
            EventBus.RaiseEvent<IUpdateUIEvents>(ui => ui.UpdateScore(_model.FiguresCount));
        }
    }
}