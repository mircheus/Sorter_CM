namespace Game.Scripts.Infrastructure.Model
{
    public interface IEndGameEvents : IGlobalSubscriber
    {
        void OnGameWin(int score);
        void OnGameOver();
    }
}