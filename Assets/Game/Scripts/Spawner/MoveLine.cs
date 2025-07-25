using System;
using System.Collections.Generic;
using Game.Scripts.FigureFactory;
using Game.Scripts.FigureFactory.Figures;
using UnityEngine;

namespace Game.Scripts.Spawner
{
    public class MoveLine : MonoBehaviour, IPausable
    {
        [Header("References: ")]
        [SerializeField] private Transform startPoint;
        [SerializeField] private DeathZone deathZone;

        public Transform StartPoint => startPoint;
        public DeathZone DeathZone => deathZone;

        private List<Figure> _figures;

        public List<Figure> Figures => _figures;

        private void OnEnable()
        {
            _figures = new List<Figure>();
            EventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(this);
        }

        public void AssignFigure(Figure figure)
        {
            _figures.Add(figure);
            figure.Despawned += OnDespawnedFigure;
        }

        public void Pause()
        {
            foreach (var figure in _figures)
            {
                figure.StopMovement();
            }
        }

        public void Resume()
        {
            foreach (var figure in _figures)
            {
                figure.ResumeMovement();
            }
        }

        public void ClearAssignedFigures()
        {
            _figures.Clear();
        }

        private void OnDespawnedFigure(Figure figure)
        {
            figure.Despawned -= OnDespawnedFigure;
            _figures.Remove(figure);
        }
    }
}