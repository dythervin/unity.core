using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

namespace Dythervin
{
    public static class Enums
    {
        public static IReadOnlyList<T> GetValues<T>()
            where T : Enum
        {
            return Cache<T>.Values;
        }

        // ReSharper disable once UnusedParameter.Global
        public static IReadOnlyList<T> GetValues<T>(this T @enum)
            where T : Enum
        {
            return GetValues<T>();
        }

        public static EnumFlagsInt32Enumerable<TEnum> ToEnumerable<TEnum>(this TEnum @enum)
            where TEnum : unmanaged, Enum
        {
            return new EnumFlagsInt32Enumerable<TEnum>(@enum);
        }

        public static IReadOnlyList<string> GetNames(Type type)
        {
            return Cache<Enum>.Names;
        }

        public static IReadOnlyList<string> GetNames<T>(this T @enum)
            where T : Enum
        {
            return GetNames(typeof(T));
        }

        public static bool HasFlagFast(this BindingFlags value, BindingFlags flag)
        {
            return (value & flag) != 0;
        }

        public static string ToStringCached<T>(this T value)
            where T : Enum
        {
            if (Cache<T>.Dictionary.TryGetValue(value, out string name))
                return name;

            throw new ArgumentException($"Enum value {value} not found in {typeof(T)}");
        }

        private static class Cache<T>
            where T : Enum
        {
            public static readonly IReadOnlyList<string> Names;
            public static readonly IReadOnlyDictionary<T, string> Dictionary;
            public static readonly IReadOnlyList<T> Values;

            static Cache()
            {
                Type type = typeof(T);

                Values = (T[])Enum.GetValues(type);
                var names = new string[Values.Count];
                var dictionary = new Dictionary<T, string>(Values.Count);
                int index = 0;
                foreach (T value in Values)
                {
                    var name = value.ToString();
                    dictionary.Add(value, name);
                    names[index++] = name;
                }

                Names = names;
                Dictionary = dictionary;
            }
        }
    }
}