using System.Runtime.CompilerServices;
using System.Text;

namespace Dythervin.Core.Extensions
{
    public static class StringExt
    {
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
    }
}