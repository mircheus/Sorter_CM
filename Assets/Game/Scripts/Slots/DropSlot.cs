using System;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Infrastructure;
using Game.Scripts.Model;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Slots
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DropSlot : MonoBehaviour // TODO: можно в SlotSpawner это всё закинуть
    {
        [Header("References: ")]
        [SerializeField] private FigureType slotType;

        private SpriteRenderer _spriteRenderer;
        
        public event UnityAction<Figure> FigureDespawned; // TODO: переделать под EventBus

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void OnEnable()
        {
            if (_spriteRenderer.sprite == null)
            {
                _spriteRenderer.sprite = slotType.FigureSprite;
            }
        }

        public void SnapFigureToSlot(Figure figure)
        {
            if (figure == null)
            {
                return;
            }
            
            FigureDespawned?.Invoke(figure);
            var isCorrectFigure = slotType == figure.FigureType;
            EventBus.RaiseEvent<IFigureSnapEvent>(model => model.SnapFigure(isCorrectFigure));
        }
    }
}