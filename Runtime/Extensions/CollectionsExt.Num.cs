using System.Collections.Generic;

namespace Dythervin.Core.Extensions
{
    public static partial class CollectionsExt
    {
        /// <summary>
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns>Sum of elements in range [0: index]</returns>
        public static int GetSumTo(this IReadOnlyList<int> list, int index)
        {
            int sum = 0;
            for (int i = 0; i <= index; i++)
            {
                sum += list[i];
            }

            return sum;
        }
    }
}