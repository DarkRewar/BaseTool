using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

namespace BaseTool.Generic.Utils
{
    public static class Translation
    {
        //public static Dictionary<string, string> Traductions;

        public static Action OnLanguageChanged;

        public readonly static Dictionary<string, string> LanguagesAvailable;

        public static string CurrentCountryKey { get => _translations.CountryKey; }

        public static string CurrentCountry { get => _translations.CountryName; }

        private static TranslationLanguage _translations;

        static Translation()
        {
            //Traductions = new Dictionary<string, string>();

            //Resources.LoadAll<TranslationLanguage>("Settings/Langages");
            //LanguagesAvailable = new Dictionary<string, string>
            //{
            //    { "en", "English" },
            //    { "fr", "Français" }
            //};

            //CurrentLanguage = PlayerPrefs.GetString("Language", "en");
            //ChangeLanguage(CurrentLanguage);
        }

        /// <summary>
        /// Mets à jour la langue sélectionnée.
        /// </summary>
        /// <param name="country"></param>
        public static void ChangeLanguage(string country)
        {
        }

        /// <summary>
        /// Traduit la phrase avec la clé correspondante.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Translate(string key)
        {
            if (_translations.Translations.TryGetValue(key, out string val))
            {
                return val;
            }
            return null;
        }

        /// <summary>
        /// Traduit la phrase avec la clé correspondante.
        /// Si la clé n'existe pas, met le texte par défaut.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultText"></param>
        /// <returns></returns>
        public static string Translate(string key, string defaultText)
        {
            if (_translations.Translations.TryGetValue(key, out string val))
            {
                return val;
            }
            return defaultText;
        }

        /// <summary>
        /// Traduit la phrase avec la clé correspondante.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string TranslateTag(string text)
        {
            Regex regex = new Regex("{{(.*?)}}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(text);

            foreach (Match m in matches)
            {
                string key = m.Value.Remove(m.Length - 2, 2).Remove(0, 2);
                text = text.Replace(m.Value, Translate(key, key));
            }

            return text;
        }
    }    

    public class TranslationContainer
    {
        public List<TranslationPair> TranslationList;

        public TranslationContainer(List<TranslationPair> pairs)
        {
            TranslationList = pairs;
        }
    }

    [Serializable]
    public struct TranslationPair
    {
        public string Index;
        public string Value;

        public TranslationPair(string index, string value)
        {
            Index = index;
            Value = value;
        }
    }
}
