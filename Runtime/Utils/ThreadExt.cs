using System.Threading;
using UnityEngine;

namespace Dythervin.Core.Utils
{
    public static class ThreadExt
    {
        private static Thread _mainThread;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void EnterPlayMode()
        {
            _mainThread = Thread.CurrentThread;
        }

        public static bool IsMain => _mainThread == Thread.CurrentThread;
    }
}