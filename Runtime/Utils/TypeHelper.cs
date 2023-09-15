using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dythervin.Core.Extensions;

namespace Dythervin.Core.Utils
{
    public static class TypeHelper
    {
        private static readonly List<Type> Buffer = new List<Type>();
        
        private static Assembly[] _allAssemblies;
        private static Type[] _allTypesArray;
        private static Type[] _nonUnityObjects;
        private static Type[] _unityObjects;
        private static Type[] _monoBehaviours;
        private static Type[] _scriptableObjects;

        public static IReadOnlyList<Type> AllTypes =>
            _allTypesArray ??= AllAssemblies.SelectMany(assembly => assembly.GetTypes()).ToArray();

        public static IReadOnlyList<Assembly> AllAssemblies =>
            _allAssemblies ??= AppDomain.CurrentDomain.GetAssemblies();

        public static IReadOnlyList<Type> NonUnityObjects =>
            _nonUnityObjects ??= Get(type => !type.Implements(typeof(UnityEngine.Object)));

        public static IReadOnlyList<Type> UnityObjects =>
            _unityObjects ??= Get(type => type.Implements(typeof(UnityEngine.Object)));

        public static IReadOnlyList<Type> MonoBehaviours =>
            _monoBehaviours ??= Get(UnityObjects, type => type.Implements(typeof(UnityEngine.MonoBehaviour)));

        public static IReadOnlyList<Type> ScriptableObjects =>
            _scriptableObjects ??= Get(UnityObjects, type => type.Implements(typeof(UnityEngine.ScriptableObject)));

        public static Type[] Get(Predicate<Type> filter)
        {
            return Get(AllTypes, filter);
        }

        public static Type[] Get(this IReadOnlyList<Type> list, Predicate<Type> filter)
        {
            Buffer.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                Type type = list[i];
                if (filter(type))
                    Buffer.Add(type);
            }

            var array = Buffer.ToArray();
            Buffer.Clear();
            return array;
        }
    }
}