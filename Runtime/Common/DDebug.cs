using System;
using System.Diagnostics;
using Dythervin.Core.Utils;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Dythervin.Core
{
    public static class DDebug
    {

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.unityLogger"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        public static ILogger unityLogger => Debug.unityLogger;

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.DrawLine(Vector3, Vector3, Color, float)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
        {
            Debug.DrawLine(start, end, color, duration, true);
        }

        /// <summary>
        /// </summary>        // ReSharper disable Unity.PerformanceAnalysis
        /// <inheritdoc cref="UnityEngine.Debug.DrawLine(Vector3, Vector3, Color)"/>
        [Conditional(Symbols.DDebug)]
        public static void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            Debug.DrawLine(start, end, color, 0, true);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.DrawLine(Vector3, Vector3)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void DrawLine(Vector3 start, Vector3 end)
        {
            Debug.DrawLine(start, end, Color.white, 0, true);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.DrawRay(Vector3, Vector3, Color, float)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
        {
            Debug.DrawRay(start, dir, color, duration, true);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.DrawRay(Vector3, Vector3, Color)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void DrawRay(Vector3 start, Vector3 dir, Color color)
        {
            float duration = 0.0f;
            Debug.DrawRay(start, dir, color, duration, true);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.DrawRay(Vector3, Vector3)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void DrawRay(Vector3 start, Vector3 dir)
        {
            float duration = 0.0f;
            Color white = Color.white;
            Debug.DrawRay(start, dir, white, duration, true);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.DrawRay(Vector3, Vector3, Color, float)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void DrawRay(Vector3 start, Vector3 dir,
            [UnityEngine.Internal.DefaultValue("Color.white")] Color color,
            [UnityEngine.Internal.DefaultValue("0.0f")] float duration,
            [UnityEngine.Internal.DefaultValue("true")] bool depthTest)
        {
            Debug.DrawLine(start, start + dir, color, duration, true);
            // ReSharper disable Unity.PerformanceAnalysis
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.Log(object)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void Log(LogType logType, object message)
        {
            unityLogger.Log(logType, message);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.Log(object, Object)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void Log(LogType logType, object message, Object context)
        {
            unityLogger.Log(logType, message, context);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.Log(object)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void Log(object message)
        {
            unityLogger.Log(LogType.Log, message);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.Log(object, Object)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void Log(object message, Object context)
        {
            unityLogger.Log(LogType.Log, message, context);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogFormat(string, object[])"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void LogFormat(string format, params object[] args)
        {
            unityLogger.LogFormat(LogType.Log, format, args);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogFormat(Object, string, object[])"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void LogFormat(Object context, string format, params object[] args)
        {
            unityLogger.LogFormat(LogType.Log, context, format, args);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogFormat(LogType, LogOption, Object, string, object[])"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format,
            params object[] args)
        {
            Debug.LogFormat(logType, logOptions, context, format, args);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogError(object)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void LogError(object message)
        {
            unityLogger.Log(LogType.Error, message);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogError(object, Object)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void LogError(object message, Object context)
        {
            unityLogger.Log(LogType.Error, message, context);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogErrorFormat(string, object[])"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void LogErrorFormat(string format, params object[] args)
        {
            unityLogger.LogFormat(LogType.Error, format, args);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogErrorFormat(Object, string, object[])"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void LogErrorFormat(Object context, string format, params object[] args)
        {
            unityLogger.LogFormat(LogType.Error, context, format, args);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogException(Exception)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void LogException(Exception exception)
        {
            unityLogger.LogException(exception, null);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogException(Exception, Object)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void LogException(Exception exception, Object context)
        {
            unityLogger.LogException(exception, context);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogWarning(object)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void LogWarning(object message)
        {
            unityLogger.Log(LogType.Warning, message);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogWarning(object, Object)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void LogWarning(object message, Object context)
        {
            unityLogger.Log(LogType.Warning, message, context);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogWarningFormat(string, object[])"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void LogWarningFormat(string format, params object[] args)
        {
            unityLogger.LogFormat(LogType.Warning, format, args);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogWarningFormat(string, object[])"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.DDebug)]
        public static void LogWarningFormat(Object context, string format, params object[] args)
        {
            unityLogger.LogFormat(LogType.Warning, context, format, args);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.Assert(bool)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.UNITY_ASSERTIONS)]
        [Conditional(Symbols.DDebug)]
        public static void Assert(bool condition)
        {
            if (condition)
                return;

            unityLogger.Log(LogType.Assert, "Assertion failed");
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.Assert(bool, Object)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.UNITY_ASSERTIONS)]
        [Conditional(Symbols.DDebug)]
        public static void Assert(bool condition, Object context)
        {
            if (condition)
                return;

            unityLogger.Log(LogType.Assert, (object)"Assertion failed", context);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.Assert(bool, object)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.UNITY_ASSERTIONS)]
        [Conditional(Symbols.DDebug)]
        public static void Assert(bool condition, object message)
        {
            if (condition)
                return;

            unityLogger.Log(LogType.Assert, message);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.Assert(bool, string)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.UNITY_ASSERTIONS)]
        [Conditional(Symbols.DDebug)]
        public static void Assert(bool condition, string message)
        {
            if (condition)
                return;

            unityLogger.Log(LogType.Assert, message);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.Assert(bool, object, Object)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.UNITY_ASSERTIONS)]
        [Conditional(Symbols.DDebug)]
        public static void Assert(bool condition, object message, Object context)
        {
            if (condition)
                return;

            unityLogger.Log(LogType.Assert, message, context);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.Assert(bool, string, Object)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.UNITY_ASSERTIONS)]
        [Conditional(Symbols.DDebug)]
        public static void Assert(bool condition, string message, Object context)
        {
            if (condition)
                return;

            unityLogger.Log(LogType.Assert, (object)message, context);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.AssertFormat(bool, string, object[])"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.UNITY_ASSERTIONS)]
        [Conditional(Symbols.DDebug)]
        public static void AssertFormat(bool condition, string format, params object[] args)
        {
            if (condition)
                return;

            unityLogger.LogFormat(LogType.Assert, format, args);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.AssertFormat(bool, Object, string, object[])"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.UNITY_ASSERTIONS)]
        [Conditional(Symbols.DDebug)]
        public static void AssertFormat(bool condition, Object context, string format, params object[] args)
        {
            if (condition)
                return;

            unityLogger.LogFormat(LogType.Assert, context, format, args);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogAssertion(object)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.UNITY_ASSERTIONS)]
        [Conditional(Symbols.DDebug)]
        public static void LogAssertion(object message)
        {
            unityLogger.Log(LogType.Assert, message);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogAssertion(object, Object)"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.UNITY_ASSERTIONS)]
        [Conditional(Symbols.DDebug)]
        public static void LogAssertion(object message, Object context)
        {
            unityLogger.Log(LogType.Assert, message, context);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogAssertionFormat(string, object[])"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.UNITY_ASSERTIONS)]
        [Conditional(Symbols.DDebug)]
        public static void LogAssertionFormat(string format, params object[] args)
        {
            unityLogger.LogFormat(LogType.Assert, format, args);
        }

        /// <summary>
        /// <inheritdoc cref="UnityEngine.Debug.LogAssertionFormat(Object, string, object[])"/>
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional(Symbols.UNITY_ASSERTIONS)]
        [Conditional(Symbols.DDebug)]
        public static void LogAssertionFormat(Object context, string format, params object[] args)
        {
            unityLogger.LogFormat(LogType.Assert, context, format, args);
        }
    }
}