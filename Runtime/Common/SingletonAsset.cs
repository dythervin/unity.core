using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using Dythervin.Core.Extensions;
using Dythervin.Core.Utils;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting;
#if ODIN_INSPECTOR && UNITY_EDITOR
#endif

namespace Dythervin.Core
{
    using SO = ScriptableObject;

    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public abstract class SingletonAsset<T, TImpl> : SO
        where T : class
        where TImpl : SingletonAsset<T, TImpl>, T, new()
    {
        private static bool _loaded;

        private static TImpl _instance;

        private static readonly string Path = GetPath;

        private static readonly string Name = $"{typeof(TImpl).Name}";

        private static readonly string AssetFolderPath = $"Assets/Resources/{Path}";

        private static readonly string FullPath = $"{AssetFolderPath}/{Name}.asset";

        private static readonly string FullResourcesPath = $"{Path}/{Name}";

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

        internal static T InstanceChecked
        {
            get
            {
                Load();
                return Instance;
            }
        }

        private static string GetPath
        {
            get
            {
                var attribute = typeof(TImpl).GetCustomAttribute<SingletonAssetAttribute>();
                return attribute != null ? attribute.resourcePath : SingletonAssetAttribute.DefaultPath;
            }
        }

        private static TImpl LoadAtPath =>
            Resources.Load<TImpl>(FullResourcesPath); // ReSharper disable Unity.PerformanceAnalysis

        protected static void TryLoad()
        {
            if (_loaded)
                return;

            Load();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        internal static void Load()
        {
#if UNITY_EDITOR
            if (!File.Exists(FullPath))
            {
                var instance = CreateInstance<TImpl>();
                if (!Directory.Exists(AssetFolderPath))
                    Directory.CreateDirectory(new DirectoryInfo(AssetFolderPath).FullName);

                if (!instance)
                    return;

                AssetDatabase.CreateAsset(instance, FullPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                instance.OnCreated();
                DDebug.Log($"{typeof(TImpl).Name} Created");
                return;
            }
#endif
            SetInstance(LoadAtPath);
        }

        protected virtual void OnCreated()
        {
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
            else
            {
                if (EditorUtility.IsPersistent(_instance))
                {
                    string currentPath = AssetDatabase.GetAssetPath(this);
                    if (string.IsNullOrEmpty(currentPath) || currentPath == FullPath)
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

    internal sealed class SingletonAsset : SingletonAsset<SingletonAsset>
    {
#if ODIN_INSPECTOR && UNITY_EDITOR
        [ReadOnly]
#endif
        [Preserve]
        [SerializeField]
        private SO[] singletons;

#if UNITY_EDITOR
        [UnityEditor.Callbacks.DidReloadScripts]
        [MenuItem("Tools/SingletonAssets")]
#if ODIN_INSPECTOR && UNITY_EDITOR
        [Button]
#endif
        private static void Resolve()
        {
            InstanceChecked.singletons = TypeHelper.ScriptableObjects
                .Get(type => type.IsInstantiatable() && !type.IsEnum && !type.IsPrimitive &&
                             typeof(SingletonAsset<,>).IsSubclassOfRawGeneric(type)).Select(type =>
                    (SO)type.GetProperty(nameof(InstanceChecked),
                        BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic |
                        BindingFlags.FlattenHierarchy).GetValue(null)).ToArray();

            Instance.Dirty();
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void OnRuntimeMethodLoad()
        {
            TryLoad();
        }
    }
}