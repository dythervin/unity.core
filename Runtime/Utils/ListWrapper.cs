using System.Collections;
using System.Collections.Generic;

namespace Dythervin
{
    public readonly struct ListWrapper<T> : IList<T>, IReadOnlyList<T>
    {
        private readonly IReadOnlyList<T> _list;

        public int IndexOf(T item)
        {
            throw new System.NotSupportedException();
        }

        public void Insert(int index, T item)
        {
            throw new System.NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotSupportedException();
        }

        public T this[int index]
        {
            get => _list[index];
            set => throw new System.NotSupportedException();
        }

        public void Add(T item)
        {
            throw new System.NotSupportedException();
        }

        public void Clear()
        {
            throw new System.NotSupportedException();
        }

        public bool Contains(T item)
        {
            throw new System.NotSupportedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new System.NotSupportedException();
        }

        public bool Remove(T item)
        {
            throw new System.NotSupportedException();
        }

        public int Count => _list.Count;

        public bool IsReadOnly => true;

        public ListWrapper(IReadOnlyList<T> list)
        {
            _list = list;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}