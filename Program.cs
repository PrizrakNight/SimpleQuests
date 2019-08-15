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
