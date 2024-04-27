using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Dythervin
{
    public static partial class CollectionsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAdd<T>(this ICollection<T> list, T value)
        {
            int count = list.Count;
            list.Add(value);
            return count != list.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRemove<T>(this ICollection<T> list, T value)
        {
            int count = list.Count;
            list.Remove(value);
            return count != list.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetLast<T>(this T[] array)
        {
            return ref array[array.Length - 1];
        }

        public static bool Pop<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
        {
            value = dictionary[key];
            return dictionary.Remove(key);
        }

        public static TValue Pop<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value = dictionary[key];
            dictionary.Remove(key);
            return value;
        }

        public static bool Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            return dictionary.Remove(new KeyValuePair<TKey, TValue>(key, value));
        }

#if !NETSTANDARD2_1_OR_GREATER
        public static bool Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
        {
            return CollectionExtensions.Remove(dictionary, key, out value);
        }

        public static bool TryPop<T>(this Stack<T> stack, out T value)
        {
            bool has = stack.Count > 0;
            value = has ? stack.Pop() : default;
            return has;
        }
#endif
    }
}