using System;
using SimpleQuests.Localization;

namespace SimpleQuests.Commands
{
    public class NumericCommand : IConsoleCommand<int>
    {
        private Action _action;

        public int Subject { get; }

        public string Description { get; }

        public NumericCommand(int subject, string description, Action action)
        {
            Subject = subject;
            Description = description;

            _action = action;
        }

        public void Append(Action action) => _action += action;

        public void Handle()
        {
            try
            {
                _action();
            }
            catch (Exception exception)
            {
                Console.WriteLine(LocalizationService.GetStringWithParam("FailedExecuteCommand", exception.Message));
            }
        }
    }
}