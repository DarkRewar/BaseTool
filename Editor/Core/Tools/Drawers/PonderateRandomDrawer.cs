using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BaseTool.Editor
{
    [CustomPropertyDrawer(typeof(PonderateRandom<>), true)]
    public class PonderateRandomDrawer : PropertyDrawer
    {
        private bool _foldout = true;
        private int _count;
        
        // TODO(@DarkRewar #core #editor) : create a drawer for ponderate random
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // var rect = EditorGUI.PrefixLabel(position, label);
            // GUI.Label(rect, "Not implemented yet.");
            //EditorGUI.PropertyField(position, property, label);
            // var controlID = GUIUtility.GetControlID(FocusType.Passive, position);
            // var fieldPosition = EditorGUI.PrefixLabel(position, controlID, label);
            
            Rect rect = position;
            rect.height = EditorGUIUtility.singleLineHeight;
            Rect foldoutRect = position;
            foldoutRect.height = EditorGUIUtility.singleLineHeight;
            _foldout = EditorGUI.Foldout(foldoutRect, _foldout, label);
            if (_foldout)
            {
                GUIContent content = new GUIContent();
                var prop = property.FindPropertyRelative("_entries");
                int i = 0;
                foreach (SerializedProperty p in prop)
                {
                    var valueProp = p.FindPropertyRelative("Value");
                    var weightProp = p.FindPropertyRelative("Weight");
                    rect.y += EditorGUIUtility.singleLineHeight;
                    EditorGUI.PropertyField(rect, valueProp);
                    rect.y += EditorGUIUtility.singleLineHeight;
                    EditorGUI.PropertyField(rect, weightProp);
                    ++i;
                }

                _count = i;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!_foldout) return EditorGUIUtility.singleLineHeight;
            return 2 * _count * EditorGUIUtility.singleLineHeight;
        }
    }
}
