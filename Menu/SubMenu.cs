using SimpleQuests.Commands;

namespace SimpleQuests.Menu
{
    public abstract class SubMenu<TMenuOwner> : NumericCommandMenu where TMenuOwner : MenuBase, new()
    {
        public SubMenu() : base() => commands.Add(new NumericCommand(0, "Back", MoveBack));

        protected virtual void OnBack() { }

        private void MoveBack()
        {
            new TMenuOwner().Print();

            OnBack();
        }
    }
}