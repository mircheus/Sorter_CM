namespace Game.Scripts.Infrastructure.Model
{
    public interface IDecreaseHealthEvents : IGlobalSubscriber
    {
        void DecreaseHealth(int amount = 1);
    }
}