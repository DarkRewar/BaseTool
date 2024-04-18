using UnityEditor;
using UnityEngine;

namespace BaseTool.Editor
{
    public static class OpenFolders
    {
        [MenuItem("Window/BaseTool/Open Persistent Data Folder", priority = 211)]
        public static void OpenPersistentDataFolder()
        {
            EditorUtility.OpenWithDefaultApp(Application.persistentDataPath);
        }

        [MenuItem("Window/BaseTool/Open Data Folder", priority = 210)]
        public static void OpenDataFolder()
        {
            EditorUtility.OpenWithDefaultApp(Application.dataPath);
        }
    }
}