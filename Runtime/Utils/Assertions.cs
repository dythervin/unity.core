using System;
using System.Diagnostics;

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
    }
}