using System;
using System.Collections.Generic;
using SimpleQuests.Commands;
using SimpleQuests.Localization;

namespace SimpleQuests.Menu.Specific
{
    public class FailedQuestsMenu : SubMenu<MainMenu>
    {
        protected override void Display()
        {
            Console.WriteLine($"-=-=-=-=|{LocalizationService.CurrentReader["ListOfFailedQuests"]}|=-=-=-=-");

            if (Profile.Current.FailedQuests.Count > 0) Profile.Current.FailedQuests.PrintQuestInfos();
            else Console.WriteLine(LocalizationService.CurrentReader["NoFailedQuests"]);
        }

        protected override IEnumerable<NumericCommand> LoadCommands() => default;
    }
}