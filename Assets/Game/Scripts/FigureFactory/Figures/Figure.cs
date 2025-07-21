using System;
using UnityEngine;

namespace Game.Scripts.FigureFactory.Figures
{
    public abstract class Figure : MonoBehaviour, IPoolable
    {
        [SerializeField] private float speed = 5f;

        private float _currentSpeed;
        
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
    }
}