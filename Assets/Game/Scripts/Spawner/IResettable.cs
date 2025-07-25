namespace Game.Scripts.Spawner
{
    public interface IResettable : IGlobalSubscriber
    {
        void ResetState();
    }
}