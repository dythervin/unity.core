using System;
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

        ///Defined when building scripts with support for C# 7.3 or newer.
        public const string CSHARP_7_3_OR_NEWER = nameof(CSHARP_7_3_OR_NEWER);

        ///Scripting backend #define for Mono.
        public const string ENABLE_MONO = nameof(ENABLE_MONO);

        ///Scripting backend #define for IL2CPP.
        public const string ENABLE_IL2CPP = nameof(ENABLE_IL2CPP);

        ///Defined when the target build platform supports VR. Does not imply that VR is currently enabled or that the necessary plug-ins and packages needed to support VR are installed.
        public const string ENABLE_VR = nameof(ENABLE_VR);

        ///Defined when building scripts against .NET 2.0 API compatibility level on Mono and IL2CPP.
        public const string NET_2_0 = nameof(NET_2_0);

        ///Defined when building scripts against .NET 2.0 Subset API compatibility level on Mono and IL2CPP.
        public const string NET_2_0_SUBSET = nameof(NET_2_0_SUBSET);

        ///Defined when building scripts against .NET 2.0 or .NET 2.0 Subset API compatibility level on Mono and IL2CPP.
        public const string NET_LEGACY = nameof(NET_LEGACY);

        ///Defined when building scripts against .NET 4.x API compatibility level on Mono and IL2CPP.
        public const string NET_4_6 = nameof(NET_4_6);

        ///Defined when building scripts against .NET Standard 2.0 API compatibility level on Mono and IL2CPP.
        public const string NET_STANDARD_2_0 = nameof(NET_STANDARD_2_0);

        ///Defined when building scripts against .NET Standard 2.1 API compatibility level on Mono and IL2CPP.
        public const string NET_STANDARD_2_1 = nameof(NET_STANDARD_2_1);

        ///Defined when building scripts against .NET Standard 2.1 API compatibility level on Mono and IL2CPP.
        public const string NET_STANDARD = nameof(NET_STANDARD);

        ///Defined when building scripts against .NET Standard 2.1 API compatibility level on Mono and IL2CPP.
        public const string NETSTANDARD2_1 = nameof(NETSTANDARD2_1);

        ///Defined when building scripts against .NET Standard 2.1 API compatibility level on Mono and IL2CPP.
        public const string NETSTANDARD = nameof(NETSTANDARD);

        ///Defined when Windows Runtime support is enabled on IL2CPP. See Windows Runtime Support for more details.
        public const string ENABLE_WINMD_SUPPORT = nameof(ENABLE_WINMD_SUPPORT);

        ///Defined when the Input System package is enabled in Player Settings.
        public const string ENABLE_INPUT_SYSTEM = nameof(ENABLE_INPUT_SYSTEM);

        ///Defined when the legacy Input Manager is enabled in Player Settings.
        public const string ENABLE_LEGACY_INPUT_MANAGER = nameof(ENABLE_LEGACY_INPUT_MANAGER);

        ///Defined when the Server Build setting is enabled in Build Settings
        public const string UNITY_SERVER = nameof(UNITY_SERVER);

        ///Defined when your script is running in a player which was built with the “Development Build” option enabled.
        public const string DEVELOPMENT_BUILD = nameof(DEVELOPMENT_BUILD);

        public const string DDebug = nameof(DDebug);

        [Conditional(UNITY_EDITOR)]
        public static void AddSymbol(string define)
        {
#if UNITY_EDITOR
            if (EditorUserBuildSettings.selectedBuildTargetGroup == BuildTargetGroup.Unknown)
                throw new Exception("EditorUserBuildSettings.selectedBuildTargetGroup is Unknown");

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