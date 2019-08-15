namespace SimpleQuests.Commands
{
    public interface IConsoleCommand<out TInputType>
    {
        TInputType Subject { get; }

        string Description { get; }

        void Handle();
    }
}