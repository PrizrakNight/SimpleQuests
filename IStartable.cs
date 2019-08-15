namespace SimpleQuests
{
    public interface IStartable
    {
        bool IsStarted { get; }

        void Start();

        void Stop();
    }
}