using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.FigureFactory.Factories;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Infrastructure;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Scripts.FigureFactory
{
    public class FigureSpawner : MonoBehaviour
    {
        [SerializeField] private DeathZone deathZone;
        [SerializeField] private Transform[] spawnPoints;
        
        private Dictionary<Type, IFigureFactory> _factories  = new Dictionary<Type, IFigureFactory>();
        
        private Coroutine _spawnCoroutine;
        private List<Type> _figureTypes;

        [Inject]
        public void Construct(List<IFigureFactory> factories)
        {
            _factories = factories.ToDictionary(f => f.FigureType);
            _figureTypes = _factories.Keys.ToList();
        }

        private void OnEnable()
        {
            if (deathZone != null)
            {
                deathZone.FigureDespawned += OnFigureDespawned;
            }
            // _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }
        
        private void Start()
        {
            if(_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }
            
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }
        
        private void Spawn(Type figure, Vector3 position, Transform parent = null)
        {
            if(_factories.TryGetValue(figure, out var factory))
            {
                factory.GetFromPool(position, parent);
            }
            else
            {
                Debug.LogError($"Factory for type {figure} not found!");
            }
        }

        private void OnFigureDespawned(Figure obj)
        {
            var type = obj.GetType();
            
            if (_factories.TryGetValue(type, out var factory))
            {
                factory.ReturnToPool(obj);
            }
        }
        
        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                SpawnRandomFigure();
                yield return new WaitForSeconds(1f);
            }
        }

        private void SpawnRandomFigure()
        {
            int randomIndex = UnityEngine.Random.Range(0, _figureTypes.Count);
            var randomSpawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            Spawn(_figureTypes[randomIndex], randomSpawnPoint.position, randomSpawnPoint);
        }
    }
}