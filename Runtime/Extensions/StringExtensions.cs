using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Dythervin.Core.Extensions
{
    public static class StringExtensions
    {
        private const string Intend = "    ";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char GetLast(this StringBuilder builder)
        {
            return builder[builder.Length - 1];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder PopLast(this StringBuilder builder, int count = 1)
        {
            return builder.Remove(builder.Length - count, count);
        }

        public static StringBuilder RemoveFrom(this StringBuilder stringBuilder, int index)
        {
            return stringBuilder.Remove(index, stringBuilder.Length - index);
        }

        public static StringBuilder AppendIntend(this StringBuilder stringBuilder, int intendLevel)
        {
            stringBuilder.EnsureCapacity(stringBuilder.Capacity + intendLevel * Intend.Length);
            for (int i = 0; i < intendLevel; i++)
            {
                stringBuilder.Append(Intend);
            }

            return stringBuilder;
        }

        public static string ToStringAndClear(this StringBuilder stringBuilder)
        {
            string str = stringBuilder.ToString();
            stringBuilder.Clear();
            return str;
        }

        public static string FirstCharToUpper(this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input[0].ToString().ToUpper() + input.Substring(1)
            };
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