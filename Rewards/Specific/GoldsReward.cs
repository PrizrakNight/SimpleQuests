using System;
using System.Runtime.Serialization;
using SimpleQuests.Localization;

namespace SimpleQuests.Rewards.Specific
{
    [DataContract, Serializable]
    public class GoldsReward : ByteReward
    {
        public GoldsReward(byte count) : base(count) { }

        public override string ToString() => $"{Count} {LocalizationService.CurrentReader["RewardGolds"]}";

        protected override void GiveReward() => ByteCalculator(ref Profile.Current.Account.Golds);
    }
}