using System.Collections.Generic;
using SimpleQuests.Quests.Generator;

namespace SimpleQuests.Quests
{
    public interface IQuestContainer : IPrintable
    {
        IQuestGenerator QuestGenerator { get; set; }

        ICollection<IQuest> AvailableQuests { get; }

        bool TakeQuest(int index);

        void Refill();
    }
}