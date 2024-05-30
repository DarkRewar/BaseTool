using System;
using System.Collections.Generic;
using System.Linq;
using DynamicExpresso;
using UnityEditor;
using UnityEngine;

namespace BaseTool.Tools.Drawers
{
    public abstract class ConditionalDrawer : PropertyDrawer
    {
        protected bool _display = false;

        protected bool GetValueFromObject(SerializedObject serializedObject)
        {
            _display = false;

            ConditionalAttribute condAttribute = attribute as ConditionalAttribute;
            var selector = condAttribute.Selector;

            try
            {
                var interpreter = new Interpreter();
                interpreter.Reference(GetEveryEnums(serializedObject));
                interpreter.SetVariable("this", serializedObject.targetObject, serializedObject.targetObject.GetType());

                return interpreter.Eval<bool>(selector);
            }
            catch (InvalidOperationException ioe)
            {
                Debug.LogError($"Error when trying to parse expression \"{selector}\": {ioe.Message}", serializedObject.targetObject);
            }

            return false;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_display)
                return base.GetPropertyHeight(property, label);
            return 0;
        }

        protected List<ReferenceType> GetEveryEnums(SerializedObject serializedObject)
        {
            var assembly = serializedObject.targetObject.GetType().Assembly;
            var everyTypes = assembly.GetTypes();
            return everyTypes.Select(t => new ReferenceType(t.Name, t)).ToList();
        }
    }
}