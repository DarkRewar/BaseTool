using System.IO;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

namespace BaseTool
{
    [InitializeOnLoad]
    public static class ConsoleInitializer
    {
        public static string DefaultSettingsPath =>
            Path.Combine("Assets", "Resources");

        static ConsoleInitializer()
        {
            var settingsGuids = AssetDatabase.FindAssets($"t:{nameof(ConsoleSettings)}");
            if (settingsGuids.Length != 0) return;

            if (!Directory.Exists(DefaultSettingsPath))
            {
                Directory.CreateDirectory(DefaultSettingsPath);
            }

            var settings = ScriptableObject.CreateInstance<ConsoleSettings>();
            var path = Path.Combine(DefaultSettingsPath, $"{ConsoleSettings.DefaultSettingsFile}.asset");

            AssetDatabase.CreateAsset(settings, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.LogWarning($"No {nameof(ConsoleSettings)} found. One was created at {path}");
        }
    }
}