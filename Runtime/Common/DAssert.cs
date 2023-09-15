using System;
using System.Diagnostics;
using UnityEngine.Assertions;

namespace Dythervin.Core.Utils
{
    public static class DAssert
    {
        [Conditional(Symbols.UNITY_EDITOR)]
        public static void PlayMode()
        {
#if UNITY_EDITOR
            if (!ApplicationExt.IsPlaying)
                throw new Exception("Can be called in play mode only");
#endif
        }

        public static void MainThread()
        {
            if (!ThreadExt.IsMain)
                throw new Exception("Can be called on main thread only");
        }

        public static void IsNotQuitting()
        {
            if (ApplicationExt.IsQuitting)
                throw new Exception("Cannot be called while quitting play mode");
        }

        public static void IsNotNull(object value, string message = "")
        {
            if (value == null || value is UnityEngine.Object obj && obj == null)
                throw new AssertionException("Value is null", message);
        }

        public static void IsNull(object value, string message = "")
        {
            if (value == null || value is UnityEngine.Object obj && obj == null)
                return;

            throw new AssertionException("Value is not null", message);
        }
        
        public static void IsTrue(bool value, string message = "")
        {
            if (!value)
                throw new AssertionException("Value is not null", message);
        }
    }
}