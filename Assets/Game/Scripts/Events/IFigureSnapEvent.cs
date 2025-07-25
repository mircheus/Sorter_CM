namespace Game.Scripts.Events
{
    public interface IFigureSnapEvent : IGlobalSubscriber
    {
        void SnapFigure(bool isCorrect);
    }
}