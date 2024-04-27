using UnityEngine;

namespace Dythervin
{
    public static partial class Logger
    {
        /// <summary>
        ///     <inheritdoc cref="UnityEngine.Debug.unityLogger" />
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        public static ILogger UnityLogger => UnityEngine.Debug.unityLogger;
    }
}