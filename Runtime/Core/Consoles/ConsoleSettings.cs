using UnityEngine;

namespace BaseTool
{
    public class ConsoleSettings : ScriptableObject
    {
        public const string DefaultSettingsFile = "ConsoleSettings";

        [Header("Opened settings")]
        public float OpenedTimeScale = 0;

        [Header("Build settings")]
        public bool EnableInReleaseBuild = false;

        public bool EnableInDevelopmentBuild = true;

        [Header("Toggles settings")]
        public bool UseCustomInput = false;

        [IfNot(nameof(UseCustomInput))]
        public KeyCode ToggleKeyCode = KeyCode.F4;

        [IfNot(nameof(UseCustomInput))]
        public bool ToggleKeyCodeCtrl;

        [IfNot(nameof(UseCustomInput))]
        public bool ToggleKeyCodeAlt;

        public static ConsoleSettings GetOrCreate()
        {
            var settingsArray = Resources.LoadAll<ConsoleSettings>("");
            if (settingsArray.Length == 0)
                return CreateInstance<ConsoleSettings>();
            return settingsArray[0];
        }
    }
}