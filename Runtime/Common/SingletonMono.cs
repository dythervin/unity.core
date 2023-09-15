using Dythervin.Core.Utils;
using UnityEngine;

namespace Dythervin.Core
{
    public abstract class SingletonMonoBase<T> : MonoBehaviour
        where T : SingletonMonoBase<T>
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
                DDebug.LogWarning($"Trying to instantiate second instance of {typeof(T)}");
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

    public abstract class SingletonMono<T, TImp> : SingletonMonoBase<TImp>
        where T : class
        where TImp : SingletonMono<T, TImp>, T
    {
        public static T Instance
        {
            get
            {
                if (!Initialized)
                    SetInstance(FindObjectOfType<TImp>());

                return instance;
            }
        }
    }

    public class SingletonMono<T> : SingletonMono<T, T>
        where T : SingletonMono<T, T>
    {
    }

    [AddComponentMenu("")]
    public abstract class SingletonMonoAuto<T> : SingletonMonoBase<T>
        where T : SingletonMonoAuto<T>
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
                DAssert.IsNotQuitting();

                var gameObject = new GameObject(typeof(T).Name);
                SetInstance(gameObject.AddComponent<T>());
                if (instance.IsDestroyableOnLoad)
                    DontDestroyOnLoad(gameObject);
            }
        }
    }
}