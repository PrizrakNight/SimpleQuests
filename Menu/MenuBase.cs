using System;
using SimpleQuests.Localization;

namespace SimpleQuests.Menu
{
    public abstract class MenuBase : IPrintable
    {
        public MenuBase() => LocalizationService.OnReaderChanged += OnLocalizationChanged;

        public abstract void Print();

        protected virtual void Refresh()
        {
            Console.Clear();

            Print();
        }

        protected void GetInput(out int input)
        {
            if (int.TryParse(Console.ReadLine(), out int result)) input = result;
            else
            {
                Refresh();

                Console.WriteLine(LocalizationService.CurrentReader["InvalidInputData"]);

                GetInput(out input);
            }
        }

        private void OnLocalizationChanged(ILocalizationReader reader) => Refresh();
    }
}