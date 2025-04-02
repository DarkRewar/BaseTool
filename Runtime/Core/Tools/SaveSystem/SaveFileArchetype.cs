using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
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
    
    [CreateAssetMenu(menuName = "BaseTool/Create SaveFile Architecture")]
    public class SaveFileArchitecture : ScriptableObject
    {
        [Header("Save File Settings")]
        [SerializeField] private string _saveFileName = "Game.sav";
        [SerializeField] private string _saveFolder = "Not used yet";
        [SerializeField] private SaveFormat _saveFormat = SaveFormat.Json;
        
        [Header("Save File Values")]
        [SerializeField] private List<SavedVariable> _variables;

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
            foreach (var savedVariable in _variables)
            {
                if (container.TryGetValue(savedVariable.Id, out object value))
                {
                    savedVariable.SetSavedValue(value);
                }
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
            Dictionary<string, object> container = new();
            foreach (var savedVariable in _variables)
            {
                container.Add(savedVariable.Id, savedVariable.GetSavedValue());
            }
            string json = JsonConvert.SerializeObject(container, Formatting.Indented);
            File.WriteAllText(_saveFileName, json);
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
            foreach (StringSavedVariable variable in _variables)
            {
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
