using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Dythervin.Core.Extensions
{
    public static partial class CollectionsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Reset<T>(this T[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = default;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(this T[] list, T value)
        {
            return Array.IndexOf(list, value) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOf<T>(this IReadOnlyList<T> list, T value, int startIndex = 0)
        {
            int num = startIndex + list.Count;
            for (int index = startIndex; index < num; ++index)
            {
                if (EqualityComparer<T>.Default.Equals(list[index], value))
                    return index;
            }

            return -1;
        }

        public static void EnsureCapacity<TValue>(this List<TValue> list, int value)
        {
            if (list.Capacity < value)
                list.Capacity = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetLast<T>(this IReadOnlyList<T> list)
        {
            return list[list.Count - 1];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddB<T>(this ICollection<T> list, T value)
        {
            int count = list.Count;
            list.Add(value);
            return count != list.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RemoveB<T>(this ICollection<T> list, T value)
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

        public static T PopAt<T>(this List<T> list, int index)
        {
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        public static T PopLast<T>(this IList<T> list)
        {
            return PopLast(list, out _);
        }

        public static bool TryPopLast<T>(this IList<T> list, out T value)
        {
            if (list.Count == 0)
            {
                value = default;
                return false;
            }

            value = PopLast(list, out _);
            return true;
        }

        public static T PopLast<T>(this IList<T> list, out int index)
        {
            index = list.Count - 1;
            T item = list[index];
            list.RemoveAt(index);
            return item;
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