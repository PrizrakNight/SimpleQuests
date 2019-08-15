namespace SimpleQuests.Localization
{
    public interface ILocalizationReader
    {
        string this[string key] { get; }

        void Read(string filename);
    }
}