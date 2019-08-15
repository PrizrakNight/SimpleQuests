using System.Collections.Generic;
using System.Xml;

namespace SimpleQuests.Localization
{
    public class XmlLocalizationReader : ILocalizationReader
    {
        public string this[string key] => _localizationDictionary[key];

        private Dictionary<string, string> _localizationDictionary;

        public void Read(string filename)
        {
            _localizationDictionary = new Dictionary<string, string>();

            XmlDocument document = new XmlDocument();

            document.Load(filename);

            foreach (object element in document.SelectSingleNode("Translations"))
            {
                if (element is XmlElement xmlElement)
                {
                    string attributeValue = xmlElement.GetAttribute("value");

                    string content = !string.IsNullOrEmpty(attributeValue) ? attributeValue : xmlElement.InnerText;

                    _localizationDictionary.Add(xmlElement.Name, content);
                }
            }
        }
    }
}