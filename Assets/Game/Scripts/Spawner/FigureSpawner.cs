using System.Collections;
using Game.Scripts.FigureFactory;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Slots;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Spawner
{
    public class FigureSpawner : MonoBehaviour
    {
        [Header("References: ")] 
        [SerializeField] private DeathZone deathZone;
        [SerializeField] private DropSlot[] dropSlots;
        [SerializeField] private Transform[] spawnPoints;

        private Coroutine _spawnCoroutine;
        private FiguresList _figuresList;
        private bool _isSpawning = false;
        private float _nextRandomSpeed;
        private FigureFactory.FigureFactory _figureFactory;
        private SpawnerSettings _spawnerSettings;

        [Inject]
        public void Construct(FigureFactory.FigureFactory factory, FiguresList figuresList,
            SpawnerSettings spawnerSettings)
        {
            _figureFactory = factory;
            _figuresList = figuresList;
            _spawnerSettings = spawnerSettings;
        }

        private void OnEnable()
        {
            if (deathZone != null)
            {
                deathZone.FigureDespawned += OnFigureDespawned;
            }

            if (dropSlots != null && dropSlots.Length > 0)
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

            if (dropSlots != null && dropSlots.Length > 0)
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

            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }

            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }

        private void Spawn(FigureType figureType, Vector3 position)
        {
            var figure = _figureFactory.CreateFigure(figureType);
            figure.gameObject.SetActive(true);
            float speed = GetRandomSpeed();
            figure.OnSpawn(position, speed); // TODO: избавиться от speed здесь
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
                float timeout = GetRandomTimeout();
                yield return new WaitForSeconds(timeout); // TODO: можно сделать настраиваемым частоту спавна
            }
        }

        private void SpawnRandomFigure()
        {
            int randomFigureIndex = GetRandomFigureIndex();
            var randomLineIndex = GetRandomLineIndex();
            Spawn(_figuresList.Figures[randomFigureIndex], spawnPoints[randomLineIndex].position);
        }

        private float GetRandomTimeout()
        {
            return Random.Range(_spawnerSettings.MinSpawnTimeout, _spawnerSettings.MaxSpawnTimeout);
        }

        private float GetRandomSpeed()
        {
            return Random.Range(_spawnerSettings.MinSpeed, _spawnerSettings.MaxSpeed);
        }

        private int GetRandomFigureIndex()
        {
            return Random.Range(0, _figuresList.Figures.Count);
        }

        private int GetRandomLineIndex()
        {
            return Random.Range(0, spawnPoints.Length);
        }
    }
}