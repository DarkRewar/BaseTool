using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BaseTool.Editor
{
    [CustomEditor(typeof(SaveFileArchitecture))]
    public class SaveFileArchetypeEditor : UnityEditor.Editor
    {
        private SaveFileArchitecture _saveFile;
        
        private void OnEnable()
        {
            _saveFile = (SaveFileArchitecture) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Save"))
            {
                _saveFile.Save();
            }
            if (GUILayout.Button("Load"))
            {
                _saveFile.Load();
            }
            
            EditorGUILayout.EndHorizontal();
        }
    }
}