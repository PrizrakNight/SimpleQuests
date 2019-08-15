using System;
using System.Threading;
using SimpleQuests.Commands;
using SimpleQuests.Localization;

namespace SimpleQuests.Menu.Specific
{
    public class TakenQuestsMenu : SubMenu<MainMenu>
    {
        public TakenQuestsMenu() => commands.Add(new NumericCommand(1, "StartQuestCommand", StartQuest));

        protected override void Display()
        {
            Console.WriteLine($"-=-=-=-=|{LocalizationService.CurrentReader["ListOfQuestsTaken"]}|=-=-=-=-");

            if (Profile.Current.TakenQuests.Count > 0) Profile.Current.TakenQuests.PrintQuestInfos();
            else Console.WriteLine(LocalizationService.CurrentReader["NoTakenQuests"]);
        }

        private void StartQuest()
        {
            PrintMessage();

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    if(index == 0) break;

                    try
                    {
                        Profile.Current.TakenQuests[index - 1].Start();
                    }
                    catch
                    {
                        Console.WriteLine(LocalizationService.CurrentReader["QuestIndexNotFound"]
                            .Replace("{0}", index.ToString()));
                    }
                }
            }

            CloseSelectMode();
        }

        private void PrintMessage()
        {
            Console.WriteLine(LocalizationService.CurrentReader["StartSelectionMode"]);
            Console.WriteLine(LocalizationService.CurrentReader["CommandQuitSelectionMode"].Replace("{0}", "0"));
        }

        private void CloseSelectMode()
        {
            Console.WriteLine(LocalizationService.CurrentReader["QuitSelectionMode"]);

            Thread.Sleep(400);

            this.UpdatePrint();
        }
    }
}