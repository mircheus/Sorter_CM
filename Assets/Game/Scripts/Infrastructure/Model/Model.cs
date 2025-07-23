using System;
using Game.Scripts.FigureFactory;
using Game.Scripts.Infrastructure;
using Game.Scripts.Infrastructure.Model;
using UnityEngine;

namespace Game.Scripts.Model
{
    public class Model : IModel, IFigureExplodeEvent, IFigureSnapEvent, IDisposable
    {
        private int _figuresCount;
        private int _health;
        private int _score;
        
        public int Health => _health;
        public int Score => _score;

        // TODO: создавать Model в GameController или какой-то вышестоящей сущности
        public Model(int figuresCount, int health, int score) // TODO: сделать в конструкторе проверку на корректность значений
        {
            Initialize(figuresCount, health, score);
        }

        private void Initialize(int figuresCount, int health, int score = 0)
        {
            _figuresCount = figuresCount;
            _health = health;
            _score = score;
            EventBus.Subscribe(this);
        }

        public void Dispose()
        {
            EventBus.Unsubscribe(this);
        }

        public void FigureExploded()
        {
            DecreaseHealth();
            DecreaseFigureCount();
        }

        public void SnapFigure(bool isCorrect)
        {
            if (isCorrect)
            {
                IncreaseScore();
            }
            else
            {
                DecreaseHealth();
            }
            
            DecreaseFigureCount();
        }

        private void IncreaseScore(int amount = 1)
        {
            _score += amount;
            EventBus.RaiseEvent<IUpdateUIEvents>(ui => ui.UpdateScore(_score));
        }

        private void DecreaseHealth(int amount = 1)
        {
            _health -= amount;

            if (IsHealthDepleted())
            {
                EventBus.RaiseEvent<IEndGameEvents>(ui => ui.OnGameOver());
            }

            EventBus.RaiseEvent<IUpdateUIEvents>(ui => ui.UpdateHealth(_health));
        }

        private void DecreaseFigureCount(int count = 1)
        {
            _figuresCount -= count;
            
            if (IsFiguresCountDepleted())
            {
                EventBus.RaiseEvent<IEndGameEvents>(ui => ui.OnGameWin(_score));
            }
            
            EventBus.RaiseEvent<IUpdateUIEvents>(ui => ui.UpdateScore(_score));
        }

        private bool IsHealthDepleted()
        {
            return _health <= 0;
        }

        private bool IsFiguresCountDepleted()
        {
            return _figuresCount <= 0;
        }
    }
}