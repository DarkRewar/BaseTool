using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BaseTool.Tools
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class BaseToolInspector : UnityEditor.Editor
    {
        private IEnumerable<MethodInfo> _methodsWithButton;

        private void OnEnable()
        {

            _methodsWithButton = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(m => m.GetCustomAttributes<ButtonAttribute>(true).Any());
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            foreach (var method in _methodsWithButton)
            {
                if (GUILayout.Button(method.Name))
                {
                    method.Invoke(target, null);
                }
            }

            using (var iterator = serializedObject.GetIterator())
            {
                while (iterator.NextVisible(true))
                {
                    EditorGUILayout.PropertyField(iterator, new GUIContent(iterator.displayName), true);
                }
            }
        }
    }
}
