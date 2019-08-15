namespace SimpleQuests.Quests.Generator.Specific
{
    public class QuestMixGenerator : IQuestGenerator
    {
        private readonly IQuestGenerator[] _questGenerators = new IQuestGenerator[]
        {
            new KeyDownQuestGenerator(), 
            new WordQuestGenerator(), 
        };

        public IQuest GenerateQuest() => _questGenerators[Program.Random.Next(_questGenerators.Length)].GenerateQuest();
    }
}