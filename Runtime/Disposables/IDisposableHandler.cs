using System;

namespace Dythervin
{
    public interface IDisposableHandler
    {
        void Add(IDisposable? disposable);
    }
}