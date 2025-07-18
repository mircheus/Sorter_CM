using System;
using System.Collections.Generic;
using Game.Scripts.FigureFactory.Figures;
using TMPro;
using UnityEngine;

namespace Game.Scripts.FigureFactory
{
    public class FigureSpawner : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private Square squarePrefab;
        [SerializeField] private Star starPrefab;
        [SerializeField] private Circle circlePrefab;
        [SerializeField] private Triangle trianglePrefab;

        private Dictionary<Type, IFigureFactory> _factories;
        
        private void Awake()
        {
            _factories = new Dictionary<Type, IFigureFactory>()
            {
                {typeof(Square), gameObject.AddComponent<SquareFactory>()},
                {typeof(Star), gameObject.AddComponent<StarFactory>()}
                
            };
        }

        public void Spawn<T>(Vector3 position) where T : Figure
        {
            var factory = _factories[typeof(T)];
            factory.GetFromPool(position);
        }
    }

    public class StarFactory : PooledFigureFactory<Star>, IFigureFactory
    {
        public StarFactory() : base(Resources.Load<Star>("Prefabs/Figures/Star"), 10, null)
        {
        }
    }
}