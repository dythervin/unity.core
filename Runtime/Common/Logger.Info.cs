using System.Diagnostics;
using UnityEngine;

namespace Dythervin
{
    public static partial class Logger
    {
        public static partial class Info
        {
            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.Log(object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.INFO)]
            [HideInCallstack]
            public static void Log(object message)
            {
                UnityLogger.Log(LogType.Log, message);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.Log(object, Object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.INFO)]
            [HideInCallstack]
            public static void Log(object message, Object context)
            {
                UnityLogger.Log(LogType.Log, message, context);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogFormat(string, object[])"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.INFO)]
            [HideInCallstack]
            public static void LogFormat(string format, params object[] args)
            {
                UnityLogger.LogFormat(LogType.Log, format, args);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogFormat(Object, string, object[])"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.INFO)]
            [HideInCallstack]
            public static void LogFormat(Object context, string format, params object[] args)
            {
                UnityLogger.LogFormat(LogType.Log, context, format, args);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogFormat(LogType, LogOption, Object, string, object[])"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.INFO)]
            [HideInCallstack]
            public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format,
                params object[] args)
            {
                UnityEngine.Debug.LogFormat(logType, logOptions, context, format, args);
            }
        }
    }
}