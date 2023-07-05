using System.Diagnostics;
using Dythervin.Core.Utils;
using UnityEditor;
using UnityEngine;

namespace Dythervin.Core.Extensions
{
    public static class Objects
    {
        [Conditional(Symbols.UNITY_EDITOR)]
        public static void Dirty(this Object obj)
        {
#if UNITY_EDITOR
            if (obj)
                EditorUtility.SetDirty(obj);
#endif
        }

        [Conditional(Symbols.UNITY_EDITOR)]
        public static void Rename(this ScriptableObject obj, string newName)
        {
#if UNITY_EDITOR
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(obj), newName);
#endif
        }
    }
}