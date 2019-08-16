using System;
using System.Linq;
using System.Threading;
using SimpleQuests.Commands;
using SimpleQuests.Localization;
using SimpleQuests.Modes;
using SimpleQuests.Quests;

namespace SimpleQuests.Menu.Specific
{
    public class AvailableQuestMenu : SubMenu<MainMenu>
    {
        private readonly IQuestContainer _questContainer;

        public AvailableQuestMenu(IQuestContainer questContainer)
        {
            commands.Add(new NumericCommand(1, "TakeTheQuest", TakeQuest));

            _questContainer = questContainer;
        }

        protected override void Display()
        {
            Console.WriteLine($"-=-=-=-=-=|{LocalizationService.CurrentReader["ListOfAvailableQuests"]}|=-=-=-=-");

            _questContainer.Print();
        }

        private void TakeQuest()
        {
            SelectionMode<IQuest> selectionMode = new SelectionMode<IQuest>(_questContainer.AvailableQuests.ToArray());

            selectionMode.OnLaunch += PrintMessage;
            selectionMode.OnStop += CloseSelectMode;

            selectionMode.OnIndexOut += index =>
                Console.WriteLine(LocalizationService.GetStringWithParam("QuestIndexNotFound", index));

            selectionMode.OnError += exception =>
                Console.WriteLine(LocalizationService.GetStringWithParam("QuestErrorByTaken", exception.Message));

            selectionMode.OnValid += quest =>
            {
                if (quest.State != QuestState.Taken)
                {
                    Profile.Current.AddQuest(quest);

                    Refresh();
                    PrintMessage();
                }
                else Console.WriteLine(LocalizationService.CurrentReader["QuesAlreadyTaken"]);
            };

            selectionMode.Launch();
        }

        private void PrintMessage()
        {
            Console.WriteLine(LocalizationService.CurrentReader["StartSelectionMode"]);
            Console.WriteLine(LocalizationService.GetStringWithParam("CommandQuitSelectionMode", 0));
        }

        private void CloseSelectMode()
        {
            Console.WriteLine(LocalizationService.CurrentReader["QuitSelectionMode"]);

            Thread.Sleep(1000);

            this.UpdatePrint();
        }
    }
}