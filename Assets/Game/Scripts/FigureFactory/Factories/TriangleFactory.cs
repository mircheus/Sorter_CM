using Game.Scripts.FigureFactory.Figures;
using UnityEngine;

namespace Game.Scripts.FigureFactory.Factories
{
    public class TriangleFactory : PooledFigureFactory<Triangle>
    {
        public TriangleFactory(Triangle prefab, int size, Transform parent) : base(prefab, size, parent)
        {
        }
    }
}