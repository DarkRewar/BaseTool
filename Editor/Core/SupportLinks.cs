using UnityEditor;
using UnityEngine;

namespace BaseTool.Editor
{
    public static class SupportLinks
    {
        [MenuItem("Window/BaseTool/Documentation", false, 100)]
        public static void OpenDocumentation()
        {
            Application.OpenURL("https://github.com/DarkRewar/BaseTool?tab=readme-ov-file#documentation");
        }

        [MenuItem("Window/BaseTool/Report a bug...", false, 101)]
        public static void OpenIssues()
        {
            Application.OpenURL("https://github.com/DarkRewar/BaseTool/issues");
        }
    }
}
