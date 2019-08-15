using System;
using System.Collections.Generic;
using SimpleQuests.Commands;
using SimpleQuests.Localization;
using SimpleQuests.Quests;
using SimpleQuests.Quests.Generator;

namespace SimpleQuests
{
    public static class BasicEx
    {
        public static string ToCommandList(this IEnumerable<NumericCommand> numericCommands, string pattern = "{subject}) {description}")
        {
            string result = string.Empty;

            foreach (NumericCommand command in numericCommands)
                result += Environment.NewLine +
                          pattern
                              .Replace("{subject}", command.Subject.ToString())
                              .Replace("{description}", LocalizationService.CurrentReader[command.Description]);

            return result;
        }

        public static IQuest[] GenerateQuests(this IQuestGenerator questGenerator, int count)
        {
            IQuest[] result = new IQuest[count];

            for (int i = 0; i < count; i++) result[i] = questGenerator.GenerateQuest();

            return result;
        }

        public static void UpdatePrint(this IPrintable printable)
        {
            Console.Clear();

            printable.Print();
        }

        public static void Supplement(this IQuestContainer questContainer, int count)
        {
            IQuest[] quests = questContainer.QuestGenerator.GenerateQuests(count);

            for (int i = 0; i < quests.Length; i++) questContainer.AvailableQuests.Add(quests[i]);
        }

        public static bool HasRewards(this IQuest quest) => quest.Rewards != default && quest.Rewards.Length > 0;

        public static void PrintInfo(this IQuest quest, string arg)
        {
            arg = !string.IsNullOrEmpty(arg) ? $"|{arg}" : string.Empty;

            Console.WriteLine(
                $"-=-=-=-=|{LocalizationService.CurrentReader[quest.Name]}|{quest.GetState()}{arg}|=-=-=-=-");

            quest.DisplayInfo();
            quest.PrintRewards();
        }

        public static void PrintRewards(this IQuest quest)
        {
            Console.WriteLine(LocalizationService.CurrentReader["Rewards"]);

            if (quest.HasRewards())
            {
                for (int i = 0; i < quest.Rewards.Length; i++) Console.WriteLine(" - " + quest.Rewards[i]);
            }
            else Console.WriteLine(LocalizationService.CurrentReader["NoRewards"]);
        }

        public static void PrintQuestInfos(this IEnumerable<IQuest> quests, int startIndex = 1)
        {
            int index = 1;

            foreach (IQuest quest in quests)
            {
                Console.WriteLine();
                quest.PrintInfo(LocalizationService.CurrentReader["IndexName"].Replace("{0}", index++.ToString()));
            }
        }

        public static string GetState(this IQuest quest)
        {
            switch (quest.State)
            {
                case QuestState.Taken: return LocalizationService.CurrentReader["QuestStateTaken"];
                case QuestState.Completed: return LocalizationService.CurrentReader["QuestStateCompleted"];
                case QuestState.Failed: return LocalizationService.CurrentReader["QuestStateFailed"];
                case QuestState.InProgress: return LocalizationService.CurrentReader["QuestStateInProgress"];
                case QuestState.Available: return LocalizationService.CurrentReader["QuestStateAvailable"];

                default: return LocalizationService.CurrentReader["QuestStateUndefined"];
            }
        }
    }
}