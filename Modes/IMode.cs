namespace SimpleQuests.Modes
{
    public interface IMode
    {
        bool IsLaunched { get; }

        void Launch();

        void Stop();
    }
}