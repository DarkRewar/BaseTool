using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BaseTool
{
    public static class Injector
    {
        public static void Process(MonoBehaviour go)
        {
            Type type = go.GetType();

            IEnumerable<FieldInfo> fields = type.GetFields(
                BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.Public
            ).Where(f => IsValidMember(go, f));

            foreach (FieldInfo field in fields)
            {
                var comp = ExtractFromType(go, field);
                if (comp != null)
                    field.SetValue(go, comp);
            }

            IEnumerable<PropertyInfo> properties = go.GetType().GetProperties(
                BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.SetField |
                BindingFlags.GetField
            ).Where(p => IsValidMember(go, p));

            foreach (PropertyInfo property in properties)
            {
                var comp = ExtractFromType(go, property);
                if (comp != null)
                    property.SetValue(go, comp);
            }
        }

        private static object ExtractFromType(MonoBehaviour go, MemberInfo memberInfo)
        {
            var type = memberInfo switch
            {
                FieldInfo fieldInfo => fieldInfo.FieldType,
                PropertyInfo propertyInfo => propertyInfo.PropertyType,
                _ => null
            };
            if (type == null) return null;

            if (memberInfo.IsDefined(typeof(GetComponentAttribute), false))
            {
                if (go.TryGetComponent(type, out var component))
                    return component;
                else
                    Debug.LogWarning($"Component {type} not found on {go.name}");
            }
            else if (memberInfo.IsDefined(typeof(GetComponentsAttribute), false))
            {
                var components = go.GetComponents(type);
                if (components.Length != 0)
                {
                    var arr = Array.CreateInstance(type.GetElementType(), components.Length);
                    Array.Copy(components, arr, components.Length);
                    return arr;
                }
                else
                    Debug.LogWarning($"Component {type} not found on {go.name}");
            }
            else if (memberInfo.IsDefined(typeof(GetComponentInChildrenAttribute), false))
            {
                var component = go.GetComponentInChildren(type);
                if (component)
                    return component;
                else
                    Debug.LogWarning($"Component {type} not found in {go.name} children");
            }
            else if (memberInfo.IsDefined(typeof(GetComponentsInChildrenAttribute), false))
            {
                Component[] components = go.GetComponentsInChildren(type.GetElementType());
                if (components.Length != 0)
                {
                    var arr = Array.CreateInstance(type.GetElementType(), components.Length);
                    Array.Copy(components, arr, components.Length);
                    return arr;
                }
                else
                    Debug.LogWarning($"Components {type} not found in {go.name} children");
            }
            else if (memberInfo.IsDefined(typeof(GetComponentInParentAttribute), false))
            {
                var component = go.GetComponentInParent(type);
                if (component)
                    return component;
                else
                    Debug.LogWarning($"Component {type} not found in {go.name} parent");
            }
            return null;
        }

        private static bool IsValidMember(MonoBehaviour go, FieldInfo fieldInfo) =>
            fieldInfo.IsDefined(typeof(ComponentRetrieverAttribute), true)
            && (fieldInfo.GetValue(go) == null || fieldInfo.GetValue(go).Equals(null));

        private static bool IsValidMember(MonoBehaviour go, PropertyInfo propertyInfo) =>
            propertyInfo.IsDefined(typeof(ComponentRetrieverAttribute), true)
            && (propertyInfo.GetValue(go) == null || propertyInfo.GetValue(go).Equals(null));
    }
}
