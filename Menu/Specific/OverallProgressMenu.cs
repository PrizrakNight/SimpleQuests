using System;
using SimpleQuests.Localization;

namespace SimpleQuests.Menu.Specific
{
    public class OverallProgressMenu : SubMenu<MainMenu>
    {
        protected override void Display()
        {
            Console.WriteLine($"-=-=-=-=|{LocalizationService.CurrentReader["OverallProgress"]}|=-=-=-=-");

            Console.WriteLine(
                LocalizationService.GetStringWithParam("CompletedQuests", Profile.Current.CompletedQuests.Count));

            Console.WriteLine(
                LocalizationService.GetStringWithParam("FailedQuests", Profile.Current.FailedQuests.Count));

            Console.WriteLine(
                LocalizationService.GetStringWithParam("TakendQuests", Profile.Current.TakenQuests.Count));

            Console.WriteLine(LocalizationService.GetStringWithParam("EarnedCoins", Profile.Current.Account.Coins));

            Console.WriteLine(LocalizationService.GetStringWithParam("EarnedGolds", Profile.Current.Account.Golds));

            Console.WriteLine(
                LocalizationService.GetStringWithParam("EarnedDiamonds", Profile.Current.Account.Diamonds));
        }
    }
}