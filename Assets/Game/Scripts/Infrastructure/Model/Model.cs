using System;
using Game.Scripts.FigureFactory;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Infrastructure;

namespace Game.Scripts.Model
{
    public class Model : IModel, IFigureExplodeEvent, IFigureSnapEvent, IDisposable
    {
        private int _figuresCount;
        private int _health;
        private int _score;
        
        public int Health => _health;
        public int Score => _score;
        public int FiguresCount => _figuresCount;
        
        public Model(int figuresCount, int health, int score) 
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
        
        public void ResetModel(int figuresCount, int health, int score = 0)
        {
            Dispose(); 
            Initialize(figuresCount, health, score);
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
                EventBus.RaiseEvent<IEndGameEvents>(ui => ui.OnGameLoose());
                EventBus.RaiseEvent<IPausable>(game => game.Pause());
            }

            EventBus.RaiseEvent<IUpdateUIEvents>(ui => ui.UpdateHealth(_health));
        }

        private void DecreaseFigureCount(int count = 1)
        {
            _figuresCount -= count;
            
            if (IsFiguresCountDepleted())
            {
                EventBus.RaiseEvent<IEndGameEvents>(ui => ui.OnGameWin(_score));
                EventBus.RaiseEvent<IPausable>(game => game.Pause());
            }
            
            EventBus.RaiseEvent<IUpdateUIEvents>(ui => ui.UpdateFiguresCount(_figuresCount));
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