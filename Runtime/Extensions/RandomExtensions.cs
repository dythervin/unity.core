using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using Random = System.Random;

namespace Dythervin
{
    public static partial class RandomExtensions
    {
        private static readonly StringBuilder StringBuilder = new StringBuilder();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte NextByte(this Random random)
        {
            return (byte)random.Next(byte.MinValue, byte.MaxValue + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte NextByte(this Random random, byte max)
        {
            return (byte)random.Next(byte.MinValue, Mathf.Clamp(max + 1, byte.MinValue, byte.MaxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NextString(this Random random, int lenght)
        {
            StringBuilder.Clear();
            for (int i = 0; i < lenght; i++)
            {
                StringBuilder.Append(random.NextChar());
            }

            return StringBuilder.ToStringAndClear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NextBool(this Random random)
        {
            return random.Next() >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte NextSbyte(this Random random)
        {
            return (sbyte)random.Next(sbyte.MinValue, sbyte.MaxValue + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short NextShort(this Random random)
        {
            return (short)random.Next(short.MinValue, short.MaxValue + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort NextUshort(this Random random)
        {
            return (ushort)random.Next(ushort.MinValue, ushort.MaxValue + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char NextChar(this Random random)
        {
            return (char)random.NextUshort();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NextFloat(this Random random)
        {
            return (float)(random.NextDouble() / 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NextInt(this Random random)
        {
            return random.Next();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint NextUint(this Random random)
        {
            return Convert.ToUInt32(random.Next());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong NextULong(this Random random)
        {
            Span<byte> bytes = stackalloc byte[8];
            random.NextBytes(bytes);
            return BitConverter.ToUInt64(bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long NextLong(this Random random)
        {
            Span<byte> bytes = stackalloc byte[8];
            random.NextBytes(bytes);
            return BitConverter.ToInt64(bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillRandom<T>(this Random random, ICollection<T> collection, Func<Random, T> func,
            byte maxSize)
        {
            collection.Clear();
            byte size = random.NextByte(maxSize);
            for (byte i = 0; i < size; i++)
            {
                collection.Add(func.Invoke(random));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillRandom<TCollection, T>(this Random random, ref TCollection collection, Func<Random, T> func,
            byte maxSize)
            where TCollection : ICollection<T>, new()
        {
            collection ??= new TCollection();
            random.FillRandom(collection, func, maxSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillRandom<TCollection, T, T1>(this Random random, ref TCollection dictionary, Func<Random, T> funcT,
            Func<Random, T1> funcT1, byte maxSize)
            where TCollection : IDictionary<T, T1>, new()
        {
            dictionary ??= new TCollection();
            dictionary.Clear();
            byte size = random.NextByte(maxSize);
            for (byte i = 0; i < size; i++)
            {
                dictionary[funcT.Invoke(random)] = funcT1.Invoke(random);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillRandom<T>(this Random random, ref T[] collection, Func<Random, T> func,
            byte maxSize)
        {
            collection ??= new T[random.NextByte(maxSize)];
            for (byte i = 0; i < collection.Length; i++)
            {
                collection[i] = func.Invoke(random);
            }
        }
    }
}