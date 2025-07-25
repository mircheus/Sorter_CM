namespace Game.Scripts.Events
{
    public interface IFigureExplodeEvent : IGlobalSubscriber
    {
        void FigureExploded();
    }
}