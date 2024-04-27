using System;
using System.Collections.Generic;

namespace Dythervin
{
    public static class EnumerableArrayHandlerExt
    {
        /// <summary>
        ///     Disposed in its enumerator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static PooledEnumerableArrayHandler<T> ToTempEnumerableArray<T>(this IReadOnlyList<T> list)
        {
            return new(list);
        }

        /// <summary>
        ///     Disposed in its enumerator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static PooledEnumerableArrayHandler<T> ToTempEnumerableArray<T>(this HashSet<T> list)
        {
            return new(list);
        }
    }

    public struct PooledEnumerableArrayHandler<T>
    {
        private T[] _tempArray;
        private readonly int _count;

        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        public readonly int Count => _count;

        internal PooledEnumerableArrayHandler(IReadOnlyList<T> list) : this(list.Count)
        {
            for (int i = 0; i < _count; i++)
            {
                _tempArray[i] = list[i];
            }
        }

        private PooledEnumerableArrayHandler(int count)
        {
            _count = count;
            _tempArray = ArrayPool.Get<T>(count, false);
        }

        public PooledEnumerableArrayHandler(HashSet<T> hashSet) : this(hashSet.Count)
        {
            int i = 0;
            foreach (T value in hashSet)
            {
                _tempArray[i++] = value;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new(this);
        }

        public struct Enumerator : IDisposableExt
        {
            private PooledEnumerableArrayHandler<T> _self;
            private int _index;

            public T Current
            {
                get
                {
                    if (_index < 0 || _index > _self._count)
                    {
                        throw new ArgumentOutOfRangeException(nameof(_index));
                    }

                    return _self._tempArray[_index];
                }
            }

            public bool IsDisposed => _self._tempArray == null;

            public Enumerator(PooledEnumerableArrayHandler<T> self)
            {
                _self = self;
                _index = -1;
            }

            public bool MoveNext()
            {
                _index++;
                return _index < _self._count;
            }

            public void Reset()
            {
                _index = -1;
            }

            public void Dispose()
            {
                if (IsDisposed)
                    return;

                if (_self._tempArray != null)
                {
                    ArrayPool.Return(ref _self._tempArray);
                }
            }
        }
    }
}