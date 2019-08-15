using System;
using System.Runtime.Serialization;
using SimpleQuests.Localization;

namespace SimpleQuests.Quests.Specific
{
    [DataContract, Serializable]
    public class WordQuest : Quest<string>, ITemporaryQuest
    {
        [DataMember]
        public double Expire { get; set; }

        private DateTime _endTime;

        public WordQuest(string value) : base(value)
        {
            Name = "WriteWordQuestName";
            Description = "WriteWordQuestDescription";
        }

        public override void DisplayInfo()
        {
            Console.WriteLine(LocalizedDescription
                .Replace("{0}", value)
                .Replace("{1}", Expire.ToString()));

            if (IsStarted)
                Console.WriteLine(LocalizationService.CurrentReader["TimeLeft"]
                    .Replace("{0}", _endTime.Subtract(DateTime.Now).Seconds.ToString()));
        }

        protected override void Adjust()
        {
            if (DateTime.Now > _endTime)
            {
                Console.WriteLine(LocalizationService.CurrentReader["TimeOutYouLose"].Replace("{0}", LocalizedName));
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