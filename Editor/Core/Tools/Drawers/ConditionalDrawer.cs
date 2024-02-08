using UnityEditor;
using UnityEngine;

namespace BaseTool.Tools.Drawers
{
    public abstract class ConditionalDrawer : PropertyDrawer
    {
        protected bool _display = false;

        protected void CheckObject(SerializedObject serializedObject, out string selector)
        {
            _display = false;
            ConditionalAttribute condAttribute = attribute as ConditionalAttribute;
            selector = condAttribute.Selector;

            SerializedProperty targetProperty = serializedObject.FindProperty(selector);
            UnityEngine.Assertions.Assert.IsTrue(targetProperty != null, $"Property {selector} not found.");
            UnityEngine.Assertions.Assert.IsTrue(
                targetProperty.propertyType == SerializedPropertyType.Boolean,
                $"Property {selector} must be a boolean."
            );
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_display)
                return base.GetPropertyHeight(property, label);
            return 0;
        }
    }
}