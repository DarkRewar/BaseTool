using UnityEditor;
using UnityEngine;

namespace BaseTool
{
    [CustomPropertyDrawer(typeof(ValueListener<>))]
    public class ValueListenerDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var field = property.FindPropertyRelative("_value");
            if (field == null) return;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("_value"), label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var field = property.FindPropertyRelative("_value");
            if (field == null) return 0;
            return base.GetPropertyHeight(property, label);
        }
    }
}