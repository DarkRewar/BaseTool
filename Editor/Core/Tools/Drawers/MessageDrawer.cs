using UnityEditor;
using UnityEngine;

namespace BaseTool.Tools.Drawers
{
    [CustomPropertyDrawer(typeof(MessageAttribute), true)]
    public class MessageDrawer : PropertyDrawer
    {
        public MessageType ConvertMessageType(MessageAttribute.MessageType type) => type switch
        {
            MessageAttribute.MessageType.Info => MessageType.Info,
            MessageAttribute.MessageType.Warning => MessageType.Warning,
            MessageAttribute.MessageType.Error => MessageType.Error,
            _ => MessageType.None,
        };
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var msg = attribute as MessageAttribute;
            EditorGUILayout.HelpBox(msg.Message, ConvertMessageType(msg.Type));
            EditorGUILayout.PropertyField(property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 0;
    }
}
