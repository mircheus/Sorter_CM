using Game.Scripts.FigureFactory.Figures;
using UnityEngine;

namespace Game.Scripts.FigureFactory.Factories
{
    public class StarFactory : PooledFigureFactory<Star>
    {
        public StarFactory(GameObject prefab, int size, Transform parent) : base(prefab, size, parent)
        {
        }
    }
}