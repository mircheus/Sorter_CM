using System.Collections.Generic;
using Game.Scripts.FigureFactory;
using Game.Scripts.FigureFactory.Factories;
using Game.Scripts.FigureFactory.Figures;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        public GameObject squarePrefab;
        public GameObject circlePrefab;
        public GameObject trianglePrefab;
        public GameObject starPrefab;

        public override void InstallBindings()
        {
            Container.Bind<IFigureFactory>()
                .WithId(FigureType.Square)
                .To<SquareFactory>()
                .AsSingle()
                .WithArguments(squarePrefab, 5, transform);

            Container.Bind<IFigureFactory>()
                .WithId(FigureType.Circle)
                .To<CircleFactory>()
                .AsSingle()
                .WithArguments(circlePrefab, 5, this.transform);
            
            Container.Bind<IFigureFactory>()
                .WithId(FigureType.Triangle)
                .To<TriangleFactory>()
                .AsSingle()
                .WithArguments(trianglePrefab, 5, this.transform);
            
            Container.Bind<IFigureFactory>()
                .WithId(FigureType.Star)
                .To<StarFactory>()
                .AsSingle()
                .WithArguments(starPrefab, 5, this.transform);
        }
    }


}
