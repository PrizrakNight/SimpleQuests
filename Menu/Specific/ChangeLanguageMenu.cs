using System;
using System.IO;
using System.Linq;
using System.Threading;
using SimpleQuests.Commands;
using SimpleQuests.Localization;

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
            Console.WriteLine(LocalizationService.CurrentReader["SelectionLanguageMode"]);
            Console.WriteLine(LocalizationService.CurrentReader["CommandQuitSelectionLanguageMode"].Replace("{0}", "0"));

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    try
                    {
                        string path = _languagePaths[index - 1];

                        ILocalizationReader reader =
                            LocalizationService.CreateReader<XmlLocalizationReader>(path);

                        LocalizationService.SetReader(reader);

                        Console.WriteLine(LocalizationService.CurrentReader["SelectedLanguage"]
                            .Replace("{0}", Path.GetFileNameWithoutExtension(path)));

                        break;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine(LocalizationService.CurrentReader["LanguageWithIndexNotExists"]
                            .Replace("{0}", index.ToString()));
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(LocalizationService.CurrentReader["FailedBySelectLanguage"]
                            .Replace("{0}", exception.Message));
                    }
                }
                else Console.WriteLine(LocalizationService.CurrentReader["InvalidInputData"]);
            }

            ShowMenu();
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
                    Console.WriteLine(
                        $"[{LocalizationService.CurrentReader["IndexName"].Replace("{0}", (i + 1).ToString())}]: " +
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