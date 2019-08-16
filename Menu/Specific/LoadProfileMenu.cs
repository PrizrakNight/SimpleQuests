using System;
using System.IO;
using System.Linq;
using System.Threading;
using SimpleQuests.Commands;
using SimpleQuests.Localization;
using SimpleQuests.Modes;

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
            SelectionMode<FileInfo> selectionMode = new SelectionMode<FileInfo>(_profilePaths);

            selectionMode.OnLaunch += () =>
            {
                Console.WriteLine(LocalizationService.CurrentReader["SelectionProfileMode"]);
                Console.WriteLine(LocalizationService.GetStringWithParam("CommandQuitSelectionProfileMode", 0));
            };

            selectionMode.OnValid += fileInfo =>
            {
                Console.WriteLine(LocalizationService.CurrentReader["LoadingProfile"]);

                Profile.Load(Path.GetFileNameWithoutExtension(fileInfo.Name));

                Console.WriteLine(
                    LocalizationService.GetStringWithParam("LoadingProfileCompleted", Profile.Current.Username));

                selectionMode.Stop();
            };

            selectionMode.OnIndexOut += index =>
                Console.WriteLine(LocalizationService.GetStringWithParam("ProfileWithIndexNotExists", index));

            selectionMode.OnError += exception =>
                Console.WriteLine(LocalizationService.GetStringWithParam("LoadingFailed", exception.Message));

            selectionMode.OnStop += ShowMenu;

            selectionMode.Launch();
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