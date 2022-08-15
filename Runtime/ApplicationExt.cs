using UnityEditor;
using UnityEngine;

namespace Dythervin.Core
{
    public static class ApplicationExt
    {
        public static bool IsQuitting { get; private set; }
        public static event System.Action OnEnterPlayMode;
#if UNITY_EDITOR
        public static bool IsPlaying { get; private set; }
#else
        public const bool IsPlaying = true;
#endif

#if UNITY_EDITOR
        static ApplicationExt()
        {
            EditorApplication.playModeStateChanged += ApplicationOnPlayModeStateChanged_Editor;
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void EnterPlayMode()
        {
            Application.quitting += () => IsQuitting = true;
            IsQuitting = false;
#if UNITY_EDITOR
            EnterPlayMode_Editor();
#endif
        }

#if UNITY_EDITOR
        private static void EnterPlayMode_Editor()
        {
            IsPlaying = true;
            OnEnterPlayMode?.Invoke();
            OnEnterPlayMode = null;
        }


        private static void ApplicationOnPlayModeStateChanged_Editor(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.EnteredEditMode)
                IsPlaying = false;
        }
#endif
    }
}