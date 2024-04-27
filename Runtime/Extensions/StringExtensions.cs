using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Dythervin
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper([NotNull] this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input[0].ToString().ToUpper() + input.Substring(1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Remove([NotNull] this string value, [NotNull] string toRemove)
        {
            return value.Replace(toRemove, string.Empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string RemoveStart([NotNull] this string value, [NotNull] string toRemove)
        {
            return value.StartsWith(toRemove) ? value.Substring(toRemove.Length) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string RemoveSides([NotNull] this string value)
        {
            return value.Remove("left").Remove("right");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char GetLast([NotNull] this string str)
        {
            return str[str.Length - 1];
        }

        public static string SplitCamelCase([NotNull] this string str)
        {
            return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string RemoveDigits([NotNull] this string str)
        {
            return Regex.Replace(str, @"[\d-]", string.Empty);
        }
    }
}