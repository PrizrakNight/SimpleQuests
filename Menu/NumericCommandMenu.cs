using System;
using System.Collections.Generic;
using System.Linq;
using SimpleQuests.Commands;
using SimpleQuests.Localization;

namespace SimpleQuests.Menu
{
    public abstract class NumericCommandMenu : MenuBase
    {
        protected readonly HashSet<NumericCommand> commands;

        public NumericCommandMenu()
        {
            IEnumerable<NumericCommand> loadedCommands = LoadCommands();

            if (loadedCommands != default) commands = new HashSet<NumericCommand>(loadedCommands);
            else commands = new HashSet<NumericCommand>();
        }

        public override void Print()
        {
            Refresh();

            GetInput(out int input);

            ExecuteCommand(input);
        }

        protected abstract void Display();

        protected abstract IEnumerable<NumericCommand> LoadCommands();

        protected override void Refresh()
        {
            Console.Clear();

            Display();

            Console.WriteLine(commands.ToCommandList());
        }

        protected void ExecuteCommand(int commandNumber)
        {
            NumericCommand needle = commands.FirstOrDefault(command => command.Subject == commandNumber);

            if (needle != default)
                needle.Handle();

            else
            {
                Refresh();

                Console.WriteLine(LocalizationService.CurrentReader["CommandNotFound"]
                    .Replace("{0}", commandNumber.ToString()));

                GetInput(out commandNumber);

                ExecuteCommand(commandNumber);
            }
        }
    }
}