using System;
using System.Diagnostics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Dythervin
{
    public static partial class Logger
    {
        public static class Error
        {
            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogError(object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.ERROR)]
            [HideInCallstack]
            public static void Log(object message)
            {
                UnityLogger.Log(LogType.Error, message);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogError(object, UnityEngine.Object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.ERROR)]
            [HideInCallstack]
            public static void Log(object message, Object context)
            {
                UnityLogger.Log(LogType.Error, message, context);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogErrorFormat(string, object[])"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.ERROR)]
            [HideInCallstack]
            public static void LogFormat(string format, params object[] args)
            {
                UnityLogger.LogFormat(LogType.Error, format, args);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogErrorFormat(Object, string, object[])"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.ERROR)]
            [HideInCallstack]
            public static void LogFormat(Object context, string format, params object[] args)
            {
                UnityLogger.LogFormat(LogType.Error, context, format, args);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogException(Exception)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.ERROR)]
            [HideInCallstack]
            public static void LogException(Exception exception)
            {
                UnityLogger.LogException(exception, null);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogException(Exception, Object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.ERROR)]
            [HideInCallstack]
            public static void LogException(Exception exception, Object context)
            {
                UnityLogger.LogException(exception, context);
            }
        }
    }
}