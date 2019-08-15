using System;
using SimpleQuests.Quests.Specific;
using SimpleQuests.Rewards;
using SimpleQuests.Rewards.Specific;

namespace SimpleQuests.Quests.Generator.Specific
{
    public class KeyDownQuestGenerator : IQuestGenerator
    {
        public const double ProbabilityPercentForArrow = .25d;

        public IQuest GenerateQuest()
        {
            ConsoleKey currentKey = GetRandomKey();

            int countDowns = Program.Random.Next(1, 51);

            double seconds = Math.Round((double) countDowns / 3, 1);

            return new KeyDownQuest(countDowns, currentKey) {Expire = seconds, Rewards = GetRewards(countDowns)};
        }

        private ConsoleKey GetRandomKey()
        {
            double currentPercent = Program.Random.NextDouble();

            if (currentPercent <= ProbabilityPercentForArrow) return GetRandomArrow();

            return GetRandomSymbol();
        }

        private ConsoleKey GetRandomArrow() => (ConsoleKey) Program.Random.Next(37, 41);

        private ConsoleKey GetRandomSymbol() => (ConsoleKey) Program.Random.Next(65, 91);

        private IReward[] GetRewards(int countDowns)
        {
            if (countDowns > 20)
                return new IReward[]
                {
                    new CoinsReward(countDowns / 2),
                    new GoldsReward((byte) Math.Floor((decimal) countDowns / 20))
                };

            return new IReward[]
            {
                new CoinsReward(countDowns / 2)
            };
        }
    }
}