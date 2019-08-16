using System;
using SimpleQuests.Menu.Specific;

namespace SimpleQuests
{
    class Program
    {
        public static readonly Random Random = new Random();

        static void Main(string[] args)
        {
            new WelcomeMenu().Print();

            Console.ReadKey();
        }
    }
}
