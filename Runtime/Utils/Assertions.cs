using System;
using System.Diagnostics;
using UnityEngine.Assertions;

namespace Dythervin.Core.Utils
{
    public static class Assertions
    {
        [Conditional(Symbols.UNITY_EDITOR)]
        public static void AssertPlayMode()
        {
#if UNITY_EDITOR
            if (!ApplicationExt.IsPlaying)
                throw new Exception("Can be called in play mode only");
#endif
        }

        public static void AssertMainThread()
        {
            if (!ThreadExt.IsMain)
                throw new Exception("Can be called on main thread only");
        }

        public static void AssertIsNotQuitting()
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
    }
}