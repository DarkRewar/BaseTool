using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEditor;
using UnityEngine;

namespace BaseTool
{
    public enum SaveFormat
    {
        Json = 0,
        XML = 1,
        Csv = 2,
        Binary = 3
    }
    
    [CreateAssetMenu(menuName = "BaseTool/SaveFile Architecture")]
    public class SaveFileArchitecture : ScriptableObject
    {
        private static JsonConverter _jsonConverter;
        
        [Header("Save File Settings")]
        [SerializeField] private string _saveFileName = "Game.sav";
        [SerializeField] private string _saveFolder = "{Application.dataPath}";
        [SerializeField] private SaveFormat _saveFormat = SaveFormat.Json;
        
        [Header("Save File Values")]
        [SerializeField] private List<SavedVariable> _variables;

        public string SavePath
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                return sb.ToString();
            }
        }

        public bool IsValidFolderPath() => true;

        public string GetPath(string key) => key switch
        {
            "Application.dataPath" or "APPDATA" => Application.dataPath,
            _ => throw new InvalidExpressionException($"Path key {key} is not valid.")
        };

        public string SaveFilePath => $"{SavePath}/{_saveFileName}";

        // private void OnValidate()
        // {
        //     if (_variables.Count == _internalVariables.Count) return;
        //     
        //     foreach (var variable in _internalVariables)
        //     
        //     
        //     AssetDatabase.SaveAssets();
        // }

        [ContextMenu("Load")]
        public void Load()
        {
            // load sync
            string saveFile = File.ReadAllText(_saveFileName);
            Dictionary<string, object> container = JsonConvert.DeserializeObject<Dictionary<string, object>>(saveFile);
            SaveFileSerializationContext context = new SaveFileSerializationContext(container);
            foreach (var savedVariable in _variables)
            {
                savedVariable.DeserializeFromFile(context);
            }
        }

        public async void LoadAsync()
        {
            // load async
        }

        [ContextMenu("Save")]
        public void Save()
        {
            // enqueue to save buffer
            SaveFileSerializationContext context = new SaveFileSerializationContext();
            foreach (var savedVariable in _variables)
            {
                savedVariable.SerializeToFile(context);
            }
            string json = JsonConvert.SerializeObject(context.Values, Formatting.Indented);

            if (Application.isPlaying)
            {
                SaveBuffer.SaveToFile(_saveFileName, json);
            }
            else
            {
                File.WriteAllText(_saveFileName, json);
#if UNITY_EDITOR
                AssetDatabase.Refresh();
#endif
            }
        }

        [ContextMenu("Add String Variable")]
        private void AddStringVariable()
        {
            StringSavedVariable variable = ScriptableObject.CreateInstance<StringSavedVariable>();
            variable.name = "StringSavedVariable";
            _variables.Add(variable);
            
            AssetDatabase.AddObjectToAsset(variable, this);
            AssetDatabase.SaveAssets();
        }

        [ContextMenu("Add Int Variable")]
        private void AddIntVariable()
        {
            IntSavedVariable variable = ScriptableObject.CreateInstance<IntSavedVariable>();
            variable.name = "StringSavedVariable";
            _variables.Add(variable);
            
            AssetDatabase.AddObjectToAsset(variable, this);
            AssetDatabase.SaveAssets();
        }

        [ContextMenu("Clear Variables")]
        private void ClearVariable()
        {
            foreach (SavedVariable variable in _variables)
            {
                if (!variable) continue;
                AssetDatabase.RemoveObjectFromAsset(variable);
                DestroyImmediate(variable);
                AssetDatabase.SaveAssets();
            }
            _variables.Clear();
        }

        [ContextMenu("Revert changes")]
        internal void RevertChanges()
        {
            
        }

        [ContextMenu("Apply changes")]
        internal void ApplyChanges()
        {
            
        }
    }
}
