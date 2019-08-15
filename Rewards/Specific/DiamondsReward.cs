using System;
using SimpleQuests.Localization;
using System.Runtime.Serialization;

namespace SimpleQuests.Rewards.Specific
{
    [DataContract, Serializable]
    public class DiamondsReward : ByteReward
    {
        public DiamondsReward(byte count) : base(count) { }

        public override string ToString() => $"{Count} {LocalizationService.CurrentReader["RewardDiamonds"]}";

        protected override void GiveReward() => ByteCalculator(ref Profile.Current.Account.Diamonds);
    }
}