using System;
using System.IO;
using System.Linq;
using System.Threading;
using SimpleQuests.Commands;
using SimpleQuests.Localization;
using SimpleQuests.Modes;

namespace SimpleQuests.Menu.Specific
{
    public class ChangeLanguageMenu<TOwnerMenu> : SubMenu<TOwnerMenu> where TOwnerMenu : MenuBase, new()
    {
        private string[] _languagePaths;

        public ChangeLanguageMenu() => commands.Add(new NumericCommand(1, "SelectLanguage", SelectLanguage));

        protected override void Display()
        {
            Console.WriteLine($"-=-=-=-=|{LocalizationService.CurrentReader["SelectLanguage"]}|=-=-=-=-");

            LoadLanguagePaths();

            PrintLanguagePaths();
        }

        private void SelectLanguage()
        {
            SelectionMode<string> selectionMode = new SelectionMode<string>(_languagePaths);

            selectionMode.OnLaunch += () =>
            {
                Console.WriteLine(LocalizationService.CurrentReader["SelectionLanguageMode"]);
                Console.WriteLine(LocalizationService.GetStringWithParam("CommandQuitSelectionLanguageMode", 0));
            };

            selectionMode.OnValid += path =>
            {
                ILocalizationReader reader =
                                LocalizationService.CreateReader<XmlLocalizationReader>(path);

                LocalizationService.SetReader(reader);

                Console.WriteLine(LocalizationService.GetStringWithParam("SelectedLanguage",
                    Path.GetFileNameWithoutExtension(path)));

                selectionMode.Stop();
            };

            selectionMode.OnIndexOut += index =>
                Console.WriteLine(LocalizationService.GetStringWithParam("LanguageWithIndexNotExists", index));

            selectionMode.OnError += exception =>
                Console.WriteLine(LocalizationService.GetStringWithParam("FailedBySelectLanguage", exception.Message));

            selectionMode.OnStop += ShowMenu;

            selectionMode.Launch();
        }

        private void LoadLanguagePaths()
        {
            _languagePaths = Directory.GetFiles("Translations");

            _languagePaths = _languagePaths.Where(path => path.Contains(".lang")).ToArray();
        }

        private void PrintLanguagePaths()
        {
            if (_languagePaths != default && _languagePaths.Length > 0)
            {
                for (int i = 0; i < _languagePaths.Length; i++)
                    Console.WriteLine($"[{LocalizationService.GetStringWithParam("IndexName", i + 1)}]: " +
                                      Path.GetFileNameWithoutExtension(_languagePaths[i]));
            }
            else Console.WriteLine(LocalizationService.CurrentReader["NoLanguages"]);
        }

        private void ShowMenu()
        {
            Thread.Sleep(1000);

            this.UpdatePrint();
        }
    }
}