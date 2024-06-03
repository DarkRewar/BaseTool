using UnityEditor;
using UnityEngine;

namespace BaseTool.Editor.Tools.Drawers
{
    [CustomPropertyDrawer(typeof(ValueListener<>))]
    public class ValueListenerDrawer : BaseToolPropertyDrawer
    {
        public override void OnDrawGUI(Rect position, SerializedProperty property, GUIContent label)
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