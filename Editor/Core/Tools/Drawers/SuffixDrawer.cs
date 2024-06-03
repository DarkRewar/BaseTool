using UnityEditor;
using UnityEngine;

namespace BaseTool.Editor.Tools.Drawers
{
    [CustomPropertyDrawer(typeof(SuffixAttribute))]
    public class SuffixDrawer : BaseToolPropertyDrawer
    {
        private static GUIStyle _suffixStyle = null;
        internal static GUIStyle SuffixStyle
        {
            get
            {
                if (_suffixStyle == null)
                {
                    _suffixStyle = new GUIStyle();
                    _suffixStyle.alignment = TextAnchor.MiddleRight;
                    _suffixStyle.normal.textColor = new Color(0.5f, 0.52f, 0.541f);
                    _suffixStyle.padding = new RectOffset(0, 5, 0, 0);
                }
                return _suffixStyle;
            }
        }

        public override void OnDrawGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (CanUseDrawer(property.propertyType))
            {
                SuffixAttribute suffix = attribute as SuffixAttribute;

                EditorGUI.PropertyField(position, property);
                GUI.Label(position, suffix.Text, SuffixStyle);
            }
            else
            {
                var style = new GUIStyle();
                style.normal.textColor = Color.red;
                GUI.Label(position, $"You can't use Suffix on a {property.propertyType}!", style);
            }
        }

        private bool CanUseDrawer(SerializedPropertyType propertyType) => propertyType switch
        {
            SerializedPropertyType.Integer => true,
            SerializedPropertyType.Float => true,
            SerializedPropertyType.String => true,
            SerializedPropertyType.Hash128 => true,
            _ => false
        };
    }
}
