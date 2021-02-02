using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;

namespace BaseTool.Generic.Utils
{
    public class TranslationWindow : EditorWindow
    {
        private List<TranslationLanguage> _translationObjects;

        private List<string> _languagesAvailable;

        private string _newTranslateIndex;
        private string _newCountry;
        private string _newCountryCode;
        private string _search;

        private class EditorTranslation
        {
            public string TranslateIndex;
            //public string OriginalIndex;

            public Dictionary<string, string> Translations;
        }

        private Dictionary<string, EditorTranslation> _translations;

        [MenuItem("Window/BaseTool/Generic/Translations")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(TranslationWindow));
        }

        private void OnEnable()
        {
            _translations = new Dictionary<string, EditorTranslation>();
            _translationObjects = new List<TranslationLanguage>();
            string[] files = AssetDatabase.FindAssets("t:BaseTool.Generic.Utils.TranslationLanguage");
            //string[] files = AssetDatabase.FindAssets("t:TranslationLanguage");

            if(files.Length > 0)
            {
                foreach(string f in files)
                {
                    string path = AssetDatabase.GUIDToAssetPath(f);
                    TranslationLanguage tl = AssetDatabase.LoadAssetAtPath<TranslationLanguage>(path);
                    if(tl != null)
                    {
                        tl.LoadTranslations();
                        _translationObjects.Add(tl);
                    }
                }

                LoadTranslations();
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("Search translation:");
            _search = EditorGUILayout.TextField(_search);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical();
            if(_translations != null)
            {
                foreach (var pair in _translations)
                {
                    if(string.IsNullOrEmpty(_search) || pair.Key.Contains(_search))
                    {
                        Rect content = EditorGUILayout.BeginVertical();
                        _translations[pair.Key].TranslateIndex = GUILayout.TextArea(pair.Value.TranslateIndex);
                        foreach (string key in _languagesAvailable)
                        {
                            EditorGUILayout.BeginVertical();
                            GUILayout.Label(key);
                            _translations[pair.Key].Translations[key] = GUILayout.TextArea(_translations[pair.Key].Translations[key]);
                            EditorGUILayout.EndVertical();
                        }
                        EditorGUILayout.EndVertical();
                        GUI.Box(content, GUIContent.none);
                        GUILayout.Space(10);
                    }
                }
            }

            GUILayout.FlexibleSpace();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();

            _newTranslateIndex = GUILayout.TextField(_newTranslateIndex);

            if (GUILayout.Button("Add new translation"))
            {
                //foreach(TranslationLanguage t in _translationObjects)
                //{
                //    t.Translations.Add(_newCountryCode, "");
                //}
                var dic = new Dictionary<string, string>();
                foreach (string l in _languagesAvailable)
                {
                    dic.Add(l, "");
                }
                _translations.Add(_newTranslateIndex, new EditorTranslation
                {
                    TranslateIndex = _newTranslateIndex,
                    Translations = dic
                });
                _newTranslateIndex = null;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Language");
            _newCountry = GUILayout.TextField(_newCountry);
            GUILayout.Label("Code");
            _newCountryCode = GUILayout.TextField(_newCountryCode);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Create new language"))
            {
                TranslationLanguage lang = ScriptableObject.CreateInstance<TranslationLanguage>();
                lang.CountryKey = _newCountryCode;
                lang.CountryName = _newCountry;

                foreach(var t in _translations)
                {
                    lang.Translations.Add(t.Key, "");
                }

                string settingsPath = $"{Application.dataPath}/Settings/Languages";
                string resourcesPath = $"{Application.dataPath}/Resources/Languages";

                if (!Directory.Exists(settingsPath)) Directory.CreateDirectory(settingsPath);
                AssetDatabase.CreateAsset(lang, $"Assets/Settings/Languages/{_newCountryCode}_{_newCountry}.asset");
                AssetDatabase.SaveAssets();

                //TextAsset textAsset = new TextAsset();
                if (!Directory.Exists(resourcesPath)) Directory.CreateDirectory(resourcesPath);
                File.WriteAllText($"{ resourcesPath}/{_newCountryCode}_{_newCountry}.json", "{}");
                //AssetDatabase.CreateAsset(textAsset, $"Assets/Resources/Languages/{_newCountryCode}_{_newCountry}.json");


                EditorUtility.FocusProjectWindow();

                _translationObjects.Add(lang);
                Selection.activeObject = lang;
                _newCountry = null;
                _newCountryCode = null;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            GUI.color = Color.green;
            if(GUILayout.Button("Save translations"))
            {
                foreach (TranslationLanguage tl in _translationObjects)
                {
                    tl.Translations.Clear();
                }

                foreach (var t in _translations)
                {
                    foreach(TranslationLanguage tl in _translationObjects)
                    {
                        tl.Translations.Add(
                            t.Value.TranslateIndex,
                            t.Value.Translations[tl.CountryKey]
                        );
                    }
                }

                foreach (TranslationLanguage tl in _translationObjects)
                {
                    tl.Save();
                }

                LoadTranslations();
            }

            EditorGUILayout.EndVertical();
        }

        void LoadTranslations()
        {
            _translations.Clear();
            _languagesAvailable = new List<string>();
            foreach (var t in _translationObjects)
            {
                _languagesAvailable.Add(t.CountryKey);

                foreach (var trans in t.Translations)
                {
                    if (_translations.ContainsKey(trans.Key))
                    {
                        _translations[trans.Key].Translations.Add(t.CountryKey, trans.Value);
                    }
                    else
                    {
                        var dic = new Dictionary<string, string>();
                        dic.Add(t.CountryKey, trans.Value);
                        _translations.Add(trans.Key, new EditorTranslation
                        {
                            TranslateIndex = trans.Key,
                            Translations = dic
                        });
                    }
                }
            }
        }
    }
}
