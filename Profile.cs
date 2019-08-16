using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SimpleQuests.Quests;

namespace SimpleQuests
{
    [Serializable]
    public class Profile
    {
        public readonly string Username;

        public readonly DateTime Created;

        public readonly Account Account = new Account();

        public readonly List<IQuest> TakenQuests = new List<IQuest>();

        public readonly List<IQuest> CompletedQuests = new List<IQuest>();

        public readonly List<IQuest> FailedQuests = new List<IQuest>();

        public static Profile Current { get; private set; }

        public List<IQuest> MixQuests
        {
            get
            {
                List<IQuest> result = new List<IQuest>(TakenQuests);

                result.AddRange(CompletedQuests);
                result.AddRange(FailedQuests);

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

        public static void Save()
        {
            using (FileStream stream = new FileStream($"Profiles/{Current.Username}.sdat", FileMode.Create))
            {
                BinaryFormatter binary = new BinaryFormatter();

                binary.Serialize(stream, Current);
            }
        }

        public static void Load(string username)
        {
            using (FileStream stream = new FileStream($"Profiles/{username}.sdat", FileMode.Open))
            {
                BinaryFormatter binary = new BinaryFormatter();

                Current = (Profile) binary.Deserialize(stream);
            }
        }
    }
}