using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Infrastructure;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Scripts.FigureFactory
{
    public class FigureSpawner : MonoBehaviour
    {
        private Dictionary<FigureType, IFigureFactory> _factories  = new Dictionary<FigureType, IFigureFactory>();

        [Inject]
        public void Construct(
            [Inject(Id = FigureType.Square)] IFigureFactory squareFactory)
            // [Inject(Id = FigureType.Circle)] IFigureFactory circleFactory,
            // [Inject(Id = FigureType.Triangle)] IFigureFactory triangleFactory,
            // [Inject(Id = FigureType.Star)] IFigureFactory starFactory)
        {
            _factories = new Dictionary<FigureType, IFigureFactory>
            {
                { FigureType.Square, squareFactory },
                // { FigureType.Circle, circleFactory },
                // { FigureType.Triangle, triangleFactory },
                // { FigureType.Star, starFactory },
            };
        }

        private void Start()
        {
            // Example of spawning figures at start
            Spawn(FigureType.Square, new Vector3(0, 0, 0));
        }

        public void Spawn(FigureType type, Vector3 pos)
        {
            _factories[type].GetFromPool(pos);
        }
    }
}