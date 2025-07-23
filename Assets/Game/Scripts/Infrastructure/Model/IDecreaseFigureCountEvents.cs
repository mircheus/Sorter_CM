using Game.Scripts.Infrastructure;

namespace Game.Scripts.Model
{
    public interface IDecreaseFigureCountEvents : IGlobalSubscriber
    {
        void DecreaseFigureCount(int count = 1);
    }
}