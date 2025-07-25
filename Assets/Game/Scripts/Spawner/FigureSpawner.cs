using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Events;
using Game.Scripts.FigureFactory;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Slots;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Spawner
{
    public class FigureSpawner : MonoBehaviour, IEndGameEvents, IPausable, IResettable
    {
        [Header("References: ")]
        [SerializeField] private DropSlot[] dropSlots; 
        [SerializeField] private MoveLine[] moveLines;

        private Coroutine _spawnCoroutine;
        private FiguresList _figuresList;
        private bool _isSpawning = false;
        private float _nextRandomSpeed;
        private FigureFactory.FigureFactory _figureFactory;
        private SpawnerSettings _spawnerSettings;

        [Inject]
        public void Construct(FigureFactory.FigureFactory factory, FiguresList figuresList, SpawnerSettings spawnerSettings)
        {
            _figureFactory = factory;
            _figuresList = figuresList;
            _spawnerSettings = spawnerSettings;
        }

        private void OnEnable()
        {
            foreach (var moveLine in moveLines)
            {
                moveLine.DeathZone.FigureDespawned += OnFigureDespawned;
            }

            if (dropSlots != null && dropSlots.Length > 0)
            {
                foreach (var slot in dropSlots)
                {
                    slot.FigureDespawned += OnFigureDespawned;
                }
            }
            
            EventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            foreach (var moveLine in moveLines)
            {
                moveLine.DeathZone.FigureDespawned -= OnFigureDespawned;
            }

            if (dropSlots != null && dropSlots.Length > 0)
            {
                foreach (var slot in dropSlots)
                {
                    slot.FigureDespawned -= OnFigureDespawned;
                }
            }

            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }

            _isSpawning = false; 
            EventBus.Unsubscribe(this);
        }

        private void Start()
        {
            StartSpawn();
        }
        
        public void Pause()
        {
            _isSpawning = false;
        }

        public void Resume()
        {
            _isSpawning = true;
            StartSpawn();
        }

        public void ResetState()
        {
            List<Figure> figures = new List<Figure>();

            foreach (var moveLine in moveLines)
            {
                figures.AddRange(moveLine.Figures);
                moveLine.ClearAssignedFigures();
            }

            foreach (var figure in figures)
            {
                _figureFactory.ReturnFigureToPool(figure);
            }
        }
        
        public void OnGameWin(int score)
        {
            _isSpawning = false;
        }

        public void OnGameLoose()
        {
            _isSpawning = false;
        }

        private void StartSpawn()
        {
            _isSpawning = true;
            
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }

            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }

        private void Spawn(FigureType figureType, MoveLine moveLine)
        {
            var figure = _figureFactory.CreateFigure(figureType);
            moveLine.AssignFigure(figure);
            float speed = GetRandomSpeed();
            figure.OnSpawn(moveLine.StartPoint.position, speed);
        }

        private void OnFigureDespawned(Figure figure)
        {
            _figureFactory.ReturnFigureToPool(figure);
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
            Spawn(_figuresList.Figures[randomFigureIndex], moveLines[randomLineIndex]);
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
            return Random.Range(0, moveLines.Length);
        }
    }
}