using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SimpleQuests.Quests;

namespace SimpleQuests
{
    [DataContract, Serializable]
    public class Profile
    {
        [DataMember]
        public readonly string Username;

        [DataMember]
        public readonly DateTime Created;

        [DataMember]
        public readonly Account Account = new Account();

        [DataMember]
        public readonly List<IQuest> TakenQuests = new List<IQuest>();

        [DataMember]
        public readonly List<IQuest> CompletedQuests = new List<IQuest>();

        [DataMember]
        public readonly List<IQuest> FailedQuests = new List<IQuest>();

        public static Profile Current { get; private set; }

        public List<IQuest> MixQuests
        {
            get
            {
                List<IQuest> result = new List<IQuest>(TakenQuests);

                result.AddRange(CompletedQuests);

                return result;
            }
        }

        private Profile(string username)
        {
            Username = username;

            Created = DateTime.Now;
        }

        public void AddQuest(IQuest quest)
        {
            TakenQuests.Add(quest);

            quest.State = QuestState.Taken;
        }

        public void AddQuests(IEnumerable<IQuest> quests)
        {
            foreach (IQuest quest in quests) AddQuest(quest);
        }

        public static void CreateNew(string username) => Current = new Profile(username);

        public static void Save(bool dataContract = false)
        {
            using (FileStream stream = new FileStream($"Profiles/{Current.Username}.sdat", FileMode.Create))
            {
                if (dataContract)
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Profile));

                    serializer.WriteObject(stream, Current);
                }
                else
                {
                    BinaryFormatter binary = new BinaryFormatter();

                    binary.Serialize(stream, Current);
                }
            }
        }

        public static void Load(string username, bool dataContract = false)
        {
            using (FileStream stream = new FileStream($"Profiles/{username}.sdat", FileMode.Open))
            {
                Profile profile;

                if (dataContract)
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Profile));

                    profile = (Profile) serializer.ReadObject(stream);
                }
                else
                {
                    BinaryFormatter binary = new BinaryFormatter();

                    profile = (Profile) binary.Deserialize(stream);
                }

                Current = profile;
            }
        }
    }
}