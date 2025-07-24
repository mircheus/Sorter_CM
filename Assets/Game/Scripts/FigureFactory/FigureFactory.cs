using System.Collections.Generic;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Utilities;

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