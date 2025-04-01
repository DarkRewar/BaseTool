using UnityEditor;
using UnityEngine;

namespace BaseTool.Tools.Drawers
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    internal class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }
}
