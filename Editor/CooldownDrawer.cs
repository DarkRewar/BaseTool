using UnityEngine;
using UnityEditor;

namespace BaseTool
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
