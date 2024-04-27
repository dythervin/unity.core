using System;
using System.Diagnostics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Dythervin
{
    public static partial class Logger
    {
        public static class Debug
        {
            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.DrawLine(Vector3, Vector3, Color, float)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
            {
                UnityEngine.Debug.DrawLine(start, end, color, duration, true);
            }

            /// <summary>
            /// </summary>        // ReSharper disable Unity.PerformanceAnalysis
            /// <inheritdoc cref="UnityEngine.Debug.DrawLine(Vector3, Vector3, Color)"/>
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void DrawLine(Vector3 start, Vector3 end, Color color)
            {
                UnityEngine.Debug.DrawLine(start, end, color, 0, true);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.DrawLine(Vector3, Vector3)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void DrawLine(Vector3 start, Vector3 end)
            {
                UnityEngine.Debug.DrawLine(start, end, Color.white, 0, true);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.DrawRay(Vector3, Vector3, Color, float)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
            {
                UnityEngine.Debug.DrawRay(start, dir, color, duration, true);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.DrawRay(Vector3, Vector3, Color)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void DrawRay(Vector3 start, Vector3 dir, Color color)
            {
                float duration = 0.0f;
                UnityEngine.Debug.DrawRay(start, dir, color, duration, true);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.DrawRay(Vector3, Vector3)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void DrawRay(Vector3 start, Vector3 dir)
            {
                float duration = 0.0f;
                Color white = Color.white;
                UnityEngine.Debug.DrawRay(start, dir, white, duration, true);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.DrawRay(Vector3, Vector3, Color, float)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void DrawRay(Vector3 start, Vector3 dir,
                [UnityEngine.Internal.DefaultValue("Color.white")] Color color,
                [UnityEngine.Internal.DefaultValue("0.0f")] float duration,
                [UnityEngine.Internal.DefaultValue("true")] bool depthTest)
            {
                UnityEngine.Debug.DrawLine(start, start + dir, color, duration, true);
                // ReSharper disable Unity.PerformanceAnalysis
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.Log(object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void Log(LogType logType, object message)
            {
                UnityLogger.Log(logType, message);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.Log(object, UnityEngine.Object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void Log(LogType logType, object message, Object context)
            {
                UnityLogger.Log(logType, message, context);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.Log(object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void Log(object message)
            {
                UnityLogger.Log(LogType.Log, message);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.Log(object, Object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void Log(object message, Object context)
            {
                UnityLogger.Log(LogType.Log, message, context);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogFormat(string, object[])"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void LogFormat(string format, params object[] args)
            {
                UnityLogger.LogFormat(LogType.Log, format, args);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogFormat(Object, string, object[])"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void LogFormat(Object context, string format, params object[] args)
            {
                UnityLogger.LogFormat(LogType.Log, context, format, args);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogFormat(LogType, LogOption, Object, string, object[])"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format,
                params object[] args)
            {
                UnityEngine.Debug.LogFormat(logType, logOptions, context, format, args);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogError(object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void LogError(object message)
            {
                UnityLogger.Log(LogType.Error, message);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogError(object, UnityEngine.Object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void LogError(object message, Object context)
            {
                UnityLogger.Log(LogType.Error, message, context);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogErrorFormat(string, object[])"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void LogErrorFormat(string format, params object[] args)
            {
                UnityLogger.LogFormat(LogType.Error, format, args);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogErrorFormat(Object, string, object[])"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void LogErrorFormat(Object context, string format, params object[] args)
            {
                UnityLogger.LogFormat(LogType.Error, context, format, args);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogException(Exception)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void LogException(Exception exception)
            {
                UnityLogger.LogException(exception, null);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogException(Exception, Object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void LogException(Exception exception, Object context)
            {
                UnityLogger.LogException(exception, context);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogWarning(object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void LogWarning(object message)
            {
                UnityLogger.Log(LogType.Warning, message);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogWarning(object, Object)"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void LogWarning(object message, Object context)
            {
                UnityLogger.Log(LogType.Warning, message, context);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogWarningFormat(string, object[])"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void LogWarningFormat(string format, params object[] args)
            {
                UnityLogger.LogFormat(LogType.Warning, format, args);
            }

            /// <summary>
            /// <inheritdoc cref="UnityEngine.Debug.LogWarningFormat(string, object[])"/>
            /// </summary>
            // ReSharper disable Unity.PerformanceAnalysis
            [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
            [HideInCallstack]
            public static void LogWarningFormat(Object context, string format, params object[] args)
            {
                UnityLogger.LogFormat(LogType.Warning, context, format, args);
            }
        }
    }
}