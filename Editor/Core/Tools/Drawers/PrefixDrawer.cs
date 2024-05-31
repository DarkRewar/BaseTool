using UnityEditor;
using UnityEngine;

namespace BaseTool.Editor.Tools.Drawers
{
    [CustomPropertyDrawer(typeof(PrefixAttribute))]
    public class PrefixDrawer : PropertyDrawer
    {
        private static GUIStyle _prefixStyle = null;
        internal static GUIStyle PrefixStyle
        {
            get
            {
                if (_prefixStyle == null)
                {
                    _prefixStyle = new GUIStyle();
                    _prefixStyle.alignment = TextAnchor.MiddleRight;
                    _prefixStyle.normal.textColor = new Color(0.5f, 0.52f, 0.541f);
                    _prefixStyle.padding = new RectOffset(0, 5, 0, 0);
                }
                return _prefixStyle;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (CanUseDrawer(property.propertyType))
            {
                PrefixAttribute prefix = attribute as PrefixAttribute;
                var offsetPosition = EditorGUI.PrefixLabel(position, label);

                var content = new GUIContent(prefix.Text);
                var textSize = PrefixStyle.CalcSize(content);
                var labelPosition = new Rect(offsetPosition.x, position.y, textSize.x, position.height);
                GUI.Label(labelPosition, content, PrefixStyle);

                var propertyPosition = new Rect(
                    offsetPosition.x + textSize.x,
                    position.y,
                    offsetPosition.width - textSize.x,
                    position.height);
                EditorGUI.PropertyField(propertyPosition, property, GUIContent.none);
            }
            else
            {
                var style = new GUIStyle();
                style.normal.textColor = Color.red;
                GUI.Label(position, $"You can't use Prefix on a {property.propertyType}!", style);
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
