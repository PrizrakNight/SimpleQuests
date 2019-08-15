using System;

namespace SimpleQuests.Rewards
{
    [Serializable]
    public abstract class ByteReward : RewardBase
    {
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