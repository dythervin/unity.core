using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Dythervin.Core.Extensions
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

        public static void GetNameValues(this Transform transform, Dictionary<int, Transform> dictionary)
        {
            Buffer.Clear();
            do
            {
                int key = transform.name.GetHashCode();
                if (dictionary.ContainsKey(key))
                {
                    Debug.LogError($"Contains {transform.name}", transform);
                    Debug.LogError($"   Target", dictionary[transform.name.GetHashCode()]);
                    continue;
                }

                dictionary.Add(key, transform);

                for (int i = 0; i < transform.childCount; i++)
                {
                    Buffer.Push(transform.GetChild(i));
                }
            } while (Buffer.TryPop(out transform));
        }
    }
}