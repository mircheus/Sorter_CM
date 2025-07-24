namespace Game.Scripts.Model
{
    public interface IEndGameEvents : IGlobalSubscriber
    {
        void OnGameWin(int score);
        void OnGameLoose();
    }
    
    public interface IEndGameEventsPublisher
    {
        void PublishGameWin(int score);
        void PublishGameLoose();
    }
}