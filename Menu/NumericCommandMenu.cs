using System;
using System.Collections.Generic;
using System.Linq;
using SimpleQuests.Commands;
using SimpleQuests.Localization;

namespace SimpleQuests.Menu
{
    public abstract class NumericCommandMenu : MenuBase
    {
        protected HashSet<NumericCommand> commands = new HashSet<NumericCommand>();

        public override void Print()
        {
            Refresh();

            ExecuteCommand(GetInput());
        }

        protected abstract void Display();

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

                Console.WriteLine(LocalizationService.GetStringWithParam("CommandNotFound", commandNumber));

                ExecuteCommand(GetInput());
            }
        }
    }
}