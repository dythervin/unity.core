using System;

namespace Dythervin
{
    public class ObjectPool<T> : ObjectPoolBase<T>
        where T : class
    {
        private readonly Func<T> _createFunc;

        public ObjectPool(Func<T> createFunc, Action<T>? onReturn = null, Action<T>? onRelease = null,
            int capacity = DefaultCapacity, int maxSize = DefaultMaxSize) : base(onReturn: onReturn,
            onRelease: onRelease,
            capacity: capacity,
            maxSize: maxSize)
        {
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
        }

        protected override T Instantiate()
        {
            T? t = _createFunc();
            if (t is null)
            {
                throw new NullReferenceException("CreateFunc returned null");
            }

            return t;
        }
    }
}