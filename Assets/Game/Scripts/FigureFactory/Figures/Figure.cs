using System;
using Game.Scripts.Slots;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.FigureFactory.Figures
{
    public class Figure : MonoBehaviour, IPoolable, IDraggable
    {
        [Header("References: ")]
        [SerializeField] private SpriteRenderer spriteRenderer; 
        
        private float _movementSpeed;
        private float _currentSpeed;
        private Vector3 _originalPosition;
        private FigureType _figureType;

        public FigureType FigureType => _figureType;
        
        public void Initialize(FigureType figureType)
        {
            spriteRenderer.sprite = figureType.FigureSprite;
            _figureType = figureType;
        }
        
        private void OnDisable()
        {
            _currentSpeed = 0f;
            _figureType = null;
        }

        private void Update()
        {
            MoveRight();
        }
        
        public virtual void OnSpawn(Vector3 position, float speed)
        {
            _movementSpeed = speed;
            _currentSpeed = _movementSpeed;
            transform.position = position;
            gameObject.SetActive(true);
        }

        public virtual void OnDespawn()
        {
            gameObject.SetActive(false);
        }

        public void StartDrag()
        {
            _originalPosition = transform.position;
            _currentSpeed = 0f;
        }

        public void EndDrag()
        {
            if (TrySnapToSlot())
            {
                return;
            }
            
            transform.position = _originalPosition;
            _currentSpeed = _movementSpeed;
        }

        private void MoveRight()
        {
            transform.Translate(Vector3.right * (_currentSpeed * Time.deltaTime));
        }
        
        private bool TrySnapToSlot()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out DropSlot dropSlot))
                {
                    dropSlot.SnapFigureToSlot(this);
                    return true;
                }
            }

            return false;
        }
    }
}