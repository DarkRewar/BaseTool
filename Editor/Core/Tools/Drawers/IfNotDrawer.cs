using UnityEditor;
using UnityEngine;

namespace BaseTool.Tools.Drawers
{
    [CustomPropertyDrawer(typeof(IfNotAttribute))]
    public class IfNotDrawer : ConditionalDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CheckObject(property.serializedObject, out string selector);
            SerializedProperty targetProperty = property.serializedObject.FindProperty(selector);
            if (!targetProperty.boolValue)
            {
                _display = true;
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}