using UnityEngine;
using UnityEditor;
using BaseTool.Editor.Tools.Drawers;

namespace BaseTool.Tools.Drawers
{
    [CustomPropertyDrawer(typeof(Cooldown))]
    public class CooldownDrawer : BaseToolPropertyDrawer
    {
        public override void OnDrawGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("Duration"), label);
        }
    }
}
