using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.FigureFactory;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Infrastructure;
using Game.Scripts.Slots;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Spawner
{
    public class FigureSpawner : MonoBehaviour
    {
        [SerializeField] private DeathZone deathZone; // TODO: можно заинжектить
        [SerializeField] private DropSlot[] dropSlots; // TODO: можно заинжектить
        [SerializeField] private Transform[] spawnPoints;
        
        private Coroutine _spawnCoroutine;
        private FiguresList _figuresList;
        private bool _isSpawning = false;
        private float _nextRandomSpeed;
        private FigureFactory.FigureFactory _figureFactory;

        [Inject]
        public void Construct(FigureFactory.FigureFactory factory, FiguresList figuresList)
        {
            _figureFactory = factory;
            _figuresList = figuresList;
        }

        private void OnEnable()
        {
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
        
        private void Spawn(FigureType figureType, Vector3 position, Transform parent = null)
        {
            var figure = _figureFactory.CreateFigure(figureType);
            figure.gameObject.transform.position = position;
            figure.gameObject.SetActive(true);
            // if(_factories.TryGetValue(figure, out var factory))
            // {
            //     var newFigure = factory.GetFromPool(position, parent);
            //     newFigure.OnSpawn(position, _nextRandomSpeed);
            //     _nextRandomSpeed = Random.Range(_figureConfig.FigureSpeedMin, _figureConfig.FigureSpeedMax);
            // }
            // else
            // {
            //     Debug.LogError($"Factory for type {figure} not found!");
            // }
        }

        private void OnFigureDespawned(Figure obj)
        {
            // var type = obj.GetType();
            //
            // if (_factories.TryGetValue(type, out var factory))
            // {
            //     factory.ReturnToPool(obj);
            // }
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
            int randomIndex = Random.Range(0, _figuresList.Figures.Count);
            var randomSpawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            Spawn(_figuresList.Figures[randomIndex], randomSpawnPoint.position, randomSpawnPoint);
        }
    }
}