using System.Collections.Generic;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.FigureFactory
{
    public class FigureFactory
    {
        private List<FigureType> _figureTypes;
        private ObjectPool<Figure> _figurePool;
        
        public FigureFactory(FiguresList figuresList, ObjectPool<Figure> figurePool)
        {
            _figureTypes = new List<FigureType>(figuresList.Figures);
            _figurePool = figurePool;
        }
        
        public Figure CreateFigure(FigureType figureType)
        {
            if (_figureTypes.Contains(figureType) == false)
            {
                Debug.LogError($"FigureType - {figureType} is not presented in Dictionary.");
            }
            
            var figure = _figurePool.Get();
            figure.Initialize(figureType);
            return figure;
        }
        
        public void ReturnFigureToPool(Figure figure)
        {
            _figurePool.ReturnToPool(figure);
        }
    }
}