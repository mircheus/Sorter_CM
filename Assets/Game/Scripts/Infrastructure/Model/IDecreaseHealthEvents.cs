using Game.Scripts.Infrastructure;

namespace Game.Scripts.Model
{
    public interface IDecreaseHealthEvents : IGlobalSubscriber
    {
        void DecreaseHealth(int amount = 1);
    }
}