using System;

namespace Dythervin.Core.Utils
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonAssetAttribute : Attribute
    {
        public const string DefaultPath = "Global";
        public readonly string resourcePath;

        public SingletonAssetAttribute() : this(DefaultPath) { }

        /// <summary>
        /// </summary>
        /// <param name="resourcePath">Relative to resources folder</param>
        public SingletonAssetAttribute(string resourcePath)
        {
            this.resourcePath = resourcePath;
        }
    }
}