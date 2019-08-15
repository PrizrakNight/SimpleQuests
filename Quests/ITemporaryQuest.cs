namespace SimpleQuests.Quests
{
    public interface ITemporaryQuest : IQuest
    {
        double Expire { get; set; }
    }
}