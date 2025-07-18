using System;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Game.Scripts.FigureFactory
{
    public abstract class PooledFigureFactory<T> : IFigureFactory where T : Figure
    {
        private readonly ObjectPool<T> _pool;
        // public Type FigureType => typeof(T); // 👈 Added
        
        Figure IFigureFactory.GetFromPool(Vector3 spawnPosition) => GetFromPool(spawnPosition);
        void IFigureFactory.ReturnToPool(Figure figure) => ReturnToPool((T)figure);

        [Inject]
        protected PooledFigureFactory(GameObject prefab, int size, Transform parent)
        {
            _pool = new ObjectPool<T>(prefab, size, parent);
        }

        public virtual T GetFromPool(Vector3 spawnPosition)
        {
            return _pool.Get(spawnPosition);
        }

        private void ReturnToPool(T figure)
        {
            _pool.ReturnToPool(figure);
        }
    }
}