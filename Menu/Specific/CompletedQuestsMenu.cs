using System;
using SimpleQuests.Localization;

namespace SimpleQuests.Menu.Specific
{
    public class CompletedQuestsMenu : SubMenu<MainMenu>
    {
        protected override void Display()
        {
            Console.WriteLine($"-=-=-=-=|{LocalizationService.CurrentReader["ListOfCompletedQuests"]}|=-=-=-=-");

            if (Profile.Current.CompletedQuests.Count > 0) Profile.Current.CompletedQuests.PrintQuestInfos();
            else Console.WriteLine(LocalizationService.CurrentReader["NoCompletedQuests"]);
        }
    }
}