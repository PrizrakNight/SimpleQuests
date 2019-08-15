using System;
using SimpleQuests.Menu.Specific;

namespace SimpleQuests
{
    class Program
    {
        public static readonly Random Random = new Random();

        [STAThread]
        static void Main(string[] args)
        {
            new WelcomeMenu().Print();
            //LocalizationService.SetReader(
            //    LocalizationService.CreateReader<XmlLocalizationReader>("Translations/RU.lang"));

            //Profile.CreateNew("Adriano");

            //IQuestContainer questContainer = new QuestContainer(new QuestMixGenerator(), 3);

            //Profile.Current.TakenQuests.AddRange(questContainer.AvailableQuests);
            //Profile.Current.AddQuests(questContainer.AvailableQuests);

            //new CompletedQuestsMenu().Print();

            //questContainer.Print();

            //new TakenQuestsMenu().Print();

            //WordQuest needle = questContainer
            //    .AvailableQuests
            //    .Cast<WordQuest>()
            //    .OrderByDescending(quest => quest.Expire)
            //    .First();

            //Profile.Current.TakenQuests.Add(needle);

            //needle.Start();

            //Task current = Task.Factory.StartNew(Inputs);

            //Thread.Sleep(3000);

            //Console.WriteLine("Task canceled");

            //LocalizationService.SetReader(LocalizationService.CreateReader("Translations/RU.lang"));

            //CancellationTokenSource tokenSource = new CancellationTokenSource(10 * 1000);

            //Profile.CreateNew("Adriano");

            //IQuestContainer questContainer = new QuestContainer(new QuestMixGenerator(), 5);

            //questContainer.Print();

            //Thread.Sleep(5000);

            //questContainer.TakeQuest(0);

            //string[] aL = LocalizationService.GetAvailableLanguages();

            //for (int i = 0; i < aL.Length; i++)
            //{
            //    Console.WriteLine(Path.GetFileNameWithoutExtension(aL[i]));
            //}
            //string word = "Hello World!";

            //WordQuest wordQuest = new WordQuest(word)
            //{
            //    Expire = 5,
            //    Rewards = new IReward[]
            //    {
            //        new CoinsReward(20),
            //        new GoldsReward(1),
            //    }
            //};

            //Profile.Current.TakenQuests.Add(wordQuest);

            //wordQuest.Start();

            Console.ReadKey();
        }

        static void Inputs()
        {
            try
            {
                Console.WriteLine("Enter name...");

                string name = Console.ReadLine();

                Console.WriteLine($"Hello {name}!");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
