namespace Game.Scripts.FigureFactory.Figures
{
    public interface IPausable : IGlobalSubscriber
    {
        void Pause();
        void Resume();
    }
}