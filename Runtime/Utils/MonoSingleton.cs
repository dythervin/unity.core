using UnityEngine;

namespace Dythervin.Core.Utils
{
    public abstract class MonoSingletonBase<T> : MonoBehaviour
        where T : MonoSingletonBase<T>
    {
        protected static T instance { get; private set; }

        // ReSharper disable once StaticMemberInGenericType
        protected static bool Initialized { get; private set; }

        private bool _initialized;

        protected virtual void Awake()
        {
            if (instance == this)
                return;


            if (instance == null)
            {
                SetInstance((T)this);
            }
            else
            {
                Destroy(this);
                Debug.LogWarning($"Trying to instantiate second instance of {typeof(T)}");
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
                SetInstance(null);
        }


        /// <summary>
        /// Called on awake or before instance returned, depending on what happened first
        /// </summary>
        protected virtual void Init()
        {
            _initialized = true;
        }

        protected static void SetInstance(T value)
        {
            instance = value;
            Initialized = value != null;
            if (Initialized && !value._initialized)
                value.Init();
        }
    }


    public abstract class MonoSingleton<T> : MonoSingletonBase<T>
        where T : MonoSingleton<T>
    {
        public static T Instance
        {
            get
            {
                if (!Initialized)
                    SetInstance(FindObjectOfType<T>());

                return instance;
            }
        }
    }

    [AddComponentMenu("")]
    public abstract class MonoSingletonAuto<T> : MonoSingletonBase<T>
        where T : MonoSingletonAuto<T>
    {
        protected virtual bool IsDestroyableOnLoad => true;

        public static T Instance
        {
            get
            {
                TryInit();
                return instance;
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void TryInit()
        {
            if (!Initialized)
            {
                Assertions.AssertIsNotQuitting();

                var gameObject = new GameObject(typeof(T).Name);
                SetInstance(gameObject.AddComponent<T>());
                if (instance.IsDestroyableOnLoad)
                    DontDestroyOnLoad(gameObject);
            }
        }
    }
}