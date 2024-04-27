using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;

namespace Dythervin.Editor
{
    [AttributeUsage(AttributeTargets.Class)]
    [Conditional(Symbols.UNITY_EDITOR)]
    public class EditorSingletonAssetAttribute : SingletonAssetPathAttribute
    {
        /// <summary>
        /// </summary>
        /// <param name="path">Relative to assets folder</param>
        public EditorSingletonAssetAttribute(string path) : base(path.RemoveStart("Assets/"))
        {
        }

        public override bool TryGetFullPath(out string fullPath)
        {
            fullPath = Path.Combine("Assets", path + ".asset");
            return true;
        }

        protected override bool TryLoadImpl<T>(out T instance)
        {
            Type type = typeof(T);
            if (!type.TryGetCustomAttribute(out EditorSingletonAssetAttribute attribute))
            {
                instance = null;
                return false;
            }

            if (attribute.TryGetFullPath(out string fullPath))
            {
                SingletonAssetHelper.TryCreateIfNotExist<T>(fullPath);
                instance = AssetDatabase.LoadAssetAtPath<T>(fullPath);
                return instance;
            }

            instance = null;
            return false;
        }
    }
}