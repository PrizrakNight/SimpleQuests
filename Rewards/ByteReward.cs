using System;
using System.Runtime.Serialization;

namespace SimpleQuests.Rewards
{
    [DataContract, Serializable]
    public abstract class ByteReward : RewardBase
    {
        [DataMember]
        public readonly byte Count;

        public ByteReward(byte count) => Count = count;

        protected void ByteCalculator(ref byte origin)
        {
            int exp = origin + Count;

            if (exp >= byte.MaxValue) origin = byte.MaxValue;
            else origin = (byte) exp;
        }
    }
}