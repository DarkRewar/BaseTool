using System;
using System.Reflection;
using System.Text;
using Codice.Utils;
using UnityEditor;
using UnityEngine;

namespace BaseTool.Editor.Core.Wizard
{
    [InitializeOnLoad]
    public static class GlobalDefineInitializer
    {
        static readonly StringBuilder _stringBuilder = new();

        public const string CoreDefine = "BASETOOL_CORE";
        public const string MovementDefine = "BASETOOL_MOVEMENT";
        public const string ShooterDefine = "BASETOOL_SHOOTER";
        public const string RPGDefine = "BASETOOL_RPG";
        public const string RogueliteDefine = "BASETOOL_ROGUELITE";
        public const string UIDefine = "BASETOOL_UI";

        static GlobalDefineInitializer()
        {
            if (HasGlobalDefine(CoreDefine)) return;

            AddGlobalDefine(CoreDefine);
            AddGlobalDefine(MovementDefine);
            AddGlobalDefine(ShooterDefine);
            AddGlobalDefine(RPGDefine);
            AddGlobalDefine(UIDefine);
        }

        /// <summary>
        /// Adds the given global define if it's not already present
        /// </summary>
        public static void AddGlobalDefine(string id)
        {
            bool added = false;
            int totGroupsModified = 0;
            BuildTargetGroup[] targetGroups = (BuildTargetGroup[])Enum.GetValues(typeof(BuildTargetGroup));
            foreach (BuildTargetGroup btg in targetGroups)
            {
                //if (btg == BuildTargetGroup.Unknown) continue;
                if (!IsValidBuildTargetGroup(btg)) continue;
                string defs = PlayerSettings.GetScriptingDefineSymbolsForGroup(btg);
                string[] singleDefs = defs.Split(';');
                if (Array.IndexOf(singleDefs, id) != -1) continue; // Already present
                added = true;
                totGroupsModified++;
                defs += defs.Length > 0 ? ";" + id : id;
                PlayerSettings.SetScriptingDefineSymbolsForGroup(btg, defs);
            }
            if (added) Debug.Log(string.Format("BaseTool : added global define \"{0}\" to {1} BuildTargetGroups", id, totGroupsModified));
        }

        /// <summary>
        /// Removes the given global define if it's present
        /// </summary>
        public static void RemoveGlobalDefine(string id)
        {
            bool removed = false;
            int totGroupsModified = 0;
            BuildTargetGroup[] targetGroups = (BuildTargetGroup[])Enum.GetValues(typeof(BuildTargetGroup));
            foreach (BuildTargetGroup btg in targetGroups)
            {
                //if (btg == BuildTargetGroup.Unknown) continue;
                if (!IsValidBuildTargetGroup(btg)) continue;
                string defs = PlayerSettings.GetScriptingDefineSymbolsForGroup(btg);
                string[] singleDefs = defs.Split(';');
                if (Array.IndexOf(singleDefs, id) == -1) continue; // Not present
                removed = true;
                totGroupsModified++;
                _stringBuilder.Length = 0;
                for (int i = 0; i < singleDefs.Length; ++i)
                {
                    if (singleDefs[i] == id) continue;
                    if (_stringBuilder.Length > 0) _stringBuilder.Append(';');
                    _stringBuilder.Append(singleDefs[i]);
                }
                PlayerSettings.SetScriptingDefineSymbolsForGroup(btg, _stringBuilder.ToString());
            }
            _stringBuilder.Length = 0;
            if (removed)
                Debug.Log($"BaseTool : removed global define \"{id}\" from {totGroupsModified} BuildTargetGroups");
        }

        /// <summary>
        /// Returns TRUE if the given global define is present in all the <see cref="BuildTargetGroup"/>
        /// or only in the given <see cref="BuildTargetGroup"/>, depending on passed parameters.<para/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="buildTargetGroup"><see cref="BuildTargetGroup"/>to use. Leave NULL to check in all of them.</param>
        public static bool HasGlobalDefine(string id, BuildTargetGroup? buildTargetGroup = null)
        {
            BuildTargetGroup[] targetGroups = buildTargetGroup == null
                ? (BuildTargetGroup[])Enum.GetValues(typeof(BuildTargetGroup))
                : new[] { (BuildTargetGroup)buildTargetGroup };
            foreach (BuildTargetGroup btg in targetGroups)
            {
                if (!IsValidBuildTargetGroup(btg)) continue;
                string defs = PlayerSettings.GetScriptingDefineSymbolsForGroup(btg);
                string[] singleDefs = defs.Split(';');
                if (Array.IndexOf(singleDefs, id) != -1) return true;
            }
            return false;
        }

        static bool IsValidBuildTargetGroup(BuildTargetGroup group)
        {
            if (group == BuildTargetGroup.Unknown) return false;
            Type moduleManager = Type.GetType("UnityEditor.Modules.ModuleManager, UnityEditor.dll");
            //            MethodInfo miIsPlatformSupportLoaded = moduleManager.GetMethod("IsPlatformSupportLoaded", BindingFlags.Static | BindingFlags.NonPublic);
            MethodInfo miGetTargetStringFromBuildTargetGroup = moduleManager.GetMethod(
                "GetTargetStringFromBuildTargetGroup", BindingFlags.Static | BindingFlags.NonPublic
            );
            MethodInfo miGetPlatformName = typeof(PlayerSettings).GetMethod(
                "GetPlatformName", BindingFlags.Static | BindingFlags.NonPublic
            );
            string targetString = (string)miGetTargetStringFromBuildTargetGroup.Invoke(null, new object[] { group });
            string platformName = (string)miGetPlatformName.Invoke(null, new object[] { group });

            // Group is valid if at least one betweeen targetString and platformName is not empty.
            // This seems to me the safest and more reliant way to check,
            // since ModuleManager.IsPlatformSupportLoaded dosn't work well with BuildTargetGroup (only BuildTarget)
            bool isValid = !string.IsNullOrEmpty(targetString) || !string.IsNullOrEmpty(platformName);

            //            Debug.Log((isValid ? "<color=#00ff00>" : "<color=#ff0000>") + group + " > " + targetString + " / " + platformName + " > "  + isValid + "/" + miIsPlatformSupportLoaded.Invoke(null, new object[] {group.ToString()}) + "</color>");
            return isValid;
        }
    }
}