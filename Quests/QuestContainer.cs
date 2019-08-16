using System;
using System.Collections.Generic;
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