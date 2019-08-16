using System;
using System.Threading;
using SimpleQuests.Commands;
using SimpleQuests.Localization;
using SimpleQuests.Modes;
using SimpleQuests.Quests;

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
            SelectionMode<IQuest> selectionMode = new SelectionMode<IQuest>(Profile.Current.TakenQuests.ToArray());

            selectionMode.OnLaunch += PrintMessage;

            selectionMode.OnIndexOut += index =>
                Console.WriteLine(LocalizationService.GetStringWithParam("QuestIndexNotFound", index));

            selectionMode.OnValid += quest => quest.Start();

            selectionMode.OnStop += CloseSelectMode;

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

            Thread.Sleep(400);

            this.UpdatePrint();
        }
    }
}