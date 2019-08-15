using SimpleQuests.Rewards;

namespace SimpleQuests.Quests
{
    public interface IQuest : IStartable
    {
        string Name { get; set; }

        string Description { get; set; }

        QuestState State { get; set; }

        IReward[] Rewards { get; set; }

        void AdjustProgress();

        void DisplayInfo();
    }
}