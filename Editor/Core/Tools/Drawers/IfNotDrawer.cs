using UnityEditor;
using UnityEngine;

namespace BaseTool.Tools.Drawers
{
    [CustomPropertyDrawer(typeof(IfNotAttribute))]
    public class IfNotDrawer : ConditionalDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!GetValueFromObject(property.serializedObject))
            {
                _display = true;
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}