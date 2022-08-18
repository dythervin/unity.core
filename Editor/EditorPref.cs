using System.Collections.Generic;
using Dythervin.Core.Utils;
using UnityEditor;

namespace Dythervin.Core.Editor
{
    public abstract class EditorPref<T>
    {
        public readonly string key;
        private bool _loaded;
        private T _value;

        protected EditorPref(string key)
        {
            this.key = key;
            _loaded = false;
        }

        protected EditorPref(string key, T defaultValue) : this(key)
        {
            if (!ThreadExt.IsMain)
            {
                EditorApplication.delayCall += () =>
                {
                    if (!EditorPrefs.HasKey(key))
                    {
                        _value = defaultValue;
                        // ReSharper disable once VirtualMemberCallInConstructor
                        PersistentValue = defaultValue;
                    }
                };
                return;
            }

            if (EditorPrefs.HasKey(key))
            {
                _value = defaultValue;
                // ReSharper disable once VirtualMemberCallInConstructor
                PersistentValue = defaultValue;
            }
        }

        public T Value
        {
            get
            {
                TryLoad();
                return _value;
            }
            set
            {
                TryLoad();
                if (EqualityComparer<T>.Default.Equals(_value, value))
                    return;

                _value = value;
                PersistentValue = value;
            }
        }

        public abstract T PersistentValue { get; set; }

        public static implicit operator T(EditorPref<T> pref)
        {
            return pref.Value;
        }

        private void TryLoad()
        {
            if (_loaded)
                return;
            _loaded = true;
            _value = PersistentValue;
        }
    }

    public sealed class EditorPrefBool : EditorPref<bool>
    {
        public EditorPrefBool(string key) : base(key) { }
        public EditorPrefBool(string key, bool defaultValue) : base(key, defaultValue) { }

        public override bool PersistentValue
        {
            get => EditorPrefs.GetBool(key);
            set => EditorPrefs.SetBool(key, value);
        }
    }

    public sealed class EditorPrefInt : EditorPref<int>
    {
        public EditorPrefInt(string key) : base(key) { }
        public EditorPrefInt(string key, int defaultValue) : base(key, defaultValue) { }

        public override int PersistentValue
        {
            get => EditorPrefs.GetInt(key);
            set => EditorPrefs.SetInt(key, value);
        }
    }

    public sealed class EditorPrefFloat : EditorPref<float>
    {
        public EditorPrefFloat(string key) : base(key) { }
        public EditorPrefFloat(string key, float defaultValue) : base(key, defaultValue) { }

        public override float PersistentValue
        {
            get => EditorPrefs.GetFloat(key);
            set => EditorPrefs.SetFloat(key, value);
        }
    }

    public sealed class EditorPrefString : EditorPref<string>
    {
        public EditorPrefString(string key) : base(key) { }
        public EditorPrefString(string key, string defaultValue) : base(key, defaultValue) { }

        public override string PersistentValue
        {
            get => EditorPrefs.GetString(key);
            set => EditorPrefs.SetString(key, value);
        }
    }
}