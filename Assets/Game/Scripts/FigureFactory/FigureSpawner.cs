using System;
using System.Collections;
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
        [SerializeField] private Transform[] spawnPoints;
        
        private Dictionary<FigureType, IFigureFactory> _factories  = new Dictionary<FigureType, IFigureFactory>();
        
        private Coroutine _spawnCoroutine;

        [Inject]
        public void Construct(
            [Inject(Id = FigureType.Square)] IFigureFactory squareFactory,
            [Inject(Id = FigureType.Circle)] IFigureFactory circleFactory,
            [Inject(Id = FigureType.Triangle)] IFigureFactory triangleFactory,
            [Inject(Id = FigureType.Star)] IFigureFactory starFactory)
        {
            _factories = new Dictionary<FigureType, IFigureFactory>
            {
                { FigureType.Square, squareFactory },
                { FigureType.Circle, circleFactory },
                { FigureType.Triangle, triangleFactory },
                { FigureType.Star, starFactory }
            };
        }

        private void Start()
        {
            // Example of spawning figures at start
            Spawn(FigureType.Square, new Vector3(-4, 0, 0));
            Spawn(FigureType.Circle, new Vector3(-2, 0, 0));
            Spawn(FigureType.Triangle, new Vector3(0, 0, 0));
            Spawn(FigureType.Star, new Vector3(2, 0, 0));
        }

        private void Spawn(FigureType type, Vector3 pos)
        {
            _factories[type].GetFromPool(pos);
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                foreach (var spawnPoint in spawnPoints)
                {
                    var randomType = (FigureType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(FigureType)).Length);
                    Spawn(randomType, spawnPoint.position);
                }
                yield return new WaitForSeconds(2f); // Adjust the delay as needed
            }
        } 
    }
}