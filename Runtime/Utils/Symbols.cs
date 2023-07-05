using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;

namespace Dythervin.Core.Utils
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class Symbols
    {
        ///Scripting symbol to call Unity Editor scripts from your game code.
        public const string UNITY_EDITOR = nameof(UNITY_EDITOR);
        ///Scripting symbol for Editor code on Windows.
        public const string UNITY_EDITOR_WIN = nameof(UNITY_EDITOR_WIN);
        ///Scripting symbol for Editor code on Mac OS X.
        public const string UNITY_EDITOR_OSX = nameof(UNITY_EDITOR_OSX);
        ///Scripting symbol for Editor code on Linux.
        public const string UNITY_EDITOR_LINUX = nameof(UNITY_EDITOR_LINUX);
        ///Scripting symbol to compile or execute code specifically for Mac OS X (including Universal, PPC and Intel architectures).
        public const string UNITY_STANDALONE_OSX = nameof(UNITY_STANDALONE_OSX);
        ///Scripting symbol for compiling/executing code specifically for Windows standalone applications.
        public const string UNITY_STANDALONE_WIN = nameof(UNITY_STANDALONE_WIN);
        ///Scripting symbol for compiling/executing code specifically for Linux standalone applications.
        public const string UNITY_STANDALONE_LINUX = nameof(UNITY_STANDALONE_LINUX);
        ///Scripting symbol for compiling/executing code for any standalone platform (Mac OS X, Windows or Linux).
        public const string UNITY_STANDALONE = nameof(UNITY_STANDALONE);
        ///Scripting symbol for compiling/executing code for the Wii console.
        public const string UNITY_WII = nameof(UNITY_WII);
        ///Scripting symbol for compiling/executing code for the iOS platform.
        public const string UNITY_IOS = nameof(UNITY_IOS);
        ///Scripting symbol for the Android platform.
        public const string UNITY_ANDROID = nameof(UNITY_ANDROID);
        ///Scripting symbol for the Magic Leap OS platform. You can also use PLATFORM_LUMIN. Note that the Lumin platform is no longer supported.
        public const string UNITY_LUMIN = nameof(UNITY_LUMIN);
        ///Scripting symbol for the Tizen platform.
        public const string UNITY_TIZEN = nameof(UNITY_TIZEN);
        ///Scripting symbol for the Apple TV platform.
        public const string UNITY_TVOS = nameof(UNITY_TVOS);
        ///Scripting symbol for Universal Windows Platform. Additionally, NETFX_CORE is defined when compiling C# files against .NET Core and using .NET scripting backend.
        public const string UNITY_WSA = nameof(UNITY_WSA);
        ///Scripting symbol for Universal Windows Platform. Additionally WINDOWS_UWP is defined when compiling C# files against .NET Core.
        public const string UNITY_WSA_10_0 = nameof(UNITY_WSA_10_0);
        ///Scripting symbol for WebGL.
        public const string UNITY_WEBGL = nameof(UNITY_WEBGL);
        ///Scripting symbol for the Facebook platform (WebGL or Windows standalone).
        public const string UNITY_FACEBOOK = nameof(UNITY_FACEBOOK);
        ///Scripting symbol for calling Unity Analytics methods from your game code. Version 5.2 and above.
        public const string UNITY_ANALYTICS = nameof(UNITY_ANALYTICS);
        ///Scripting symbol for assertions control process.
        public const string UNITY_ASSERTIONS = nameof(UNITY_ASSERTIONS);
        ///Scripting symbol for 64-bit platforms.
        public const string UNITY_64 = nameof(UNITY_64);
        
        public const string LOGGING = nameof(LOGGING);

        [Conditional(UNITY_EDITOR)]
        public static void AddSymbol(string define)
        {
#if UNITY_EDITOR
            string defines =
                PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

            if (defines.Contains(define))
                return;

            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
                $"{defines};{define}");
#endif
        }
    }
}