using System;
using System.Collections.Generic;
using System.Reflection;
using Game.Scripts.FigureFactory;
using Game.Scripts.FigureFactory.Factories;
using Game.Scripts.FigureFactory.Figures;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        // public List<Figure> figures;
        public Transform objectPool;
        public Square squarePrefab;
        public Circle circlePrefab;
        public Triangle trianglePrefab;
        public Star starPrefab;

        public override void InstallBindings()
        {
            BindFactory<SquareFactory, Square>(squarePrefab);
            BindFactory<CircleFactory, Circle>(circlePrefab);
            BindFactory<TriangleFactory, Triangle>(trianglePrefab);
            BindFactory<StarFactory, Star>(starPrefab);
        }

        private void BindFactory<TFactory, TFigure>(TFigure prefab)
            where TFactory : PooledFigureFactory<TFigure>
            where TFigure : Figure
        {
            Container.Bind<IFigureFactory>()
                .To<TFactory>()
                .AsSingle()
                .WithArguments(prefab, 5, objectPool);
        }
    }
}