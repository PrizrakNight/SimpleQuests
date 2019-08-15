using System;
using System.Linq;
using SimpleQuests.Quests.Specific;
using SimpleQuests.Rewards;
using SimpleQuests.Rewards.Specific;

namespace SimpleQuests.Quests.Generator.Specific
{
    public class WordQuestGenerator : IQuestGenerator
    {
        public const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_- ";

        public IQuest GenerateQuest()
        {
            string currentWord = GetRandomWord();

            double seconds = currentWord.Length * 1.5d; //Math.Round((double) currentWord.Length / 2, 1); //This formula gives the impossible amount of time.

            return new WordQuest(currentWord) {Expire = seconds, Rewards = GetRewards(currentWord.Length)};
        }

        private string GetRandomWord() => new string(Enumerable.Repeat(Chars, Program.Random.Next(4, 19))
            .Select(s => s[Program.Random.Next(s.Length)]).ToArray());

        private IReward[] GetRewards(int length)
        {
            if (length >= 9)
                return new IReward[]
                {
                    new GoldsReward((byte) (length / 4)),
                    new DiamondsReward((byte) Math.Floor((decimal) length / 9)),
                };

            return new IReward[]
            {
                new GoldsReward((byte) (length / 4))
            };
        }
    }
}