using System;
using System.Collections;
using System.Collections.Generic;

namespace Dythervin.Core.Extensions
{
    public static class EnumsExt
    {
        private static readonly Dictionary<Type, IDictionary> Names = new Dictionary<Type, IDictionary>();
        private static readonly Dictionary<Type, Array> Values = new Dictionary<Type, Array>();

        public static IReadOnlyList<T> GetValues<T>(this T @enum, bool cache = false)
            where T : Enum
        {
            Type type = typeof(T);
            if (!Values.TryGetValue(type, out Array array))
            {
                array = Enum.GetValues(typeof(T));
                if (cache)
                    Values[type] = array;
            }

            return (IReadOnlyList<T>)array;
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