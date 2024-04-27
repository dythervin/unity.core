using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dythervin
{
    public static class StringBuilderExtensions
    {
        private const string Intend = "\t";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char GetLast(this StringBuilder builder)
        {
            return builder[builder.Length - 1];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOf(this StringBuilder builder, char value, int startIndex)
        {
            return IndexOf(builder, value, startIndex, builder.Length - startIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOf(this StringBuilder builder, string value, int startIndex)
        {
            return IndexOf(builder, value, startIndex, builder.Length - startIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOf(this StringBuilder builder, char value)
        {
            return IndexOf(builder, value, 0, builder.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOf(this StringBuilder builder, string value)
        {
            return IndexOf(builder, value, 0, builder.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOf(this StringBuilder builder, char value, int startIndex, int count)
        {
            if (startIndex < 0 || startIndex >= builder.Length)
                throw new ArgumentOutOfRangeException(nameof(startIndex));

            if (count < 0 || count > builder.Length - startIndex)
                throw new ArgumentOutOfRangeException(nameof(count));

            for (int i = startIndex; i < startIndex + count; i++)
            {
                if (builder[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }

        public static int IndexOf(this StringBuilder builder, string value, int startIndex, int count)
        {
            if (startIndex < 0 || startIndex >= builder.Length)
                throw new ArgumentOutOfRangeException(nameof(startIndex));

            if (count < 0 || count > builder.Length - startIndex)
                throw new ArgumentOutOfRangeException(nameof(count));

            for (int i = startIndex; i < startIndex + count; i++)
            {
                int startI = i;
                for (int j = 0; j < value.Length; j++)
                {
                    if (builder[i] != value[j])
                    {
                        break;
                    }

                    if (j == value.Length - 1)
                    {
                        return startI;
                    }

                    i++;
                }
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder PopLast(this StringBuilder builder, int count = 1)
        {
            return builder.Remove(builder.Length - count, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        public static StringBuilder AppendIntended(this StringBuilder stringBuilder, int intendLevel, string str)
        {
            stringBuilder.AppendIntend(intendLevel);
            stringBuilder.Append(str);
            return stringBuilder;
        }

        public static StringBuilder AppendLineIntended(this StringBuilder stringBuilder, int intendLevel, string str)
        {
            stringBuilder.AppendIntend(intendLevel);
            stringBuilder.AppendLine(str);

            return stringBuilder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringAndClear(this StringBuilder stringBuilder)
        {
            string str = stringBuilder.ToString();
            stringBuilder.Clear();
            return str;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToString(this StringBuilder stringBuilder, int startIndex)
        {
            return stringBuilder.ToString(startIndex, stringBuilder.Length - startIndex);
        }
        
        
    }
}