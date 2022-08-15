using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;

namespace Dythervin.Core.Utils
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class Symbols
    {
        public const string UNITY_EDITOR = "UNITY_EDITOR";
        public const string DEVELOPMENT_BUILD = "DEVELOPMENT_BUILD";


        [Conditional(UNITY_EDITOR)]
        public static void AddSymbol(string define)
        {
#if UNITY_EDITOR
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            if (defines.Contains(define))
                return;

            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, $"{defines};{define}");
#endif
        }
    }
}