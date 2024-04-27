using System;
using System.IO;

namespace Dythervin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ResourceAssetAttribute : SingletonAssetPathAttribute
    {
        /// <summary>
        /// </summary>
        /// <param name="resourcePath">Relative to resources folder</param>
        public ResourceAssetAttribute(string resourcePath) : base(resourcePath.RemoveStart("Assets/")
            .RemoveStart("Resources/"))
        {
        }

        public override bool TryGetFullPath(out string fullPath)
        {
            fullPath = Path.Combine("Assets/Resources", path + ".asset");
            return true;
        }

        protected override bool TryLoadImpl<T>(out T instance)
        {
            instance = UnityEngine.Resources.Load<T>(base.path);
            return instance;
        }
    }
}