namespace Game.Scripts.Events
{
    public interface IUpdateUIEvents : IGlobalSubscriber
    {
        void UpdateHealth(int health);
        void UpdateScore(int score);
        void UpdateFiguresCount(int figuresCount);
    }
}