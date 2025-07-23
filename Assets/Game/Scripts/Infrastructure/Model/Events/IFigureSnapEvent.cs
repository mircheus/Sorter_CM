using Game.Scripts.Infrastructure;

namespace Game.Scripts.Model
{
    public interface IFigureSnapEvent : IGlobalSubscriber
    {
        void SnapFigureCorrectly(bool isCorrect);
    }
}