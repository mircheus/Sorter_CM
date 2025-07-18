using Game.Scripts.FigureFactory.Figures;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Scripts.FigureFactory
{
    public abstract class PooledFigureFactory<T> : MonoBehaviour, IFigureFactory where T : Figure
    {
        private readonly ObjectPool<T> _pool;

        protected PooledFigureFactory(T prefab, int size, Transform parent = null)
        {
            _pool = new ObjectPool<T>(prefab.gameObject, size, parent);
        }
        
        public virtual T GetFromPool(Vector3 spawnPosition)
        {
            return _pool.Get(spawnPosition);
        }
        
        public void ReturnToPool(T figure)
        {
            _pool.ReturnToPool(figure);
        }
        
        // Interface implementations
        Figure IFigureFactory.GetFromPool(Vector3 spawnPosition)
        {
            return GetFromPool(spawnPosition);
        }

        void IFigureFactory.ReturnToPool(Figure figure)
        {
            if (figure is T typedFigure)
                ReturnToPool(typedFigure);
        }
    }
}