using System.IO;
using UnityEditor;

namespace BaseTool.UI
{
    internal static class UICustomScriptTemplates
    {
        private static string ViewClassScriptTemplate => "ScriptTemplates/80-BaseTool__UI__View Class-NewViewClass.cs.txt";

        internal static string GetCurrentFilePath()
        {
            var assets = AssetDatabase.FindAssets($"t:Script {nameof(UICustomScriptTemplates)}");
            var path = AssetDatabase.GUIDToAssetPath(assets[0]);
            return path.BeforeLast($"/{nameof(UICustomScriptTemplates)}.cs");
        }

        [MenuItem("Assets/Create/BaseTool/UI/View Class", priority = 50)]
        internal static void CreateViewClass()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
                Path.Combine(GetCurrentFilePath(), ViewClassScriptTemplate),
                "NewViewClass.cs");
        }
    }
}
