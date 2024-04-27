using System.Threading;
using UnityEngine;

namespace Dythervin
{
    public static class ThreadHelper
    {
        private static Thread _mainThread;

        public static bool IsMain => _mainThread == Thread.CurrentThread;

        [RuntimeInitializeOnLoadMethod]
        private static void EnterPlayMode()
        {
            _mainThread = Thread.CurrentThread;
        }
    }
}