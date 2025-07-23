using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.FigureFactory.Factories;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Infrastructure;
using Game.Scripts.Slots;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.FigureFactory
{
    public class FigureSpawner : MonoBehaviour
    {
        [SerializeField] private DeathZone deathZone; // TODO: можно заинжектить
        [SerializeField] private DropSlot[] dropSlots; // TODO: можно заинжектить
        [SerializeField] private Transform[] spawnPoints;

        private Dictionary<Type, IFigureFactory> _factories  = new Dictionary<Type, IFigureFactory>();
        
        private Coroutine _spawnCoroutine;
        private List<Type> _figureTypes;
        private bool _isSpawning = false;
        private FigureConfig _figureConfig;
        private float _nextRandomSpeed;

        [Inject]
        public void Construct(List<IFigureFactory> factories, FigureConfig figureConfig)
        {
            _factories = factories.ToDictionary(f => f.FigureType);
            _figureTypes = _factories.Keys.ToList();
            _figureConfig = figureConfig;
            Debug.Log("_figureConfig in FigureSpawner: " + _figureConfig.FigureSpeedMin + " - " + _figureConfig.FigureSpeedMax);
        }

        private void OnEnable()
        {
            _nextRandomSpeed = Random.Range(_figureConfig.FigureSpeedMin, _figureConfig.FigureSpeedMax);
            
            if (deathZone != null)
            {
                deathZone.FigureDespawned += OnFigureDespawned;
            }
            
            if(dropSlots != null && dropSlots.Length > 0)
            {
                foreach (var slot in dropSlots)
                {
                    slot.FigureDespawned += OnFigureDespawned;
                }
            }
        }
        
        private void OnDisable()
        {
            if (deathZone != null)
            {
                deathZone.FigureDespawned -= OnFigureDespawned;
            }

            if(dropSlots != null && dropSlots.Length > 0)
            {
                foreach (var slot in dropSlots)
                {
                    slot.FigureDespawned += OnFigureDespawned;
                }
            }
            
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
            
            _isSpawning = false; // TODO: можно отдать на контроль вышестоящей сущности
        }
        
        private void Start()
        {
            _isSpawning = true; // TODO: можно отдать на контроль вышестоящей сущности
            
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
                var newFigure = factory.GetFromPool(position, parent);
                newFigure.OnSpawn(position, _nextRandomSpeed);
                _nextRandomSpeed = Random.Range(_figureConfig.FigureSpeedMin, _figureConfig.FigureSpeedMax);
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
            while (_isSpawning)
            {
                SpawnRandomFigure();
                yield return new WaitForSeconds(1f); // TODO: можно сделать настраиваемым частоту спавна
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