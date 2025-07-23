using Game.Scripts.Infrastructure;

namespace Game.Scripts.Model
{
    public interface IUpdateUIEvents : IGlobalSubscriber
    {
        void UpdateHealth(int health);
        void UpdateScore(int score);
    }
}