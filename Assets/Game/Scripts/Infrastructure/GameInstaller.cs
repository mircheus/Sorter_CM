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
        [Header("References: ")]
        // [SerializeField] private GameController gameController;
        [SerializeField] private Transform objectPool;
        [SerializeField] private Square squarePrefab;
        [SerializeField] private Circle circlePrefab;
        [SerializeField] private Triangle trianglePrefab;
        [SerializeField] private Star starPrefab;
        
        [Header("Settings: ")]
        [SerializeField] private int figuresCount = 10;
        [SerializeField] private int startScore = 0;
        [SerializeField] private int startHealth = 25;

        public override void InstallBindings()
        {
            BindFactory<SquareFactory, Square>(squarePrefab);
            BindFactory<CircleFactory, Circle>(circlePrefab);
            BindFactory<TriangleFactory, Triangle>(trianglePrefab);
            BindFactory<StarFactory, Star>(starPrefab);

            var model = new Model.Model(figuresCount, startHealth, startScore);
            
            Container.Bind<IModel>()
                .FromInstance(model)
                .AsSingle()
                .NonLazy();
        }

        private void BindFactory<TFactory, TFigure>(TFigure prefab)
            where TFactory : PooledFigureFactory<TFigure>
            where TFigure : Figure
        {
            Container.Bind<IFigureFactory>()
                .To<TFactory>()
                .AsSingle()
                .WithArguments(prefab, 5, objectPool); // TODO: вынести 5 в настройку
        }
    }
}