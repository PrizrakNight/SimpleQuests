using System;
using SimpleQuests.Localization;

namespace SimpleQuests.Rewards.Specific
{
    [Serializable]
    public class GoldsReward : ByteReward
    {
        public GoldsReward(byte count) : base(count) { }

        public override string ToString() => $"{Count} {LocalizationService.CurrentReader["RewardGolds"]}";

        protected override void GiveReward() => ByteCalculator(ref Profile.Current.Account.Golds);
    }
}