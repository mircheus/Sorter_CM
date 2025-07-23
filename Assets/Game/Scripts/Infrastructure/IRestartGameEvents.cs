namespace Game.Scripts.Infrastructure
{
    internal interface IRestartGameEvents : IGlobalSubscriber
    {
        void RestartGame();
    }
}