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
                return;
            }
            
            var figureType = figure.GetType();
            FigureDespawned?.Invoke(figure);
            var isCorrectFigure = figureType == slotType.GetType();
            EventBus.RaiseEvent<IFigureSnapEvent>(model => model.SnapFigure(isCorrectFigure));
        }
    }
}