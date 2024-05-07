using UnityEditor;
using UnityEngine;

namespace BaseTool.Editor
{
    [CustomPropertyDrawer(typeof(PonderateRandom<>))]
    internal class PonderateRandomDrawer : PropertyDrawer
    {
        // TODO(@DarkRewar #core #editor) : create a drawer for ponderate random
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var rect = EditorGUI.PrefixLabel(position, label);
            GUI.Label(position, label);
            GUI.Label(rect, "Not implemented yet.");
        }
    }
}
