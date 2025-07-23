using Game.Scripts.Infrastructure;

namespace Game.Scripts.Model
{
    public interface IFigureSnapEvent : IGlobalSubscriber
    {
        void SnapFigure(bool isCorrect);
    }
}