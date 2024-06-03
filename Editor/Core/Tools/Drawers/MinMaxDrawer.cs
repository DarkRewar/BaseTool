using UnityEditor;
using UnityEngine;

namespace BaseTool.Editor.Tools.Drawers
{
    [CustomPropertyDrawer(typeof(MinMaxAttribute))]
    public class MinMaxDrawer : BaseToolPropertyDrawer
    {
        public override void OnDrawGUI(Rect position, SerializedProperty property, GUIContent label)
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

                float minValue = EditorGUI.FloatField(leftRect, property.vector2Value.x); //Writes the value on the left
                float maxValue = EditorGUI.FloatField(rightRect, property.vector2Value.y); //Writes the value on the right

                EditorGUI.MinMaxSlider(valueRect, ref minValue, ref maxValue, minMax.MinLimit, minMax.MaxLimit);

                //Assigns the value to the property
                property.vector2Value = new Vector2(minValue, maxValue);
            }
            else
            {
                GUI.Label(position, "You can use MinMax only on a Vector2!");
            }
        }
    }
}
