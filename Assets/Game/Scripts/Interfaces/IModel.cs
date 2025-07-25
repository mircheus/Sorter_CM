namespace Game.Scripts.Infrastructure
{
    public interface IModel
    {
        public int Health { get; }
        public int Score { get; }
        public int FiguresCount { get; }
    }
}