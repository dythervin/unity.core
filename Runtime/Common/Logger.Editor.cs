using System;
using System.Diagnostics;
using System.Linq;
using UnityEditor;

namespace Dythervin
{
    public static partial class Logger
    {
        private const string MenuItemVerbosityError = "Debug/Verbosity/Error";
        private const string MenuItemVerbosityWarn = "Debug/Verbosity/Warn";
        private const string MenuItemVerbosityInfo = "Debug/Verbosity/Info";
        private const string MenuItemVerbosityDebug = "Debug/Verbosity/Debug";
        private const string MenuItemVerbosityDebugHeavy = "Debug/Verbosity/DebugHeavy";

        [Conditional(Symbols.UNITY_EDITOR)]
        public static void SetLogVerbosity(LogVerbosity verbosity)
        {
#if UNITY_EDITOR
            var symbols = Symbols.GetSymbols().ToHashSet();
            symbols.Remove(Symbols.LOG_VERBOSITY.ERROR);
            symbols.Remove(Symbols.LOG_VERBOSITY.WARN);
            symbols.Remove(Symbols.LOG_VERBOSITY.INFO);
            symbols.Remove(Symbols.LOG_VERBOSITY.DEBUG);
            symbols.Remove(Symbols.LOG_VERBOSITY.DEBUG_HEAVY);
            switch (verbosity)
            {
                case LogVerbosity.Error:
                    symbols.Add(Symbols.LOG_VERBOSITY.ERROR);
                    break;

                case LogVerbosity.Warn:
                    symbols.Add(Symbols.LOG_VERBOSITY.WARN);
                    goto case LogVerbosity.Error;

                case LogVerbosity.Info:
                    symbols.Add(Symbols.LOG_VERBOSITY.INFO);
                    goto case LogVerbosity.Warn;

                case LogVerbosity.Debug:
                    symbols.Add(Symbols.LOG_VERBOSITY.DEBUG);
                    goto case LogVerbosity.Info;

                case LogVerbosity.DebugHeavy:
                    symbols.Add(Symbols.LOG_VERBOSITY.DEBUG_HEAVY);
                    goto case LogVerbosity.Debug;

                default:
                    throw new ArgumentOutOfRangeException(nameof(verbosity), verbosity, null);
            }

            Symbols.SetSymbols(symbols.ToArray());
#endif
        }

        public static LogVerbosity GetLogVerbosity()
        {
            var symbols = Symbols.GetSymbols().ToHashSet();
            if (symbols.Contains(Symbols.LOG_VERBOSITY.DEBUG_HEAVY))
                return LogVerbosity.DebugHeavy;

            if (symbols.Contains(Symbols.LOG_VERBOSITY.DEBUG))
                return LogVerbosity.Debug;

            if (symbols.Contains(Symbols.LOG_VERBOSITY.INFO))
                return LogVerbosity.Info;

            if (symbols.Contains(Symbols.LOG_VERBOSITY.WARN))
                return LogVerbosity.Warn;

            if (symbols.Contains(Symbols.LOG_VERBOSITY.ERROR))
                return LogVerbosity.Error;

            return LogVerbosity.None;
        }
#if UNITY_EDITOR

        [MenuItem(MenuItemVerbosityError, priority = (int)LogVerbosity.Error)]
        private static void SetVerbosityError()
        {
            SetLogVerbosity(LogVerbosity.Error);
        }

        [MenuItem(MenuItemVerbosityError, true)]
        private static bool SetVerbosityErrorValidate()
        {
            Menu.SetChecked(MenuItemVerbosityError, GetLogVerbosity() >= LogVerbosity.Error);
            return true;
        }

        [MenuItem(MenuItemVerbosityWarn, priority = (int)LogVerbosity.Warn)]
        private static void SetVerbosityWarn()
        {
            SetLogVerbosity(LogVerbosity.Warn);
        }

        [MenuItem(MenuItemVerbosityWarn, true)]
        private static bool SetVerbosityWarnValidate()
        {
            Menu.SetChecked(MenuItemVerbosityWarn, GetLogVerbosity() >= LogVerbosity.Warn);
            return true;
        }

        [MenuItem(MenuItemVerbosityInfo, priority = (int)LogVerbosity.Info)]
        private static void SetVerbosityInfo()
        {
            SetLogVerbosity(LogVerbosity.Info);
        }

        [MenuItem(MenuItemVerbosityInfo, true)]
        private static bool SetVerbosityInfoValidate()
        {
            Menu.SetChecked(MenuItemVerbosityInfo, GetLogVerbosity() >= LogVerbosity.Info);
            return true;
        }

        [MenuItem(MenuItemVerbosityDebug, priority = (int)LogVerbosity.Debug)]
        private static void SetVerbosityDebug()
        {
            SetLogVerbosity(LogVerbosity.Debug);
        }

        [MenuItem(MenuItemVerbosityDebug, true)]
        private static bool SetVerbosityDebugValidate()
        {
            Menu.SetChecked(MenuItemVerbosityDebug, GetLogVerbosity() >= LogVerbosity.Debug);
            return true;
        }

        [MenuItem(MenuItemVerbosityDebugHeavy, priority = (int)LogVerbosity.DebugHeavy)]
        private static void SetVerbosityDebugHeavy()
        {
            SetLogVerbosity(LogVerbosity.DebugHeavy);
        }

        [MenuItem(MenuItemVerbosityDebugHeavy, true)]
        private static bool SetVerbosityDebugHeavyValidate()
        {
            Menu.SetChecked(MenuItemVerbosityDebugHeavy, GetLogVerbosity() >= LogVerbosity.DebugHeavy);
            return true;
        }
#endif
    }
}