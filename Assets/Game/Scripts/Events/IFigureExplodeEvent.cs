using Game.Scripts.Infrastructure;

namespace Game.Scripts.FigureFactory
{
    public interface IFigureExplodeEvent : IGlobalSubscriber
    {
        void FigureExploded();
    }
}