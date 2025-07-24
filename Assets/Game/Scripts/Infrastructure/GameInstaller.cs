using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Infrastructure;
using Game.Scripts.Spawner;
using Game.Scripts.Utilities;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class GameInstaller : MonoInstaller
    {
        [Header("References: ")] 
        [SerializeField] private Transform poolParent; 
        [SerializeField] private Figure figurePrefab;
        [SerializeField] private GameController gameController;
        [SerializeField] private FiguresList figuresList;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private SpawnerSettings spawnerSettings;

        public override void InstallBindings()
        {
            Container.Bind<GameController>()
                .FromInstance(gameController)
                .AsSingle()
                .NonLazy();
            
            ObjectPool<Figure> objectPool = new ObjectPool<Figure>(figurePrefab, 25, poolParent); // TODO: где хранить настройку initialSize
            FigureFactory.FigureFactory figureFactory = new FigureFactory.FigureFactory(figuresList, objectPool);

            Container.Bind<FigureFactory.FigureFactory>()
                .FromInstance(figureFactory)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<FiguresList>()
                .FromInstance(figuresList)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<SpawnerSettings>()
                .FromInstance(spawnerSettings)
                .AsSingle()
                .NonLazy();

            Container.Bind<Camera>()
                .FromInstance(mainCamera)
                .AsSingle()
                .NonLazy();
        }
    }
}