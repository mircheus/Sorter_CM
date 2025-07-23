namespace Game.Scripts.Infrastructure.Model
{
    public interface IDecreaseFigureCountEvents : IGlobalSubscriber
    {
        void DecreaseFigureCount(int count = 1);
    }
}