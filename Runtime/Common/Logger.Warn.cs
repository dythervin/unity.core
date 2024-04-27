using System.Diagnostics;
using UnityEngine;

namespace Dythervin
{
    public static partial class Logger
    {
        public static class Warn
        {
            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogWarning(object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.WARN)]
            [HideInCallstack]
            public static void Log(object message)
            {
                UnityLogger.Log(LogType.Warning, message);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogWarning(object, Object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.WARN)]
            [HideInCallstack]
            public static void Log(object message, Object context)
            {
                UnityLogger.Log(LogType.Warning, message, context);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogWarningFormat(string, object[])"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.WARN)]
            [HideInCallstack]
            public static void LogFormat(string format, params object[] args)
            {
                UnityLogger.LogFormat(LogType.Warning, format, args);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogWarningFormat(string, object[])"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.WARN)]
            [HideInCallstack]
            public static void LogFormat(Object context, string format, params object[] args)
            {
                UnityLogger.LogFormat(LogType.Warning, context, format, args);
            }
        }
    }
}