using System;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Infrastructure;
using Game.Scripts.Model;
using UnityEngine;

namespace Game.Scripts.FigureFactory
{
    [RequireComponent(typeof(Collider2D))]
    public class DeathZone : MonoBehaviour
    {
        public event Action<Figure> FigureDespawned;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Figure figure))
            {
                FigureDespawned?.Invoke(figure);
                EventBus.RaiseEvent<IDecreaseFigureCountEvents>(model => model.DecreaseFigureCount());
                EventBus.RaiseEvent<IDecreaseHealthEvents>(model => model.DecreaseHealth());
            }
        }
    }
}