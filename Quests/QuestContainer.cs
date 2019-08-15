using System;
using System.Collections.Generic;
using System.Linq;
using SimpleQuests.Localization;
using SimpleQuests.Quests.Generator;

namespace SimpleQuests.Quests
{
    [Serializable]
    public class QuestContainer : IQuestContainer
    {
        public readonly int CountQuests = 15;

        public IQuestGenerator QuestGenerator { get; set; }

        public ICollection<IQuest> AvailableQuests { get; private set; } = new HashSet<IQuest>();

        #region Constructors

        public QuestContainer(IQuestGenerator questGenerator, in int countQuests)
        {
            CountQuests = countQuests;

            QuestGenerator = questGenerator;

            Refill();
        }

        public QuestContainer(IQuestGenerator questGenerator)
        {
            QuestGenerator = questGenerator;

            Refill();
        }

        #endregion

        #region Methods_PUBLIC

        public bool TakeQuest(int index)
        {
            try
            {
                IQuest needle = AvailableQuests.ElementAt(index);

                if (needle.State != QuestState.Taken)
                {
                    Profile.Current.AddQuest(needle);

                    this.UpdatePrint();

                    return true;
                }
                else Console.WriteLine("Нельзя взять квест, который был ранее взят.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine(LocalizationService.CurrentReader["QuestIndexNotFound"]
                    .Replace("{0}", index.ToString()));
            }
            catch (Exception exception)
            {
                Console.WriteLine(LocalizationService.CurrentReader["QuestErrorByTaken"]
                    .Replace("{0}", exception.Message));
            }

            return false;
        }

        public void Print() => AvailableQuests.PrintQuestInfos();

        public void Refill()
        {
            AvailableQuests.Clear();

            IQuest[] quests = QuestGenerator.GenerateQuests(CountQuests);

            for (int i = 0; i < quests.Length; i++) AvailableQuests.Add(quests[i]);
        }

        #endregion
    }
}