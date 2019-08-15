using System;
using System.Threading;
using SimpleQuests.Commands;
using SimpleQuests.Localization;
using SimpleQuests.Quests;
using SimpleQuests.Quests.Generator.Specific;

namespace SimpleQuests.Menu.Specific
{
    public class MainMenu : NumericCommandMenu
    {
        public MainMenu()
        {
            commands.Add(new NumericCommand(1, "ListOfAvailableQuests", OpenAvailableQuestMenu));
            commands.Add(new NumericCommand(2, "ListOfQuestsTaken", OpenTakenQuestMenu));
            commands.Add(new NumericCommand(3, "ListOfCompletedQuests", OpenCompletedQuestMenu));
            commands.Add(new NumericCommand(4, "ListOfFailedQuests", OpenFailedQuestMenu));
            commands.Add(new NumericCommand(5, "OverallProgress", OpenProgressMenu));
            commands.Add(new NumericCommand(6, "SaveProfile", SaveProfile));
        }

        protected override void Display() =>
            Console.WriteLine($"-=-=-=-=-=|{LocalizationService.CurrentReader["MainMenu"]}|=-=-=-=-");

        private void OpenFailedQuestMenu() => new FailedQuestsMenu().Print();

        private void OpenAvailableQuestMenu() => new AvailableQuestMenu(new QuestContainer(new QuestMixGenerator(), 7)).Print();

        private void OpenTakenQuestMenu() => new TakenQuestsMenu().Print();

        private void OpenCompletedQuestMenu() => new CompletedQuestsMenu().Print();

        private void OpenProgressMenu() => new OverallProgressMenu().Print();

        private void SaveProfile()
        {
            try
            {
                Console.WriteLine(LocalizationService.CurrentReader["SavingProfile"]);

                Profile.Save();

                Console.WriteLine(LocalizationService.CurrentReader["SavingProfileCompleted"]
                    .Replace("{0}", Profile.Current.Username));
            }
            catch(Exception exception)
            {
                Console.WriteLine(LocalizationService.CurrentReader["SavingFailed"].Replace("{0}", exception.Message));
            }

            Thread.Sleep(1000);

            this.UpdatePrint();
        }
    }
}