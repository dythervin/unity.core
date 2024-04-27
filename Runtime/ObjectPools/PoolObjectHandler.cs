using System;

namespace Dythervin
{
    public struct PoolObjectHandler<T> : IDisposable
        where T : class
    {
        public bool IsActive { get; private set; }
        private T? _obj;
        private readonly IObjectPool<T?> _objectPool;

        public readonly T? Object
        {
            get
            {
                AssertNotDisposed();
                return _obj;
            }
        }

        public PoolObjectHandler(T? obj, IObjectPool<T?> objectPool)
        {
            _obj = obj;
            _objectPool = objectPool;
            IsActive = true;
        }

        public void Dispose()
        {
            AssertNotDisposed();
            IsActive = false;
            _objectPool.Return(ref _obj);
        }

        private readonly void AssertNotDisposed()
        {
            if (!IsActive)
                throw new ObjectDisposedException("Handler");
        }
    }
}