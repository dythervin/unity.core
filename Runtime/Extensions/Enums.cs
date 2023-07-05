using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Assertions;

namespace Dythervin.Core.Extensions
{
    public static class Enums
    {
        private static readonly Dictionary<Type, IDictionary> NamesDictionary = new Dictionary<Type, IDictionary>();
        private static readonly Dictionary<Type, Array> Values = new Dictionary<Type, Array>();
        private static readonly Dictionary<Type, IReadOnlyList<string>> Names =
            new Dictionary<Type, IReadOnlyList<string>>();

        public static IReadOnlyList<T> GetValues<T>(bool cache = false)
            where T : Enum
        {
            Type type = typeof(T);
            if (!Values.TryGetValue(type, out Array array))
            {
                array = Enum.GetValues(typeof(T));
                if (cache)
                    Values[type] = array;
            }

            // ReSharper disable once SuspiciousTypeConversion.Global
            return (IReadOnlyList<T>)array;
        }

        // ReSharper disable once UnusedParameter.Global
        public static IReadOnlyList<T> GetValues<T>(this T @enum, bool cache = false)
            where T : Enum
        {
            return GetValues<T>(cache);
        }

        public static EnumFlagsEnumerable<TEnum> ToEnumerable<TEnum>(this TEnum @enum)
            where TEnum : unmanaged, Enum
        {
            return new EnumFlagsEnumerable<TEnum>(@enum);
        }

        public static IReadOnlyList<string> GetNames<T>(bool cache = false)
            where T : Enum
        {
            return GetNames(typeof(T), cache);
        }

        public static IReadOnlyList<string> GetNames(Type type, bool cache = false)
        {
            Assert.IsTrue(type != null && type.IsEnum);
            if (!Names.TryGetValue(type, out var array))
            {
                array = Enum.GetNames(type);
                if (cache)
                    Names[type] = array;
            }

            return array;
        }

        public static IReadOnlyList<string> GetNames<T>(this T @enum, bool cache = false)
            where T : Enum
        {
            return GetNames<T>(cache);
        }


        public static bool HasFlagFast(this BindingFlags value, BindingFlags flag)
        {
            return (value & flag) != 0;
        }

        public static string GetNameCached<T>(this T value)
            where T : Enum
        {
            Type type = typeof(T);
            Dictionary<T, string> dict;

            if (NamesDictionary.TryGetValue(type, out IDictionary iDict))
                dict = (Dictionary<T, string>)iDict;
            else
                NamesDictionary.Add(type, dict = new Dictionary<T, string>());

            if (!dict.TryGetValue(value, out string name))
                dict[value] = name = value.ToString();

            return name;
        }
    }
}