using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class EndGameView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button restartButton;

        public event Action RestartButtonClicked;
        
        private void Awake()
        {
            Hide();
        }

        private void OnEnable()
        {
            restartButton.onClick.AddListener(OnRestartButtonClicked);
        }
        
        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
            RestartButtonClicked?.Invoke();
        }

        public void Hide()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void Show()
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}
