using System.Reflection;
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
            if (targetProperty == null)
            {
                if (serializedObject.targetObject == null) return;
                PropertyInfo propertyInfo = serializedObject.targetObject.GetType().GetProperty(selector);
                UnityEngine.Assertions.Assert.IsTrue(propertyInfo != null, $"Property of field {selector} not found.");
                UnityEngine.Assertions.Assert.IsTrue(propertyInfo.PropertyType == typeof(bool), $"Property of field {selector} is not a boolean.");
            }
            else
            {
                //UnityEngine.Assertions.Assert.IsTrue(targetProperty != null, $"Property {selector} not found.");
                UnityEngine.Assertions.Assert.IsTrue(
                    targetProperty.propertyType == SerializedPropertyType.Boolean,
                    $"Property {selector} must be a boolean.");
            }
        }

        protected bool GetValueFromObject(SerializedObject serializedObject, string selector)
        {
            SerializedProperty targetProperty = serializedObject.FindProperty(selector);
            if (targetProperty == null)
            {
                PropertyInfo propertyInfo = serializedObject.targetObject.GetType().GetProperty(selector);
                return (bool)propertyInfo.GetValue(serializedObject.targetObject);
            }
            else
            {
                return targetProperty.boolValue;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_display)
                return base.GetPropertyHeight(property, label);
            return 0;
        }
    }
}