using System;
using System.Collections.Generic;
using SimpleQuests.Commands;
using SimpleQuests.Localization;

namespace SimpleQuests.Menu.Specific
{
    public class OverallProgressMenu : SubMenu<MainMenu>
    {
        protected override void Display()
        {
            Console.WriteLine($"-=-=-=-=|{LocalizationService.CurrentReader["OverallProgress"]}|=-=-=-=-");

            Console.WriteLine(LocalizationService.CurrentReader["CompletedQuests"]
                .Replace("{0}", Profile.Current.CompletedQuests.Count.ToString()));

            Console.WriteLine(LocalizationService.CurrentReader["FailedQuests"]
                .Replace("{0}", Profile.Current.FailedQuests.Count.ToString()));

            Console.WriteLine(LocalizationService.CurrentReader["TakendQuests"]
                .Replace("{0}", Profile.Current.TakenQuests.Count.ToString()));

            Console.WriteLine(LocalizationService.CurrentReader["EarnedCoins"]
                .Replace("{0}", Profile.Current.Account.Coins.ToString()));

            Console.WriteLine(LocalizationService.CurrentReader["EarnedGolds"]
                .Replace("{0}", Profile.Current.Account.Golds.ToString()));

            Console.WriteLine(LocalizationService.CurrentReader["EarnedDiamonds"]
                .Replace("{0}", Profile.Current.Account.Diamonds.ToString()));
        }

        protected override IEnumerable<NumericCommand> LoadCommands() => default;
    }
}