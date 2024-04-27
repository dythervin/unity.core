using System;

namespace Dythervin
{
    public class ObjectPoolAuto<T> :
        ObjectPoolBase<T>
        where T : class, new()
    {
        public ObjectPoolAuto(Action<T>? onRelease = null, Action<T>? onReturn = null, int capacity = DefaultCapacity,
            int maxSize = DefaultMaxSize) : base(onReturn: onReturn,
            onRelease: onRelease, capacity: capacity, maxSize: maxSize) { }

        protected override T Instantiate()
        {
            return new T();
        }
    }
}