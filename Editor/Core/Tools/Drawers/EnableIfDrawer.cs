using UnityEditor;
using UnityEngine;

namespace BaseTool.Tools.Drawers
{
    [CustomPropertyDrawer(typeof(EnableIfAttribute))]
    public class EnableIfDrawer : ConditionalDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = GetValueFromObject(property.serializedObject);
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
            _display = true;
        }
    }
}