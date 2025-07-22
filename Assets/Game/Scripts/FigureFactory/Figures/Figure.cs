using System;
using Game.Scripts.Slots;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.FigureFactory.Figures
{
    // [RequireComponent(typeof(Draggable))]
    public abstract class Figure : MonoBehaviour, IPoolable, IDraggable
    {
        [SerializeField] private float speed = 5f;

        private float _currentSpeed;
        private Vector3 _originalPosition;

        private void OnEnable()
        {
            _currentSpeed = speed;
        }

        private void OnDisable()
        {
            _currentSpeed = 0f;
        }

        private void Update()
        {
            MoveRight();
        }

        public virtual void OnSpawn(Vector3 position)
        {
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
            _currentSpeed = speed;
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