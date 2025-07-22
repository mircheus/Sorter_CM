using Game.Scripts.FigureFactory.Figures;
using UnityEngine;

namespace Game.Scripts.FigureFactory.Factories
{
    public class SquareFactory : PooledFigureFactory<Square>
    {
        public SquareFactory(Square prefab, int size, Transform parent) : base(prefab, size, parent)
        {
        }
    }
}