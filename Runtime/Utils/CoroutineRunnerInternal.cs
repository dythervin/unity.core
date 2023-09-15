using System.Collections;
using UnityEngine;

namespace Dythervin.Core.Utils
{
    internal class CoroutineRunnerInternal : SingletonMonoAuto<CoroutineRunnerInternal>
    {
    }

    public static class CoroutineRunner
    {
        public static void StartCoroutine(IEnumerator enumerator)
        {
            CoroutineRunnerInternal.Instance.StartCoroutine(enumerator);
        }

        public static void StopCoroutine(IEnumerator enumerator)
        {
            CoroutineRunnerInternal.Instance.StopCoroutine(enumerator);
        }

        public static void StopCoroutine(Coroutine coroutine)
        {
            CoroutineRunnerInternal.Instance.StopCoroutine(coroutine);
        }
    }
}