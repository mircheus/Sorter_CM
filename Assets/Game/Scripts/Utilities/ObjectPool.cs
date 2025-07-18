using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Utilities
{
    public class ObjectPool<T> where T : MonoBehaviour, IPoolable
    {
        private readonly Queue<T> _pool = new Queue<T>();
        private readonly GameObject _prefab;
        private readonly Transform _parent;

        public ObjectPool(GameObject prefab, int initialSize, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                var instance = Object.Instantiate(_prefab, _parent).GetComponent<T>();
                instance.gameObject.SetActive(false);
                _pool.Enqueue(instance);
            }
        }

        public T Get(Vector3 position)
        {
            T instance = _pool.Count > 0 ? _pool.Dequeue() : GameObject.Instantiate(_prefab, _parent).GetComponent<T>();
            instance.OnSpawn(position);
            return instance;
        }

        public void ReturnToPool(T instance)
        {
            instance.OnDespawn();
            _pool.Enqueue(instance);
        }
    }
}