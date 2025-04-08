using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BaseTool.Editor
{
    [CustomEditor(typeof(SaveFileArchitecture))]
    public class SaveFileArchetypeEditor : UnityEditor.Editor
    {
        [SerializeField] private VisualTreeAsset _template;

        private SaveFileArchitecture _saveFile;
        private SerializedProperty _variablesList;
        private IEnumerable<Type> _variableTypes;

        private void OnEnable()
        {
            _saveFile = (SaveFileArchitecture)target;
            _variablesList = serializedObject.FindProperty("_variables");

            Assembly assembly = typeof(SavedVariable).Assembly;
            Type savedType = typeof(SavedVariable);
            _variableTypes = assembly.GetTypes()
                .Where(type => savedType.IsAssignableFrom(type) && !type.IsAbstract && !type.IsSealed);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Assembly assembly = typeof(SavedVariable).Assembly;
            Type target = typeof(SavedVariable);
            var types = assembly.GetTypes()
                .Where(type => target.IsAssignableFrom(type) && !type.IsAbstract && !type.IsSealed);

            EditorGUI.BeginChangeCheck();
            int index = EditorGUILayout.Popup("Add variable", -1, types.Select(t => t.Name).ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                Type selectedType = types.ToArray()[index];

                SavedVariable variable = (SavedVariable)ScriptableObject.CreateInstance(selectedType);
                AssetDatabase.AddObjectToAsset(variable, _saveFile);

                SerializedObject variableSerialized = new SerializedObject(variable);
                variableSerialized.FindProperty("_id").stringValue = selectedType.Name;
                variableSerialized.ApplyModifiedProperties();
                int arraySize = _variablesList.arraySize;
                _variablesList.InsertArrayElementAtIndex(arraySize);
                _variablesList.GetArrayElementAtIndex(arraySize).objectReferenceValue = variable;
                _variablesList.serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(_saveFile);

                AssetDatabase.SaveAssets();
            }

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

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = _template.Instantiate();
            ListView listView = root.Q<ListView>("SavedVariables");
            listView.makeItem = () =>
            {
                VisualElement line = new VisualElement();
                line.style.flexDirection = FlexDirection.Row;

                InspectorElement inspectorElement = new();
                inspectorElement.style.flexGrow = 1;
                inspectorElement.name = "Element";
                line.Add(inspectorElement);

                Button button = new Button();
                button.text = "-";
                button.name = "Remove";

                line.Add(button);

                return line;
            };
            listView.bindItem += BindItem;

            ToolbarMenu menu = root.Q<ToolbarMenu>("AddVariable");
            foreach (var type in _variableTypes)
            {
                menu.menu.AppendAction(type.Name, CreateVariableCallback);
            }

            return root;
        }

        private void CreateVariableCallback(DropdownMenuAction action)
        {
            Type selectedType = _variableTypes.First(t => t.Name.Equals(action.name));

            SavedVariable variable = (SavedVariable)ScriptableObject.CreateInstance(selectedType);
            AssetDatabase.AddObjectToAsset(variable, _saveFile);

            SerializedObject variableSerialized = new SerializedObject(variable);
            variableSerialized.FindProperty("_id").stringValue = selectedType.Name;
            variableSerialized.ApplyModifiedProperties();
            int arraySize = _variablesList.arraySize;
            _variablesList.InsertArrayElementAtIndex(arraySize);
            _variablesList.GetArrayElementAtIndex(arraySize).objectReferenceValue = variable;
            _variablesList.serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(_saveFile);

            AssetDatabase.SaveAssets();
        }

        private void BindItem(VisualElement listEntry, int index)
        {
            Button button = listEntry.Q<Button>("Remove");
            button.UnregisterCallback<ClickEvent, int>(OnRemoveClicked);
            button.RegisterCallback<ClickEvent, int>(OnRemoveClicked, index);

            SerializedObject elementObject = new SerializedObject(_variablesList.GetArrayElementAtIndex(index).objectReferenceValue);
            InspectorElement inspectorElement = listEntry.Q<InspectorElement>("Element");
            inspectorElement.Bind(elementObject);
            // propertyField.Bind(_variablesList.GetArrayElementAtIndex(index).serializedObject);
            // propertyField.BindProperty(_variablesList.GetArrayElementAtIndex(index));
        }

        private void OnRemoveClicked(ClickEvent evt, int index)
        {
            SavedVariable variable = (SavedVariable)_variablesList.GetArrayElementAtIndex(index).objectReferenceValue;
            bool answerWasYes = EditorUtility.DisplayDialog (
                "Deleting SavedVariable", 
                $"Do you really want to delete {variable.Id} ({variable.GetType().Name})?", 
                "Yes", 
                "No");
            if (!answerWasYes) return;
            
            AssetDatabase.RemoveObjectFromAsset(variable);
            DestroyImmediate(variable);
            _variablesList.DeleteArrayElementAtIndex(index);
            _variablesList.serializedObject.ApplyModifiedProperties();
            AssetDatabase.SaveAssets();
        }
    }
}