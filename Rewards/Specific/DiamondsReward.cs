using System;
using SimpleQuests.Localization;

namespace SimpleQuests.Rewards.Specific
{
    [Serializable]
    public class DiamondsReward : ByteReward
    {
        public DiamondsReward(byte count) : base(count) { }

        public override string ToString() => $"{Count} {LocalizationService.CurrentReader["RewardDiamonds"]}";

        protected override void GiveReward() => ByteCalculator(ref Profile.Current.Account.Diamonds);
    }
}