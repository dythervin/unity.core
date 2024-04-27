using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dythervin
{
    public static class TypeHelper
    {
        private static Assembly[]? _allAssemblies;
        private static Type[]? _allTypesArray;
        private static Type[]? _nonUnityObjects;
        private static Type[]? _unityObjects;
        private static Type[]? _monoBehaviours;
        private static Type[]? _scriptableObjects;

        public static IReadOnlyList<Type> AllTypes =>
            _allTypesArray ??= AllAssemblies.SelectMany(assembly => assembly.GetTypes()).ToArray();

        public static IReadOnlyList<Assembly> AllAssemblies =>
            _allAssemblies ??= AppDomain.CurrentDomain.GetAssemblies();

        public static IReadOnlyList<Type> NonUnityObjects =>
            _nonUnityObjects ??= AllTypes.Where(type => !type.Is(typeof(UnityEngine.Object))).ToArray();

        public static IReadOnlyList<Type> UnityObjects =>
            _unityObjects ??= AllTypes.Where(type => type.Is(typeof(UnityEngine.Object))).ToArray();

        public static IReadOnlyList<Type> MonoBehaviours =>
            _monoBehaviours ??= UnityObjects.Where(type => type.Is(typeof(UnityEngine.MonoBehaviour))).ToArray();

        public static IReadOnlyList<Type> ScriptableObjects =>
            _scriptableObjects ??= UnityObjects.Where(type => type.Is(typeof(UnityEngine.ScriptableObject))).ToArray();

#if UNITY_EDITOR
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnCompile()
        {
            _allAssemblies = null;
            _allTypesArray = null;
            _nonUnityObjects = null;
            _unityObjects = null;
            _monoBehaviours = null;
            _scriptableObjects = null;
        }
#endif
    }
}