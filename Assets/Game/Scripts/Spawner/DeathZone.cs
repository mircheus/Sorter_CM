using System;
using Game.Scripts.Events;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Infrastructure;
using UnityEngine;

namespace Game.Scripts.FigureFactory
{
    [RequireComponent(typeof(Collider2D))]
    public class DeathZone : MonoBehaviour
    {
        [SerializeField] private ParticleSystem explosionFx; 
        public event Action<Figure> FigureDespawned;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Figure figure))
            {
                explosionFx.Play();
                FigureDespawned?.Invoke(figure);
                EventBus.RaiseEvent<IFigureExplodeEvent>(model => model.FigureExploded());
            }
        }
    }
}