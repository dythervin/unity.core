using UnityEditor;

namespace Dythervin.Core.Editor
{
    public abstract class EditorPref<T>
    {
        public readonly string key;
        protected readonly T defaultValue;

        protected EditorPref(string key)
        {
            this.key = key;
        }

        protected EditorPref(string key, T defaultValue) : this(key)
        {
            this.defaultValue = defaultValue;
        }

        public abstract T Value { get; set; }

        public static implicit operator T(EditorPref<T> pref)
        {
            return pref.Value;
        }
    }

    public sealed class EditorPrefBool : EditorPref<bool>
    {
        public EditorPrefBool(string key) : base(key) { }
        public EditorPrefBool(string key, bool defaultValue) : base(key, defaultValue) { }

        public override bool Value
        {
            get => EditorPrefs.GetBool(key, defaultValue);
            set => EditorPrefs.SetBool(key, value);
        }
    }

    public sealed class EditorPrefInt : EditorPref<int>
    {
        public EditorPrefInt(string key) : base(key) { }
        public EditorPrefInt(string key, int defaultValue) : base(key, defaultValue) { }

        public override int Value
        {
            get => EditorPrefs.GetInt(key, defaultValue);
            set => EditorPrefs.SetInt(key, value);
        }
    }

    public sealed class EditorPrefFloat : EditorPref<float>
    {
        public EditorPrefFloat(string key) : base(key) { }
        public EditorPrefFloat(string key, float defaultValue) : base(key, defaultValue) { }

        public override float Value
        {
            get => EditorPrefs.GetFloat(key, defaultValue);
            set => EditorPrefs.SetFloat(key, value);
        }
    }

    public sealed class EditorPrefString : EditorPref<string>
    {
        public EditorPrefString(string key) : base(key) { }
        public EditorPrefString(string key, string defaultValue) : base(key, defaultValue) { }

        public override string Value
        {
            get => EditorPrefs.GetString(key, defaultValue);
            set => EditorPrefs.SetString(key, value);
        }
    }
}