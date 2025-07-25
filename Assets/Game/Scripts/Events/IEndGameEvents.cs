namespace Game.Scripts.Events
{
    public interface IEndGameEvents : IGlobalSubscriber
    {
        void OnGameWin(int score);
        void OnGameLoose();
    }
}