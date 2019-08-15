using System;
using System.IO;

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

        public static string[] GetAvailableLanguages() => Directory.GetFiles("Translations");
    }
}