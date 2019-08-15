using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using SimpleQuests.Localization;
using SimpleQuests.Menu.Specific;
using SimpleQuests.Rewards;

namespace SimpleQuests.Quests
{
    [DataContract, Serializable]
    public abstract class Quest<TValue> : IQuest, IEquatable<Quest<TValue>>
    {
        #region Properties

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public QuestState State { get; set; } = QuestState.Available;

        [DataMember]
        public IReward[] Rewards { get; set; }

        public bool IsStarted => State == QuestState.InProgress;

        public bool HasRewards => Rewards != default && Rewards.Length > 0;

        protected string LocalizedName => LocalizationService.CurrentReader[Name];

        protected string LocalizedDescription => LocalizationService.CurrentReader[Description];

        protected bool IsDone => State == QuestState.Completed;

        protected bool IsFailed => State == QuestState.Failed;

        #endregion

        #region Fields

        [DataMember]
        protected TValue value;

        [DataMember]
        protected TValue current;

        #endregion

        public Quest(TValue value) => this.value = value;

        #region Methods_PUBLIC

        public abstract void DisplayInfo();
            
        public void Start()
        {
            if (IsStarted)
                throw new InvalidOperationException(LocalizationService.CurrentReader["CannotRestartQuest"]);

            Console.WriteLine(LocalizationService.CurrentReader["QuestStarted"].Replace("{0}", LocalizedName));

            Task.Factory.StartNew(UpdaterInfo);

            State = QuestState.InProgress;

            OnStarted();
        }

        public void Stop()
        {
            if (!IsStarted)
                throw new InvalidOperationException(LocalizationService.CurrentReader["CanStoppedAgainQuest"]);

            Console.WriteLine(LocalizationService.CurrentReader["QuestStopped"].Replace("{0}", LocalizedName));

            State = QuestState.Failed;

            MigrateQuest();

            ShowMenu();
        }

        public void AdjustProgress()
        {
            if (IsStarted) Adjust();
        }

        public void GiveOutRewards()
        {
            if (!HasRewards)
                throw new InvalidOperationException(LocalizationService.CurrentReader["QuestNotContainsRewards"]
                    .Replace("{0}", LocalizedName));

            for (int i = 0; i < Rewards.Length; i++) Rewards[i].GiveOut();
        }

        public bool Equals(Quest<TValue> other) => other != null && other.Name == Name
                                                                 && other.Description == Description;

        #endregion

        #region Methods_PROTECTED

        protected abstract void Adjust();

        protected abstract void OnStarted();

        protected void UpdateInfo()
        {
            Console.Clear();

            DisplayInfo();
        }

        protected void SetCompleted()
        {
            Console.WriteLine(LocalizationService.CurrentReader["QuestCompleted"].Replace("{0}", LocalizedName));

            State = QuestState.Completed;

            if (HasRewards)
            {
                GiveOutRewards();
                Console.WriteLine(LocalizationService.CurrentReader["TakedReward"]);
            }

            MigrateQuest();

            ShowMenu();
        }

        #endregion

        #region Methods_PRIVATE

        private void MigrateQuest()
        {
            if (IsStarted) return;

            if (Profile.Current == default)
                throw new NullReferenceException(LocalizationService.CurrentReader["CurrentProfileIsNull"]);

            if (!Profile.Current.TakenQuests.Contains(this))
                throw new InvalidOperationException(LocalizationService.CurrentReader["QuestNotExistsInTaken"]
                    .Replace("{0}", LocalizedName));

            Profile.Current.TakenQuests.Remove(this);

            if (State == QuestState.Failed) Profile.Current.FailedQuests.Add(this);
            else if (State == QuestState.Completed) Profile.Current.CompletedQuests.Add(this);
        }

        private async void UpdaterInfo()
        {
            while (IsStarted)
            {
                UpdateInfo();

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        private void ShowMenu()
        {
            Thread.Sleep(1000);

            new TakenQuestsMenu().Print();
        }

        #endregion
    }
}