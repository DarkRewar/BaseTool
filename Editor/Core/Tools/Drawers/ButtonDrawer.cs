using UnityEditor;
using UnityEngine;

namespace BaseTool.Tools.Drawers
{
    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    public class ButtonDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            GUI.Label(position, "test");

            if (GUI.Button(position, "test"))
            {
                // tell the current game object to find and run the method we asked for!
                Selection.activeGameObject.BroadcastMessage("");
            }
        }

        public override float GetHeight() => 18f;

        public void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.Label(position, label);
        }
    }
}
