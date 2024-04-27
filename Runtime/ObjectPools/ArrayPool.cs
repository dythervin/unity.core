using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Dythervin
{
    internal interface IArrayPool
    {
        void OnMaxSizeUpdated(int value);

        void Clear(float percent);
    }

    public static class ArrayPool
    {
        private const bool ExactLenghtDefault = true;
        private static int _maxSize = 1024;
        private static readonly List<IArrayPool> Pools = new();

        public static int MaxSize
        {
            get => _maxSize;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Max Size must be greater than 0", nameof(value));
                }

                if (_maxSize == value)
                {
                    return;
                }

                _maxSize = value;
                foreach (IArrayPool arrayPool in Pools)
                {
                    arrayPool.OnMaxSizeUpdated(value);
                }
            }
        }

        public static T[] Get<T>(int lenght, bool exactLenght = ExactLenghtDefault)
        {
            return Pool<T>.Instance.Get(lenght, exactLenght);
        }

        public static void Clear(float percent = 1)
        {
            if (percent > 1 || percent <= 0)
            {
                Debug.LogError("Percent must be in range (0.0; 1.0]");
                return;
            }

            foreach (IArrayPool pool in Pools)
            {
                pool.Clear(percent);
            }
        }

        public static DisposeHandler<T[]> Get<T>(int lenght, out T[] array, bool exactLenght = ExactLenghtDefault)
        {
            array = Get<T>(lenght, exactLenght);
            return new DisposeHandler<T[]>(array, obj => Pool<T>.Instance.Return(obj));
        }

        public static void Return<T>(ref T[] array)
        {
            Pool<T>.Instance.Return(array);
            array = null;
        }

        public static void Return<T>(T[] array)
        {
            Pool<T>.Instance.Return(array);
        }

        private sealed class Pool<T> : IArrayPool
        {
            internal static readonly Pool<T> Instance = new();
            private readonly Dictionary<int, Stack<T[]>> _pools = new();

            //key - object, value - null if object is in use, otherwise - object
            private readonly ConditionalWeakTable<T[], object> _state = new();
            private static readonly bool CanContainReferences = RuntimeHelpers.IsReferenceOrContainsReferences<T>();

            private Pool()
            {
                Pools.Add(this);
            }

            internal T[] Get(int lenght, bool exactLenght)
            {
                switch (lenght)
                {
                    case < 0:
                        throw new ArgumentException("Lenght must be greater than 0", nameof(lenght));
                    case 0:
                        return Array.Empty<T>();
                }

                T[] obj;
                if (_pools.TryGetValue(lenght, out var stack) && stack.Count > 0)
                {
                    obj = stack.Pop();
                    if (_state.TryGetValue(obj, out object value) && value == null)
                    {
                        throw new InvalidOperationException("Trying to get an object that is in use");
                    }
                }
                else if (exactLenght)
                {
                    obj = new T[lenght];
                }
                else
                {
                    obj = System.Buffers.ArrayPool<T>.Shared.Rent(lenght);
                }

                _state.AddOrUpdate(obj, null);
                return obj;
            }

            void IArrayPool.Clear(float percent)
            {
                foreach (var pool in _pools)
                {
                    int count = (int)(pool.Value.Count * percent);
                    for (int i = 0; i < count; i++)
                    {
                        pool.Value.Pop();
                    }
                }
            }

            // ReSharper disable Unity.PerformanceAnalysis
            internal void Return(T[] array, bool clearArray = true)
            {
                if (array == null)
                {
                    Debug.LogError("Trying to return null object.");
                    return;
                }

                if (array == Array.Empty<T>())
                    return;

                if (clearArray && CanContainReferences)
                {
                    Array.Clear(array, 0, array.Length);
                }

                if (_state.TryGetValue(array, out object value) && value != null)
                {
                    Debug.LogError("Trying to return an object that is not in use.");
                    return;
                }

                _state.AddOrUpdate(array, array);
                if (_pools.TryGetValue(array.Length, out var stack))
                {
                    if (stack.Count < ArrayPool.MaxSize)
                    {
                        stack.Push(array);
                    }
                    // else object is returned
                }
                else
                {
                    stack = new();
                    _pools.Add(array.Length, stack);
                }
            }

            void IArrayPool.OnMaxSizeUpdated(int value)
            {
                foreach (var pool in _pools.Values)
                {
                    while (pool.Count > value)
                    {
                        pool.Pop();
                    }
                }
            }
        }
    }
}