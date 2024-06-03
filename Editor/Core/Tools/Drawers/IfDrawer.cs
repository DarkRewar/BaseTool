using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using BaseTool.Editor.Tools.Drawers;
using Unity.Properties;
using UnityEditor;
using UnityEngine;

namespace BaseTool.Tools.Drawers
{
    //[CustomPropertyDrawer(typeof(IfAttribute))]
    public class IfDrawer : BaseToolPropertyDrawer
    {
        public override void OnDrawGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //var editor = UnityEditor.Editor.CreateEditor(property., typeof(Cooldown));
            // if (property.propertyType == SerializedPropertyType.Generic)
            // {
            //     PropertyDrawer legitDrawer = AppDomain.CurrentDomain.GetAssemblies()
            //         .SelectMany(a => a.GetTypes())
            //         .Where(t => t == typeof(PropertyDrawer))
            //         .Where(t => IsDrawerFor(t, property.type))
            //         .Select(t => );

            //     if (legitDrawer != null)
            //     {
            //         legitDrawer.OnGUI(position, property, label);
            //         return;
            //     }
            // }

            using (new EditorGUI.DisabledScope(disabled: false))
            {
                EditorGUILayout.PropertyField(property, label, false);
            }
        }

        private bool IsDrawerFor(Type t, string typeName)
        {
            return false;
        }
    }
}