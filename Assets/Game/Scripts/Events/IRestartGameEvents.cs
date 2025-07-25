namespace Game.Scripts.Events
{
    internal interface IRestartGameEvents : IGlobalSubscriber
    {
        void RestartGame();
    }
}