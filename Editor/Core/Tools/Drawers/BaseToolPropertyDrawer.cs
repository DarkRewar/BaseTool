using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DynamicExpresso;
using UnityEditor;
using UnityEngine;

namespace BaseTool.Editor.Tools.Drawers
{
    public abstract class BaseToolPropertyDrawer : PropertyDrawer
    {
        public enum VisibleState
        {
            Default = 0,
            ReadOnly = 1,
            Hidden = 2
        }

        protected VisibleState _state = VisibleState.Default;

        internal bool IsVisible => _state != VisibleState.Hidden;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            UpdateState(property);
            if (!IsVisible) return;
            OnDrawGUI(position, property, label);
        }

        public abstract void OnDrawGUI(Rect position, SerializedProperty property, GUIContent label);

        protected void UpdateState(SerializedProperty property)
        {
            _state = VisibleState.Default;

            var ifAttribute = fieldInfo.GetCustomAttribute<IfAttribute>(true);
            if (ifAttribute != null)
            {
                _state = GetValueFromObject(ifAttribute, property.serializedObject) ? VisibleState.Default : VisibleState.Hidden;
            }

            var ifNotAttribute = fieldInfo.GetCustomAttribute<IfNotAttribute>(true);
            if (ifNotAttribute != null)
            {
                _state = GetValueFromObject(ifNotAttribute, property.serializedObject) ? VisibleState.Hidden : VisibleState.Default;
            }

            if (!IsVisible) return;

            var enableIfAttribute = fieldInfo.GetCustomAttribute<EnableIfAttribute>(true);
            if (enableIfAttribute != null)
            {
                _state = GetValueFromObject(enableIfAttribute, property.serializedObject) ? VisibleState.Default : VisibleState.ReadOnly;
            }

            var disableIfAttribute = fieldInfo.GetCustomAttribute<DisableIfAttribute>(true);
            if (disableIfAttribute != null)
            {
                _state = GetValueFromObject(disableIfAttribute, property.serializedObject) ? VisibleState.ReadOnly : VisibleState.Default;
            }
        }

        protected bool GetValueFromObject(SerializedObject serializedObject)
        {
            ConditionalAttribute condAttribute = attribute as ConditionalAttribute;
            return GetValueFromObject(condAttribute, serializedObject);
        }

        protected bool GetValueFromObject(ConditionalAttribute attribute, SerializedObject serializedObject)
        {
            var selector = attribute.Selector;

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
            if (IsVisible)
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