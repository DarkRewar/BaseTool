using System;
using System.Reflection;
using BaseTool.Tools.Attributes;
using UnityEngine;

namespace BaseTool.Tools
{
    public static class Injector
    {
        public static void Process(MonoBehaviour go)
        {
            Type type = go.GetType();

            System.Reflection.FieldInfo[] fields = type.GetFields(
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Public
            );

            foreach (System.Reflection.FieldInfo field in fields)
            {
                var comp = ExtractFromType(go, field);
                if (comp != null)
                    field.SetValue(go, comp);
            }

            System.Reflection.PropertyInfo[] properties = go.GetType().GetProperties(
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.SetField |
                System.Reflection.BindingFlags.GetField
            );

            foreach (System.Reflection.PropertyInfo property in properties)
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

    }
}
