using System;
using System.Diagnostics.CodeAnalysis;
using Object = UnityEngine.Object;

namespace Dythervin
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class SingletonAssetPathAttribute : Attribute
    {
        public readonly string path;

        protected SingletonAssetPathAttribute(string path)
        {
            this.path = path;
        }

        public abstract bool TryGetFullPath(out string fullPath);

        public bool TryLoad<T>([NotNullWhen(true)] out T? instance)
            where T : Object
        {
            SingletonAssetHelper.TryCreateIfNotExist<T>(this);
            return TryLoadImpl(out instance);
        }

        protected abstract bool TryLoadImpl<T>([NotNullWhen(true)] out T? instance)
            where T : Object;
    }
}