using System.Diagnostics;
using Dythervin.Core.Utils;
using Object = UnityEngine.Object;

namespace Dythervin.Core
{
    public static class DLogger
    {
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.LOGGING)]
        public static void Log(object message, Object context)
        {
            UnityEngine.Debug.Log(message, context);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.LOGGING)]
        public static void Log(object message)
        {
            UnityEngine.Debug.Log(message);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.LOGGING)]
        public static void LogWarning(object message, Object context)
        {
            UnityEngine.Debug.LogWarning(message, context);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.LOGGING)]
        public static void LogWarning(object message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.LOGGING)]
        public static void LogError(object message, Object context)
        {
            UnityEngine.Debug.LogError(message, context);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.LOGGING)]
        public static void LogError(object message)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
}