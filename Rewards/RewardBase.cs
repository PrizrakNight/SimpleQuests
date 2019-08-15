using System;
using SimpleQuests.Localization;

namespace SimpleQuests.Rewards
{
    [Serializable]
    public abstract class RewardBase : IReward
    {
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