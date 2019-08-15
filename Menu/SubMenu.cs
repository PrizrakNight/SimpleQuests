using System.Collections.Generic;
using SimpleQuests.Commands;

namespace SimpleQuests.Menu
{
    public abstract class SubMenu<TMenuOwner> : NumericCommandMenu where TMenuOwner : MenuBase, new()
    {
        public SubMenu() => commands = new HashSet<NumericCommand>
        {
            new NumericCommand(0, "Back", MoveBack)
        };

        protected virtual void OnBack() { }

        private void MoveBack()
        {
            new TMenuOwner().Print();

            OnBack();
        }
    }
}