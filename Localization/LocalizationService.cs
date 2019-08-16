using System;

namespace SimpleQuests.Localization
{
    public static class LocalizationService
    {
        public static event Action<ILocalizationReader> OnReaderChanged;

        public static ILocalizationReader CurrentReader { get; private set; } =
            CreateReader<XmlLocalizationReader>("Translations/EN.lang");

        public static TLocalizationReader CreateReader<TLocalizationReader>(string filename)
            where TLocalizationReader : ILocalizationReader, new()
        {
            TLocalizationReader localizationReader = new TLocalizationReader();

            localizationReader.Read(filename);

            return localizationReader;
        }

        public static void SetReader(ILocalizationReader localizationReader)
        {
            CurrentReader = localizationReader;

            OnReaderChanged?.Invoke(localizationReader);
        }

        public static string GetStringWithParam(string key, object param) =>
            CurrentReader[key].Replace("{0}", param.ToString());

        public static string GetStringWithParams(string key, params object[] objects) =>
            string.Format(CurrentReader[key], objects);
    }
}