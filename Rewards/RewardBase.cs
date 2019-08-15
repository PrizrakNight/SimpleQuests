using System;
using System.Runtime.Serialization;
using SimpleQuests.Localization;

namespace SimpleQuests.Rewards
{
    [DataContract, Serializable]
    public abstract class RewardBase : IReward
    {
        [DataMember]
        public bool IsIssued { get; private set; }

        public void GiveOut()
        {
            if (IsIssued)
                throw new InvalidOperationException(LocalizationService.CurrentReader["CantGiveUpRewardAgain"]);

            if (Profile.Current == default)
                throw new NullReferenceException(LocalizationService.CurrentReader["CurrentProfileIsNull"]);

            GiveReward();

            IsIssued = true;
        }

        protected abstract void GiveReward();
    }
}