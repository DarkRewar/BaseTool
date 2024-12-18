﻿using UnityEditor;
using UnityEngine;

namespace BaseTool.Tools.Drawers
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    internal class ReadOnlyDrawer : PropertyDrawer
    {
        private bool _displayed;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _displayed = position.height > 1f;
            if (!_displayed) return;

            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_displayed)
                return base.GetPropertyHeight(property, label);
            return 0;
        }
    }
}
