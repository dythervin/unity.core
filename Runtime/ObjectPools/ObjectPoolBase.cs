using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Dythervin
{
    public abstract class ObjectPoolBase<T> : IObjectPool<T>
        where T : class
    {
        public const int DefaultMaxSize = 1024;
        public const int DefaultCapacity = 16;
        private static readonly bool IsIPoolable = typeof(T).Is(typeof(IPoolable));

        public event Action<T>? OnCreated;

        public event Action<T>? OnRelease;

        public event Action<T>? OnReturn;

        private readonly Action<IPoolable> _returnToPool;
        private int _maxSize;

        //key - object, value - null if object is in use, otherwise - object
        private readonly ConditionalWeakTable<T?, object?> _state = new();
        private int _instanced;
        private bool _isInitialized;

        public bool IsDisposed { get; private set; }

        private readonly Stack<T?> _stack;

        public int CountActive => _instanced - _stack.Count;

        public int CountInactive => _stack.Count;

        public int MaxSize
        {
            get
            {
                Initialize();
                return _maxSize;
            }
            set
            {
                Initialize();
                if (_maxSize <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Max size must be greater than 0");
                }

                _maxSize = value;
            }
        }

        protected ObjectPoolBase(Action<T>? onRelease = null, Action<T>? onReturn = null,
            int capacity = DefaultCapacity, int maxSize = DefaultMaxSize)
        {
            _stack = new(capacity);
            if (maxSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxSize));
            }

            _maxSize = maxSize;
            OnReturn = onReturn;
            OnRelease = onRelease;
            _returnToPool = poolable => Return((T)poolable);
        }

        public void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;
            OnInitialized();
        }

        public virtual IObjectPool EnsureObjCount(int count)
        {
            Initialize();
            if (count <= 0 || count > _maxSize)
            {
                throw new ArgumentOutOfRangeException();
            }

            while (_stack.Count < count)
            {
                Return(GetNew());
            }

            return this;
        }

        public void Dispose()
        {
            if (IsDisposed || !_isInitialized)
            {
                return;
            }

            OnDispose();
            IsDisposed = true;
        }

        protected virtual void OnDispose()
        {
            Clear();
            OnCreated = null;
            OnRelease = null;
            OnReturn = null;
            _state.Clear();
        }

        public void Clear(float percent = 1)
        {
            this.AssertNotDisposed();

            int count = Mathf.Clamp(Mathf.RoundToInt(_stack.Count * percent), 0, _stack.Count);
            for (int i = 0; i < count; i++)
            {
                T? obj = _stack.Pop();
                if (obj != null)
                {
                    ReleaseObj(obj);
                }
            }
        }

        public virtual T Get()
        {
            this.AssertNotDisposed();
            T? obj;

            do
            {
            } while (_stack.TryPop(out obj) && obj == null);

            obj ??= GetNew();
            OnGot(obj);
            return obj;
        }

        public void Return(T element)
        {
            this.AssertNotDisposed();
            Initialize();
            DAssert.IsNotNull(element, "Trying to return null object to the pool");

            if (_state.TryGetValue(element, out object? value) && value != null)
            {
                throw new InvalidOperationException("Trying to release an object that is not in use.");
            }

            ReturnInternal(element);
        }

        public bool TryReturn(T? element)
        {
            this.AssertNotDisposed();
            Initialize();
            if (element == null)
            {
                return false;
            }

            if (_state.TryGetValue(element, out object? value) && value != null)
            {
                return false;
            }

            ReturnInternal(element);
            return true;
        }

        protected virtual void OnInitialized()
        {
        }

        private void OnGot(T obj)
        {
            if (!_state.TryGetValue(obj, out object? value))
            {
                throw new InvalidOperationException("Trying to get an object that is not registered in the pool");
            }

            if (value == null)
            {
                throw new InvalidOperationException("Trying to get an object that is already in use");
            }

            _state.AddOrUpdate(obj, null!);
        }

        private void OnObjectReturned(T element)
        {
            var actionOnRelease = OnReturn;
            actionOnRelease?.Invoke(element);
        }

        protected T GetNew()
        {
            Initialize();
            T obj = Instantiate();
            if (obj is IPoolable poolable)
            {
                poolable.OnCreatedFromPool(this);
            }

            _state.Add(obj, obj);
            ++_instanced;
            var onCreated = OnCreated;
            onCreated?.Invoke(obj);
            return obj;
        }

        protected abstract T Instantiate();

        private void ReleaseObj(T element)
        {
            var onRelease = OnRelease;
            onRelease?.Invoke(element);
            _instanced--;
        }

        object IObjectPool.Get()
        {
            return Get();
        }

        void IObjectPool.Return(object element)
        {
            Return((T)element);
        }

        bool IObjectPool.TryReturn(object? element)
        {
            if (element is T t)
                return TryReturn(t);

            return false;
        }

        private void ReturnInternal(T element)
        {
            OnObjectReturned(element);

            if (_stack.Count < _maxSize)
            {
                _state.AddOrUpdate(element, element);
                _stack.Push(element);
                if (element is IPoolable poolable)
                {
                    poolable.OnReturnToPool();
                }
            }
            else
            {
                _state.Remove(element);
                ReleaseObj(element);
            }
        }
    }
}