﻿using System;
using System.Threading;
using SimpleQuests.Commands;
using SimpleQuests.Localization;

namespace SimpleQuests.Menu.Specific
{
    public class WelcomeMenu : NumericCommandMenu
    {
        public WelcomeMenu()
        {
            commands.Add(new NumericCommand(1, "ChangeLanguage", ChangeLanguage));
            commands.Add(new NumericCommand(2, "CreateProfile", CreateProfile));
            commands.Add(new NumericCommand(3, "LoadProfile", LoadProfile));
            commands.Add(new NumericCommand(4, "Continue", Continue));
        }

        protected override void Display()
        {
            Console.WriteLine(LocalizationService.CurrentReader["WelcomeToApp"]);
            Console.WriteLine(LocalizationService.CurrentReader["AppInfo"]);

            string username = Profile.Current != default
                ? Profile.Current.Username
                : LocalizationService.CurrentReader["ProfileNotSet"];

            Console.WriteLine(LocalizationService.GetStringWithParam("YoureProfile", username));
            Console.WriteLine(LocalizationService.CurrentReader["MakeChoice"]);
        }

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