using System;

namespace Dythervin
{
    public interface IObjectPool : IDisposableExt
    {
        int CountInactive { get; }

        int CountActive { get; }

        void Return(object element);
        
        bool TryReturn(object? element);

        void Clear(float percent = 1);

        IObjectPool EnsureObjCount(int count);

        object Get();
    }

    public interface IObjectPoolOut<out T> : IObjectPool
        where T : class
    {
        event Action<T> OnCreated;

        event Action<T> OnReturn;

        event Action<T> OnRelease;

        new T Get();
    }

    public interface IObjectPool<T> : IObjectPoolOut<T>
        where T : class
    {
        void Return(T obj);

        bool TryReturn(T? element);
    }
}