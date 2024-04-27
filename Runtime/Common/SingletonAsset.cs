using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;

namespace Dythervin
{
    using SO = ScriptableObject;

    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public abstract class SingletonAsset<T, TImpl> : SO
        where T : class
        where TImpl : SingletonAsset<T, TImpl>, T, new()
    {
        private static bool _loaded;
        private static TImpl _instance;
        private bool _initialized;

        public static T Instance
        {
            get
            {
                TryLoad();
                if (!_instance._initialized)
                    _instance.Init();

                return _instance;
            }
        }

        protected static void TryLoad()
        {
            if (_loaded)
                return;

            if (SingletonAssetHelper.TryLoad(out TImpl obj))
            {
                SetInstance(obj);
            }
        }

        protected void OnEnable()
        {
            if (_instance == this)
            {
                OnEnabled();
                return;
            }

            if (_instance == null)
            {
                SetInstance((TImpl)this);
                OnEnabled();
            }
#if UNITY_EDITOR
            else if (typeof(TImpl).TryGetCustomAttribute(out SingletonAssetPathAttribute attribute))
            {
                if (EditorUtility.IsPersistent(_instance))
                {
                    if (attribute.TryGetFullPath(out var fullPath))
                    {
                        string currentPath = AssetDatabase.GetAssetPath(this);
                        if (string.IsNullOrEmpty(currentPath) || currentPath == fullPath)
                        {
                            EditorUtility.CopySerialized(_instance, this);
                            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(_instance));
                        }
                        else
                        {
                            EditorUtility.CopySerialized(this, _instance);
                            if (EditorUtility.IsPersistent(this))
                                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(this));
                            else
                                DestroyImmediate(this);
                        }
                    }
                }
                else
                {
                    DestroyImmediate(_instance);
                }
            }
#endif
        }

        protected virtual void OnEnabled()
        {
        }

        /// <summary>
        ///     Called on OnEnable or before instance returned, depending on what happened first
        /// </summary>
        protected virtual void Init()
        {
            _initialized = true;
        }

        protected virtual void OnDisable()
        {
            _initialized = false;
            if (_instance == (TImpl)this)
                SetInstance(null);
        }

        private static void SetInstance(TImpl value)
        {
            _instance = value;
            _loaded = value;
            if (_loaded && !value._initialized)
                value.Init();
        }
    }

    public class SingletonAsset<TObject> : SingletonAsset<TObject, TObject>
        where TObject : SingletonAsset<TObject, TObject>, new()
    {
    }

//     internal sealed class SingletonAsset : SingletonAsset<SingletonAsset>
//     {
// #if ODIN_INSPECTOR
//         [ReadOnly]
// #endif
//         [Preserve]
//         [SerializeField]
//         private SO[] singletons;
//
// #if UNITY_EDITOR
//         [UnityEditor.Callbacks.DidReloadScripts]
//         [MenuItem("Tools/SingletonAssets")]
// #if ODIN_INSPECTOR
//         [Button]
// #endif
//         private static void Resolve()
//         {
//             InstanceChecked.singletons = TypeHelper.ScriptableObjects
//                 .Get(type => type.IsInstantiatable() && !type.IsEnum && !type.IsPrimitive &&
//                              typeof(SingletonAsset<,>).IsSubclassOfRawGeneric(type)).Select(type =>
//                     (SO)type.GetProperty(nameof(InstanceChecked),
//                         BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic |
//                         BindingFlags.FlattenHierarchy).GetValue(null)).ToArray();
//
//             Instance.Dirty();
//         }
// #endif
//
//         [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
//         private static void OnRuntimeMethodLoad()
//         {
//             TryLoad();
//         }
//     }
}