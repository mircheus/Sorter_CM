using UnityEngine;

namespace Game.Scripts.FigureFactory
{
    public class SquareFactory : PooledFigureFactory<Square>
    {
        public SquareFactory(Square prefab, int size, Transform parent = null) : base(prefab, size, parent)
        {
        }
    }
}