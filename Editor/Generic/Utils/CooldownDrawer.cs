using UnityEngine;
using UnityEditor;

namespace BaseTool.Generic.Utils
{
    [CustomPropertyDrawer(typeof(Cooldown))]
    public class CooldownDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField( position, property.FindPropertyRelative( "Duration" ), label );
        }
    }
}
