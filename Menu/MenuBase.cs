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

        protected int GetInput()
        {
            if (int.TryParse(Console.ReadLine(), out int result)) return result;
            else
            {
                Refresh();

                Console.WriteLine(LocalizationService.CurrentReader["InvalidInputData"]);

                return GetInput();
            }
        }

        private void OnLocalizationChanged(ILocalizationReader reader) => Refresh();
    }
}