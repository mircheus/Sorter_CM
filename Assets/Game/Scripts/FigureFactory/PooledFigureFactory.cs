using System;
using Game.Scripts.FigureFactory.Factories;
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
        public Type FigureType => typeof(T); // 👈 Added
        
        Figure IFigureFactory.GetFromPool(Vector3 spawnPosition, Transform parent = null) => GetFromPool(spawnPosition, parent);
        void IFigureFactory.ReturnToPool(Figure figure) => ReturnToPool((T)figure);

        [Inject]
        protected PooledFigureFactory(T prefab, int size, Transform parent)
        {
            _pool = new ObjectPool<T>(prefab, size, parent);
        }

        public virtual T GetFromPool(Vector3 spawnPosition, Transform parent = null)
        {
            return _pool.Get(spawnPosition, parent);
        }

        private void ReturnToPool(T figure)
        {
            _pool.ReturnToPool(figure);
        }
    }
}