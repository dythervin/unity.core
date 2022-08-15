using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Dythervin.Core.Extensions
{
    public static partial class CollectionsExt
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandom<T>(this IReadOnlyList<T> list, int startIndex = 0)
        {
            return list[list.GetRandomIndex(startIndex)];
        }

        public static T GetRandom<T>(this IReadOnlyList<T> list, out int index, int startIndex = 0)
        {
            index = list.GetRandomIndex(startIndex);
            T item = list[index];
            return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandom<T>(this IReadOnlyList<T> list, Random random, int startIndex = 0)
        {
            return list[GetRandomIndex(list, random, startIndex)];
        }

        public static T GetRandom<T>(this IReadOnlyList<T> list, Random random, out int index, int startIndex = 0)
        {
            index = GetRandomIndex(list, random, startIndex);
            T item = list[index];
            return item;
        }

        public static int GetRandomIndex<T>(this IReadOnlyList<T> list, int startIndex = 0)
        {
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException();
            return UnityEngine.Random.Range(startIndex, list.Count);
        }

        public static int GetRandomIndex<T>(this IReadOnlyList<T> list, Random random, int startIndex = 0)
        {
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException();
            return random.Next(startIndex, list.Count);
        }

        public static int GetRandomIndex<T>(this IList<T> list, int startIndex = 0)
        {
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException();
            return UnityEngine.Random.Range(startIndex, list.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetRandomIndex<T>(this IList<T> list, Random random, int startIndex = 0)
        {
            return random.Next(startIndex, list.Count);
        }

        public static T PopRandom<T>(this IList<T> list)
        {
            int index = list.GetRandomIndex();
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }
    }
}