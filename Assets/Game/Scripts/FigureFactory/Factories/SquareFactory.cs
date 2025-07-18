using UnityEngine;

namespace Game.Scripts.FigureFactory
{
    public class SquareFactory : PooledFigureFactory<Square>
    {
        public SquareFactory(GameObject prefab, int size, Transform parent) : base(prefab, size, parent)
        {
        }
    }
}