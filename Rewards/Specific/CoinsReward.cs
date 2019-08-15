using System;
using SimpleQuests.Localization;

namespace SimpleQuests.Rewards.Specific
{
    [Serializable]
    public class CoinsReward : RewardBase
    {
        public readonly int Count;

        public CoinsReward(int count) => Count = count;

        public override string ToString() => $"{Count} {LocalizationService.CurrentReader["RewardCoins"]}";

        protected override void GiveReward() => Profile.Current.Account.Coins += Count;
    }
}