using UnityEditor;
using UnityEngine;

namespace BaseTool.Tools.Drawers
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    internal class RequiredDrawer : PropertyDrawer
    {
        private static readonly Color32 _requiredColor = new Color32(192, 57, 43, 255);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                EditorGUILayout.PropertyField(property, label);
                return;
            }

            EditorGUILayout.BeginVertical();
            Color previousColor = GUI.color;
            if (!property.objectReferenceValue)
            {
                RequiredAttribute requiredAttribute = (RequiredAttribute)attribute;
                string message = requiredAttribute.Message ?? $"Property {ObjectNames.NicifyVariableName(property.name)} is required!";
                EditorGUILayout.HelpBox(message, MessageType.Error);
                GUI.color = _requiredColor;
                label.tooltip = $"<color=red>Field is required</color>\n{label.tooltip}";
                Debug.LogError($"Property {property.name} is required!", property.serializedObject.targetObject);
            }

            EditorGUILayout.PropertyField(property, label);
            GUI.color = previousColor;
            EditorGUILayout.EndVertical();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 0;
    }
}