using UnityEngine;

namespace Game.Scripts.FigureFactory.Factories
{
    public class CircleFactory : PooledFigureFactory<Circle>
    {
        public CircleFactory(GameObject prefab, int size, Transform parent) : base(prefab, size, parent)
        {
        }
    }
}