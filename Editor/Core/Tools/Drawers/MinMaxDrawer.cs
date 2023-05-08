using BaseTool.Tools.Attributes;
using UnityEditor;
using UnityEngine;

namespace BaseTool.Tools.Drawers
{ 
    [CustomPropertyDrawer(typeof(MinMaxAttribute))]
    public class MinMaxDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Asserts that we're using the MinMax attribute on a Vector2
            UnityEngine.Assertions.Assert.IsTrue(property.propertyType == SerializedPropertyType.Vector2, "You must use a Vector2 for MinMax");

            if (property.propertyType == SerializedPropertyType.Vector2)
            {
                MinMaxAttribute minMax = attribute as MinMaxAttribute;

                //Writes the variable name on the left
                Rect totalValueRect = EditorGUI.PrefixLabel(position, label);

                //The left value, after the variable name
                Rect leftRect = new Rect(totalValueRect.x, totalValueRect.y, 50, totalValueRect.height);

                //Rect of the slider
                Rect valueRect = new Rect(leftRect.xMax, totalValueRect.y, totalValueRect.width - leftRect.width * 2 - 4, totalValueRect.height);

                //The right value
                Rect rightRect = new Rect(totalValueRect.xMax - leftRect.width - 2, totalValueRect.y, leftRect.width, totalValueRect.height);

                float minValue = property.vector2Value.x; //Current x
                float maxValue = property.vector2Value.y; //Current y

                EditorGUI.MinMaxSlider(valueRect, ref minValue, ref maxValue, minMax.minLimit, minMax.maxLimit);

                //Assigns the value to the property
                property.vector2Value = new Vector2(minValue, maxValue);

                EditorGUI.LabelField(leftRect, minValue.ToString("F3")); //Writes the value on the left
                EditorGUI.LabelField(rightRect, maxValue.ToString("F3")); //Writes the value on the right
            }
            else
            {
                GUI.Label(position, "You can use MinMax only on a Vector2!");
            }
        }
    }
}
