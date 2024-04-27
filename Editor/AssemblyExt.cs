using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Dythervin.Editor
{
    public static class AssemblyExt
    {
        private static readonly char[] Slashes = { '/', '\\' };

        [Pure]
        public static UnityEditorInternal.AssemblyDefinitionAsset? GetAssemblyDefinitionAsset(in string folderPath)
        {
            var validPaths = new HashSet<string>();
            {
                string path = folderPath;
                do
                {
                    int index = path.LastIndexOfAny(Slashes);
                    if (index == -1)
                    {
                        break;
                    }

                    path = path.Substring(0, index);
                    validPaths.Add(path);
                } while (true);
            }

            string? assemblyPath = AssetDatabase.FindAssets("t:" + nameof(UnityEditorInternal.AssemblyDefinitionAsset))
                .Select(AssetDatabase.GUIDToAssetPath).Where(x => validPaths.Contains(Path.GetDirectoryName(x)))
                .OrderByDescending(x => x.Length).FirstOrDefault();

            if (assemblyPath == null)
            {
                return null;
            }

            return AssetDatabase.LoadAssetAtPath<UnityEditorInternal.AssemblyDefinitionAsset>(
                AssetDatabase.GUIDToAssetPath(assemblyPath));
        }

        [Pure]
        public static Assembly GetAssembly(this UnityEditorInternal.AssemblyDefinitionAsset assemblyDefinitionAsset)
        {
            if (assemblyDefinitionAsset == null)
            {
                throw new ArgumentNullException(nameof(assemblyDefinitionAsset));
            }

            return Assembly.Load(assemblyDefinitionAsset.GetData().name);
        }

        [Pure]
        public static AssemblyDefinitionAssetData GetData(
            this UnityEditorInternal.AssemblyDefinitionAsset assemblyDefinitionAsset)
        {
            AssemblyDefinitionAssetData definitionAssetData =
                JsonUtility.FromJson<AssemblyDefinitionAssetData>(assemblyDefinitionAsset.text);

            if (string.IsNullOrWhiteSpace(definitionAssetData.name))
                throw new Exception("Assembly name is empty");

            return definitionAssetData;
        }
    }

    [Serializable]
    public struct AssemblyDefinitionAssetData
    {
        public string name;
        public string? rootNamespace;
        
        public string[] references;

        public string[] includePlatforms;
        public string[] excludePlatforms;
        public bool allowUnsafeCode;
        public bool overrideReferences;
        public string[] precompiledReferences;
        public bool autoReferenced;
        public string[] defineConstraints;
        //public VersionDefine[] versionDefines;
        public bool noEngineReferences;
    }
}