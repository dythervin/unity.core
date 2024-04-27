using System;

namespace Dythervin
{
    public interface IPoolable : IPoolableInner
    {
        void ReturnToPool();
    }

    public interface IPoolableInner
    {
        protected internal void OnCreatedFromPool(IObjectPool returnToPool);

        protected internal void OnReturnToPool();
    }
}