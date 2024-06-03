using UnityEditor;
using UnityEngine;

namespace BaseTool.Editor.Tools.Drawers
{
    [CustomPropertyDrawer(typeof(PonderateRandom<>))]
    internal class PonderateRandomDrawer : BaseToolPropertyDrawer
    {
        // TODO(@DarkRewar #core #editor) : create a drawer for ponderate random
        public override void OnDrawGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var rect = EditorGUI.PrefixLabel(position, label);
            GUI.Label(rect, "Not implemented yet.");
        }
    }
}
