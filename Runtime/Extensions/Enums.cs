using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Dythervin.Core.Extensions
{
    public static class Enums
    {
        private static readonly Dictionary<Type, IDictionary> Names = new Dictionary<Type, IDictionary>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] GetValues<T>(this T @enum)
            where T : Enum
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static string GetNameCached<T>(this T value)
            where T : Enum
        {
            Type type = typeof(T);
            Dictionary<T, string> dict;


            if (Names.TryGetValue(type, out var iDict))
                dict = (Dictionary<T, string>)iDict;
            else
                Names.Add(type, dict = new Dictionary<T, string>());

            if (!dict.TryGetValue(value, out string name))
                dict[value] = name = value.ToString();

            return name;
        }
    }
}