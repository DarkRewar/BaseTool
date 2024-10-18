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

        public GUIStyle GetStyle()
        {
            GUIStyle style = GUI.skin.GetStyle("Foldout");
            //style.normal.background = Texture2D.blackTexture;

            return style;
        }

        // TODO(@DarkRewar #core #editor) : create a drawer for ponderate random
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // var rect = EditorGUI.PrefixLabel(position, label);
            // GUI.Label(rect, "Not implemented yet.");
            //EditorGUI.PropertyField(position, property, label);
            // var controlID = GUIUtility.GetControlID(FocusType.Passive, position);
            // var fieldPosition = EditorGUI.PrefixLabel(position, controlID, label);

            // Rect rect = position;
            // rect.height = EditorGUIUtility.singleLineHeight;
            // Rect foldoutRect = position;
            // foldoutRect.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.BeginChangeCheck();
            _foldout = EditorGUILayout.BeginFoldoutHeaderGroup(_foldout, label, GetStyle());
            if (_foldout)
            {
                var prop = property.FindPropertyRelative("_entries");
                var totalWeightProperty = property.FindPropertyRelative("<TotalWeight>k__BackingField");
                // var list = UnityEditorInternal.ReorderableList.GetReorderableListFromSerializedProperty(prop);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Object");
                EditorGUILayout.LabelField($"Weight (Total: {totalWeightProperty.floatValue})");
                EditorGUILayout.EndHorizontal();
                
                int i = 0;
                foreach (SerializedProperty p in prop)
                {
                    var line = EditorGUILayout.BeginHorizontal();
                    var valueProp = p.FindPropertyRelative("Value");
                    var weightProp = p.FindPropertyRelative("Weight");
                    // rect.y += EditorGUIUtility.singleLineHeight;
                    EditorGUILayout.PropertyField(valueProp, GUIContent.none);
                    // rect.y += EditorGUIUtility.singleLineHeight;
                    EditorGUILayout.PropertyField(weightProp, GUIContent.none);
                    EditorGUILayout.EndHorizontal();
                    ++i;
                }
                //list.DoLayoutList();

                _count = i;
                
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    prop.arraySize -= 1;
                }

                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    prop.arraySize += 1;
                }
                EditorGUILayout.EndHorizontal();
                
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUI.EndChangeCheck();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 0;
    }
}