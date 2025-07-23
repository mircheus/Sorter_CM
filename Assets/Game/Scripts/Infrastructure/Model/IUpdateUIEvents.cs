namespace Game.Scripts.Infrastructure.Model
{
    public interface IUpdateUIEvents : IGlobalSubscriber
    {
        void UpdateHealth(int health);
        void UpdateScore(int score);
    }
}