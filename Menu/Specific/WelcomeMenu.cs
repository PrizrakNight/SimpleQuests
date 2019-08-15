using System;
using System.Collections.Generic;
using System.Threading;
using SimpleQuests.Commands;
using SimpleQuests.Localization;

namespace SimpleQuests.Menu.Specific
{
    public class WelcomeMenu : NumericCommandMenu
    {
        protected override void Display()
        {
            Console.WriteLine(LocalizationService.CurrentReader["WelcomeToApp"]);
            Console.WriteLine(LocalizationService.CurrentReader["AppInfo"]);

            string username = Profile.Current != default
                ? Profile.Current.Username
                : LocalizationService.CurrentReader["ProfileNotSet"];

            Console.WriteLine(LocalizationService.CurrentReader["YoureProfile"].Replace("{0}", username));
            Console.WriteLine(LocalizationService.CurrentReader["MakeChoice"]);
        }

        protected override IEnumerable<NumericCommand> LoadCommands() => new NumericCommand[]
        {
            new NumericCommand(1, "ChangeLanguage", ChangeLanguage), 
            new NumericCommand(2, "CreateProfile", CreateProfile), 
            new NumericCommand(3, "LoadProfile", LoadProfile),
            new NumericCommand(4, "Continue", Continue), 
        };

        private void ChangeLanguage() => new ChangeLanguageMenu<WelcomeMenu>().Print();

        private void LoadProfile() => new LoadProfileMenu<WelcomeMenu>().Print();

        private void Continue()
        {
            if (Profile.Current == default)
            {
                Console.WriteLine(LocalizationService.CurrentReader["CantContinueWhileProfileNotCreatedOrLoaded"]);

                ShowMenu();
            }
            else new MainMenu().Print();
        }

        private void CreateProfile()
        {
            Console.WriteLine(LocalizationService.CurrentReader["WriteUsername"]);

            string username = Console.ReadLine();

            Profile.CreateNew(username);

            Console.WriteLine(LocalizationService.CurrentReader["ProfileWasCreated"]);

            ShowMenu();
        }

        private void ShowMenu()
        {
            Thread.Sleep(1000);

            this.UpdatePrint();
        }
    }
}