using System;
using System.IO;
using System.Linq;
using System.Threading;
using SimpleQuests.Commands;
using SimpleQuests.Localization;

namespace SimpleQuests.Menu.Specific
{
    public class LoadProfileMenu<TOwnerMenu> : SubMenu<TOwnerMenu> where TOwnerMenu : MenuBase, new()
    {
        private FileInfo[] _profilePaths;

        public LoadProfileMenu() => commands.Add(new NumericCommand(1, "SelectProfile", SelectProfile));

        protected override void Display()
        {
            Console.WriteLine($"-=-=-=-=|{LocalizationService.CurrentReader["LoadProfile"]}|=-=-=-=-");

            LoadProfilePaths();

            PrintProfiles();
        }

        private void SelectProfile()
        {
            Console.WriteLine(LocalizationService.CurrentReader["SelectionProfileMode"]);
            Console.WriteLine(LocalizationService.CurrentReader["CommandQuitSelectionProfileMode"].Replace("{0}", "0"));

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    if(index == 0) break;

                    try
                    {
                        Console.WriteLine(LocalizationService.CurrentReader["LoadingProfile"]);

                        Profile.Load(Path.GetFileNameWithoutExtension(_profilePaths[index - 1].Name));

                        Console.WriteLine(LocalizationService.CurrentReader["LoadingProfileCompleted"]
                            .Replace("{0}", Profile.Current.Username));

                        break;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine(LocalizationService.CurrentReader["ProfileWithIndexNotExists"]
                            .Replace("{0}", index.ToString()));
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(LocalizationService.CurrentReader["LoadingFailed"]
                            .Replace("{0}", exception.Message));
                    }
                }
                else Console.WriteLine(LocalizationService.CurrentReader["InvalidInputData"]);
            }

            ShowMenu();
        }

        private void LoadProfilePaths()
        {
            DirectoryInfo directory = new DirectoryInfo("Profiles");

            _profilePaths = directory.GetFiles();

            _profilePaths = _profilePaths.Where(profile => profile.Extension == ".sdat").ToArray();
        }

        private void PrintProfiles()
        {
            if (_profilePaths == default || _profilePaths.Length == 0)
                Console.WriteLine(LocalizationService.CurrentReader["NoProfilesForLoading"]);

            else
            {
                for (int i = 0; i < _profilePaths.Length; i++)
                {
                    FileInfo currentProfile = _profilePaths[i];

                    Console.WriteLine(
                        $"[{i + 1}]: {Path.GetFileNameWithoutExtension(currentProfile.Name)} - {currentProfile.LastWriteTime.ToString("dd.MM.yyyy HH:mm:ss")}");
                }
            }
        }

        private void ShowMenu()
        {
            Thread.Sleep(1000);

            this.UpdatePrint();
        }
    }
}