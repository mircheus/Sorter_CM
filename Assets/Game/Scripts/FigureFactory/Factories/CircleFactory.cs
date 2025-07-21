using UnityEngine;

namespace Game.Scripts.FigureFactory.Factories
{
    public class CircleFactory : PooledFigureFactory<Circle>
    {
        public CircleFactory(Circle prefab, int size, Transform parent) : base(prefab, size, parent)
        {
        }
    }
}