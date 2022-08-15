using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Dythervin.Core.Extensions
{
    public static class Misc
    {
        public static bool IsSame<T>(this IList<T> a, IList<T> b)
            where T : class
        {
            int count = a.Count;
            if (count != b.Count)
                return false;

            for (int i = 0; i < count; i++)
            {
                if (!ReferenceEquals(a[i], b[i]))
                    return false;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Remove(this string value, string toRemove)
        {
            return value.Replace(toRemove, string.Empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string RemoveSides(this string value)
        {
            return value.Remove("left").Remove("right");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char GetLast(this string str)
        {
            return str[str.Length - 1];
        }

        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string RemoveDigits(this string str)
        {
            return Regex.Replace(str, @"[\d-]", string.Empty);
        }
    }
}