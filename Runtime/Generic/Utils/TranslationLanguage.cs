using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BaseTool.Generic.Utils
{
    public class TranslationLanguage : ScriptableObject
    {
        public string CountryKey = "en";
        public string CountryName = "English";

        public Dictionary<string, string> Translations = new Dictionary<string, string>();

        public bool LoadTranslations()
        {
            string json = null;
#if UNITY_EDITOR
            string key = $"Assets/Resources/Languages/{CountryKey}_{CountryName}.json";
            if (File.Exists(key))
            {
                json = File.ReadAllText(key);
            }
#else            
            string key = $"Languages/{CountryKey}_{CountryName}";
            TextAsset jsonFile = Resources.Load(key) as TextAsset;
            if (jsonFile != null)
            {
                Debug.Log("Languages files retrieved !");
                json = jsonFile.text;
            }
# endif
            if (!string.IsNullOrEmpty(json))
            {
                TranslationContainer container = JsonUtility.FromJson<TranslationContainer>(json);

                if (container != null)
                {
                    if (Translations == null) Translations = new Dictionary<string, string>();
                    else Translations.Clear();
                    foreach (TranslationPair pair in container.TranslationList)
                    {
                        Translations.Add(pair.Index, pair.Value);
                    }

                    return true;
                }
            }

            return false;
        }

        public void Save()
        {
            List<TranslationPair> pairs = new List<TranslationPair>();
            foreach (var pair in Translations)
            {
                pairs.Add(new TranslationPair(pair.Key, pair.Value));
            }
            TranslationContainer container = new TranslationContainer(pairs);
            string json = JsonUtility.ToJson(container);
            string key = $"{Application.dataPath}/Resources/Languages/{CountryKey}_{CountryName}.json";
            File.WriteAllText(key, json);
        }
    }
}
