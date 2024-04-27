using UnityEngine;

namespace Dythervin
{
    public sealed class SharedPool<T> : ObjectPoolAuto<T>
        where T : class, new()
    {
        public static readonly SharedPool<T> Instance = new SharedPool<T>();

        private SharedPool() : base()
        {
            Application.lowMemory += () => Instance.Clear(0.7f);
        }
    }
}