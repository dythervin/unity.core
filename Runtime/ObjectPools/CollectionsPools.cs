using System.Collections.Generic;
using System.Text;

namespace Dythervin
{
    public static class SharedPools
    {
        public static readonly SharedPool<StringBuilder> StringBuilder;

        static SharedPools()
        {
            StringBuilder = SharedPool<StringBuilder>.Instance;
            StringBuilder.OnReturn += builder => builder.Clear();
        }
    }

    public static class CollectionsPools<TCollection, T>
        where TCollection : class, ICollection<T>, new()
    {
        public static readonly SharedPool<TCollection> Shared;

        static CollectionsPools()
        {
            Shared = SharedPool<TCollection>.Instance;
            Shared.OnReturn += collection => collection.Clear();
        }
    }

    public static class ListPools
    {
        public static DisposeHandler<(List<T> obj, IObjectPool<List<T>> pool)> Get<T>(out List<T> value)
        {
            return ListPools<T>.Shared.Get(out value);
        }

        public static void Return<T>(ref List<T> value)
        {
            ListPools<T>.Shared.Return(ref value);
        }

        public static void Return<T>(List<T> value)
        {
            ListPools<T>.Shared.Return(value);
        }
    }

    public static class ListPools<T>
    {
        public static readonly SharedPool<List<T>> Shared = CollectionsPools<List<T>, T>.Shared;
    }

    public static class HashSetPools
    {
        public static DisposeHandler<(HashSet<T> obj, IObjectPool<HashSet<T>> pool)> Get<T>(
            out HashSet<T> value)
        {
            return HashSetPools<T>.Shared.Get(out value);
        }

        public static void Return<T>(ref HashSet<T> value)
        {
            HashSetPools<T>.Shared.Return(ref value);
        }

        public static void Return<T>(HashSet<T> value)
        {
            HashSetPools<T>.Shared.Return(value);
        }
    }

    public static class HashSetPools<T>
    {
        public static readonly SharedPool<HashSet<T>> Shared = CollectionsPools<HashSet<T>, T>.Shared;
    }

    public static class DictionaryPools
    {
        public static DisposeHandler<(Dictionary<TKey, TValue> obj, IObjectPool<Dictionary<TKey, TValue>> pool)> Get<TKey, TValue>(out Dictionary<TKey, TValue> value)
        {
            return DictionaryPools<TKey, TValue>.Shared.Get(out value);
        }

        public static void Return<TKey, TValue>(ref Dictionary<TKey, TValue> value)
        {
            DictionaryPools<TKey, TValue>.Shared.Return(ref value);
        }

        public static void Return<TKey, TValue>(Dictionary<TKey, TValue> value)
        {
            DictionaryPools<TKey, TValue>.Shared.Return(value);
        }
    }

    public static class DictionaryPools<TKey, TValue>
    {
        public static readonly SharedPool<Dictionary<TKey, TValue>> Shared =
            CollectionsPools<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>.Shared;
    }
}