using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Dythervin
{
    [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
    public static class Transforms
    {
        private static readonly Stack<Transform> Buffer = new Stack<Transform>(128);

        public static int ChildCount(this Transform transform)
        {
            Buffer.Clear();
            int amount = 1;
            Buffer.Push(transform);

            do
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);
                    if (child.childCount > 0)
                        Buffer.Push(child);
                    amount++;
                }
            } while (Buffer.TryPop(out transform));


            return amount;
        }

        public static void GetNameValues(this Transform transform, Dictionary<string, Transform> dictionary)
        {
            Buffer.Clear();

            do
            {
                dictionary.Add(transform.name, transform);
                for (int i = 0; i < transform.childCount; i++)
                {
                    Buffer.Push(transform.GetChild(i));
                }
            } while (Buffer.TryPop(out transform));
        }
    }
}