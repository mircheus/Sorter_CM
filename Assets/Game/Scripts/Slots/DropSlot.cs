using System;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Infrastructure;
using Game.Scripts.Model;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Slots
{
    public class DropSlot : MonoBehaviour
    {
        [SerializeField] private Figure slotType;
        
        public event UnityAction<Figure> FigureDespawned;

        private void Awake()
        {
            slotType.gameObject.SetActive(false);
        }

        public void SnapFigureToSlot(Figure figure)
        {
            if (figure == null)
            {
                Debug.LogWarning("Figure transform is null. Cannot snap to slot.");
                return;
            }
            
            var figureType = figure.GetType();
            FigureDespawned?.Invoke(figure);
            var isCorrectFigure = figureType == slotType.GetType();
            EventBus.RaiseEvent<IFigureSnapEvent>(model => model.SnapFigureCorrectly(isCorrectFigure));
            
            if (isCorrectFigure)
            {
                Debug.Log("<color=green>Figure snapped to slot successfully.</color>");
            }
            else
            {
                Debug.Log($"<color=red>Figure type {figureType} does not match slot type {slotType.GetType()}. Figure will not be snapped.</color>");
            }
        }
    }
}