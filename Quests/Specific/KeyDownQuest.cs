using System;
using SimpleQuests.Localization;

namespace SimpleQuests.Quests.Specific
{
    [Serializable]
    public class KeyDownQuest : CounterQuest, ITemporaryQuest
    {
        public int SecondsLeft => _endTime.Subtract(DateTime.Now).Seconds;

        public double Expire { get; set; }

        public readonly ConsoleKey Key;

        [NonSerialized]
        private DateTime _endTime;

        public KeyDownQuest(int value, ConsoleKey key) : base(value)
        {
            Name = "PressKeyQuestName";
            Description = "PressKeyQuestDescription";

            Key = key;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine(LocalizedDescription
                .Replace("{0}", Key.ToString())
                .Replace("{1}", value.ToString())
                .Replace("{2}", Expire.ToString()));

            if (IsStarted)
            {
                Console.WriteLine(LocalizationService.CurrentReader["RemainingClicks"]
                    .Replace("{0}", (value - current).ToString()));

                Console.WriteLine(LocalizationService.CurrentReader["TimeLeft"].Replace("{0}", SecondsLeft.ToString()));
            }
        }

        protected override void OnStarted()
        {
            current = 0;

            _endTime = DateTime.Now.AddSeconds(Expire);

            while (IsStarted)
            {
                if (Key == Console.ReadKey(true).Key) Counter++;

                AdjustProgress();
            }
        }

        protected override void Adjust()
        {
            if (DateTime.Now > _endTime)
            {
                Console.WriteLine(LocalizationService.CurrentReader["TimeOutYouLose"].Replace("{0}", LocalizedName));
                Stop();
            }
            else base.Adjust();
        }
    }
}