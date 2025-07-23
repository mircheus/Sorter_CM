using System;
using Game.Scripts.FigureFactory;
using Game.Scripts.Infrastructure;
using UnityEngine;

namespace Game.Scripts.Model
{
    public class Model : IModel, IDecreaseHealthEvents, IDecreaseFigureCountEvents, IDisposable
    {
        private int _figuresCount;
        private int _health;

        public int FiguresCount => _figuresCount;
        public int Health => _health;
        
        // TODO: создавать Model в GameController или какой-то вышестоящей сущности
        public Model(int figuresCount, int health) // TODO: сделать в конструкторе проверку на корректность значений
        {
            Initialize(figuresCount, health);
            Debug.Log("Model Initialized");
        }

        private void Initialize(int figuresCount, int health)
        {
            _figuresCount = figuresCount;
            _health = health;
            EventBus.Subscribe(this);
        }
        
        public void Dispose()
        {
            EventBus.Unsubscribe(this);
        }
        
        public void DecreaseHealth(int amount = 1)
        {
            _health -= amount;
            EventBus.RaiseEvent<IUpdateUIEvents>(UI => UI.UpdateHealth(_health));
        }

        public void DecreaseFigureCount(int count = 1)
        {
            _figuresCount -= count;
            EventBus.RaiseEvent<IUpdateUIEvents>(ui => ui.UpdateScore(_figuresCount));
        }
    }
}