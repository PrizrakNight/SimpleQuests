using System;
using System.Collections.Generic;
using System.Threading;
using SimpleQuests.Commands;
using SimpleQuests.Localization;
using SimpleQuests.Quests;

namespace SimpleQuests.Menu.Specific
{
    public class AvailableQuestMenu : SubMenu<MainMenu>
    {
        private readonly IQuestContainer _questContainer;

        public AvailableQuestMenu(IQuestContainer questContainer) => _questContainer = questContainer;

        protected override void Display()
        {
            Console.WriteLine($"-=-=-=-=-=|{LocalizationService.CurrentReader["ListOfAvailableQuests"]}|=-=-=-=-");

            _questContainer.Print();
        }

        protected override IEnumerable<NumericCommand> LoadCommands() => new[]
        {
            new NumericCommand(1, "TakeTheQuest", TakeQuest), 
        };

        private void TakeQuest()
        {
            PrintMessage();

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    if (index == 0) break;

                    if (_questContainer.TakeQuest(index - 1))
                    {
                        Refresh();
                        PrintMessage();
                    }
                }
                else Console.WriteLine(LocalizationService.CurrentReader["InvalidInputData"]);
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

            Thread.Sleep(1000);

            this.UpdatePrint();
        }
    }
}