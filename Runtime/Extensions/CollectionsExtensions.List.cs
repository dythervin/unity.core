using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Dythervin
{
    public static partial class ListExtensions
    {
        public static void Reset<T>(this T?[] list)
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

        public static int IndexOf<T>(this IReadOnlyList<T> list, T value, int startIndex = 0)
        {
            for (int index = startIndex; index < list.Count; ++index)
            {
                if (EqualityComparer<T>.Default.Equals(list[index], value))
                    return index;
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LastIndexOf<T>(this IReadOnlyList<T> list, T value)
        {
            return LastIndexOf(list, value, list.Count - 1);
        }

        public static int LastIndexOf<T>(this IReadOnlyList<T> list, T value, int startIndex)
        {
            for (int index = startIndex; index >= 0; --index)
            {
                if (EqualityComparer<T>.Default.Equals(list[index], value))
                    return index;
            }

            return -1;
        }

        public static int FindIndex<T>(this IReadOnlyList<T> list, Func<T, bool> predicate, int startIndex = 0)
        {
            int num = list.Count;
            for (int index = startIndex; index < num; ++index)
            {
                if (predicate(list[index]))
                    return index;
            }

            return -1;
        }

        public static int FindIndex<T, TState>(this IReadOnlyList<T> list, TState state,
            Func<T, TState, bool> predicate, int startIndex = 0)
        {
            for (int index = startIndex; index < list.Count; ++index)
            {
                if (predicate(list[index], state))
                    return index;
            }

            return -1;
        }

        public static int FindLastIndex<T>(this IReadOnlyList<T> list, Func<T, bool> predicate, int startIndex)
        {
            for (int index = startIndex; index >= 0; --index)
            {
                if (predicate(list[index]))
                    return index;
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FindLastIndex<T>(this IReadOnlyList<T> list, Func<T, bool> predicate)
        {
            return FindLastIndex(list, predicate, list.Count - 1);
        }

        public static int FindLastIndex<T, TState>(this IReadOnlyList<T> list, TState state,
            Func<T, TState, bool> predicate, int startIndex)
        {
            for (int index = startIndex; index >= 0; --index)
            {
                if (predicate(list[index], state))
                    return index;
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FindLastIndex<T, TState>(this IReadOnlyList<T> list, TState state,
            Func<T, TState, bool> predicate)
        {
            return FindLastIndex(list, state, predicate, list.Count - 1);
        }

        public static void EnsureCapacity<TValue>(this List<TValue> list, int value)
        {
            if (list.Capacity < value)
                list.Capacity = value;
        }

        public static void RemoveAtSwap<T>(this List<T> list, int index)
        {
            int count = list.Count;
            if (count <= index)
                throw new ArgumentOutOfRangeException(nameof(index));

            int lastIndex = count - 1;

            if (lastIndex == index)
            {
                list.RemoveAt(index);
                return;
            }

            list[index] = list[lastIndex];
            list.RemoveAt(lastIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetLast<T>(this IReadOnlyList<T> list)
        {
            return list[list.Count - 1];
        }

        public static T PopAt<T>(this List<T> list, int index)
        {
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveRange<T>(this List<T> list, int index)
        {
            list.RemoveRange(index, list.Count - index);
        }

        public static T PopLast<T>(this IList<T> list)
        {
            AssertListIsMutableAndNonEmpty(list);
            int index = list.Count - 1;
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        public static bool TryPopLast<T>(this IList<T?> list, out T? value)
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
            AssertListIsMutableAndNonEmpty(list);
            index = list.Count - 1;
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void AssertListIsMutableAndNonEmpty<T>(ICollection<T> list)
        {
            DAssert.IsTrue(list.Count > 0, "List is empty");
            DAssert.IsTrue(list.IsReadOnly == false, "List is read-only");
        }

        private static class ArrayAccessor<T>
        {
            public static readonly Func<List<T>, T[]> Getter;

            static ArrayAccessor()
            {
                var dm = new DynamicMethod("get",
                    MethodAttributes.Static | MethodAttributes.Public,
                    CallingConventions.Standard,
                    typeof(T[]),
                    new Type[] { typeof(List<T>) },
                    typeof(ArrayAccessor<T>),
                    true);

                var il = dm.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0); // Load List<T> argument
                il.Emit(OpCodes.Ldfld,
                    typeof(List<T>).GetField("_items",
                        BindingFlags.NonPublic | BindingFlags.Instance)); // Replace argument by field

                il.Emit(OpCodes.Ret); // Return field
                Getter = (Func<List<T>, T[]>)dm.CreateDelegate(typeof(Func<List<T>, T[]>));
            }
        }

        public static T[] GetInternalArray<T>(this List<T> list)
        {
            return ArrayAccessor<T>.Getter(list);
        }
    }
}