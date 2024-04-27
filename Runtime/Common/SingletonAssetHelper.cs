using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[assembly: InternalsVisibleTo("Dythervin.Editor")]
namespace Dythervin
{
    public static class SingletonAssetHelper
    {
        public static bool TryLoad<T>([NotNullWhen(true)] out T? instance)
            where T : UnityEngine.Object
        {
            Type type = typeof(T);
            if (!type.TryGetCustomAttribute(out SingletonAssetPathAttribute attribute))
            {
                instance = null;
                return false;
            }

            return attribute.TryLoad(out instance);
        }

        public static T Load<T>()
            where T : UnityEngine.Object
        {
            if (TryLoad(out T? instance))
                return instance;

            throw new Exception($"Failed to load {typeof(T).FullName}");
        }

        [Conditional(Symbols.UNITY_EDITOR)]
        internal static void TryCreateIfNotExist<T>(SingletonAssetPathAttribute attribute)
            where T : UnityEngine.Object
        {
            if (attribute.TryGetFullPath(out string fullPath))
                TryCreateIfNotExist<T>(fullPath);
        }

        [Conditional(Symbols.UNITY_EDITOR)]
        internal static void TryCreateIfNotExist<T>(string fullPath)
            where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            if (!File.Exists(fullPath))
            {
                T? instance;
                UnityEngine.Object? mainAsset;
                Type type = typeof(T);
                if (type.Is(typeof(UnityEngine.Object)))
                {
                    instance = UnityEngine.ScriptableObject.CreateInstance(type) as T;
                    mainAsset = instance;
                }
                else if (type.Is(typeof(GameObject)))
                {
                    instance = new GameObject(Path.GetFileName(fullPath)) as T;
                    mainAsset = instance;
                }
                else if (type.Is(typeof(Component)))
                {
                    GameObject gameObject = new GameObject(Path.GetFileName(fullPath));
                    instance = gameObject.AddComponent(type) as T;
                    mainAsset = gameObject;
                }
                else
                {
                    throw new Exception($"Type {type.Name} is not supported");
                }

                if (!mainAsset)
                    throw new Exception($"Failed to create {type.Name}");

                string? assetFolderPath = Path.GetDirectoryName(fullPath);
                if (assetFolderPath != null && !Directory.Exists(assetFolderPath))
                {
                    Directory.CreateDirectory(new DirectoryInfo(assetFolderPath).FullName);
                }

                AssetDatabase.CreateAsset(mainAsset, fullPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Logger.Debug.Log($"{fullPath} Created");
            }
#endif
        }
    }
}