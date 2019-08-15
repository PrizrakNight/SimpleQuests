using System;
using System.Runtime.Serialization;

namespace SimpleQuests
{
    [DataContract, Serializable]
    public class Account
    {
        [DataMember]
        public int Coins;

        [DataMember]
        public byte Golds;

        [DataMember]
        public byte Diamonds;
    }
}