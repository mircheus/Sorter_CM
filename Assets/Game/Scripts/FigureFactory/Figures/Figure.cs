using System;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.FigureFactory.Figures
{
    // [RequireComponent(typeof(Draggable))]
    public abstract class Figure : MonoBehaviour, IPoolable, IDraggable
    {
        [SerializeField] private float speed = 5f;

        private float _currentSpeed;
        // private Draggable _draggable;
        
        private void OnEnable()
        {
            _currentSpeed = speed;
            // _draggable = GetComponent<Draggable>();
            // _draggable.OnDragStarted += OnDragStarted;
            // _draggable.OnDragEnded += OnDragEnded;
        }

        private void OnDisable()
        {
            _currentSpeed = 0f;
            // _draggable.OnDragStarted -= OnDragStarted;
            // _draggable.OnDragEnded -= OnDragEnded;
        }

        private void Update()
        {
            transform.Translate(Vector3.right * (_currentSpeed * Time.deltaTime));
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
        
        private void OnDragStarted()
        {
            _currentSpeed = 0f;
        }
        
        private void OnDragEnded()
        {
            _currentSpeed = speed;
        }

        public void StartDrag()
        {
            Debug.Log("StartDrag");
            _currentSpeed = 0f;
        }

        public void EndDrag()
        {
            Debug.Log("EndDrag");
        }
    }
}