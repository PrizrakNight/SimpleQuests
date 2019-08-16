using System;
using SimpleQuests.Localization;

namespace SimpleQuests.Quests.Specific
{
    [Serializable]
    public class WordQuest : Quest<string>, ITemporaryQuest
    {
        public double Expire { get; set; }

        private DateTime _endTime;

        public WordQuest(string value) : base(value)
        {
            Name = "WriteWordQuestName";
            Description = "WriteWordQuestDescription";
        }

        public override void DisplayInfo()
        {
            Console.WriteLine(LocalizationService.GetStringWithParams(Description, value, Expire));

            if (IsStarted)
                Console.WriteLine(
                    LocalizationService.GetStringWithParam("TimeLeft", _endTime.Subtract(DateTime.Now).Seconds));
        }

        protected override void Adjust()
        {
            if (DateTime.Now > _endTime)
            {
                Console.WriteLine(LocalizationService.GetStringWithParam("TimeOutYouLose", LocalizedName));
                Stop();
            }

            else if (current.Equals(value)) SetCompleted();

            else Console.WriteLine(LocalizationService.CurrentReader["WordNotEqual"]);
        }

        protected override void OnStarted()
        {
            _endTime = DateTime.Now.AddSeconds(Expire);

            while (IsStarted)
            {
                current = Console.ReadLine();

                AdjustProgress();
            }
        }
    }
}