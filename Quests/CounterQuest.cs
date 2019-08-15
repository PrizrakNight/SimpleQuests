using System;

namespace SimpleQuests.Quests
{
    [Serializable]
    public abstract class CounterQuest : Quest<int>
    {
        public int Counter
        {
            get => current;
            set => current = value;
        }

        public CounterQuest(int value) : base(value) { }

        protected override void Adjust()
        {
            if (current >= value) SetCompleted();
        }
    }
}