using System.Collections.Generic;
using UnityEngine;

namespace Dythervin.Core.Extensions
{
    public static class TransformsDist
    {
        public static bool GetClosest<TElement>(this IEnumerable<TElement> targets, in Vector3 point, FuncIn<TElement, Vector3> getter,
            out TElement closest, FuncIn<TElement, bool> filter = null)
        {
            closest = default;
            float minDist = float.MaxValue;
            if (filter != null)
            {
                foreach (TElement element in targets)
                {
                    if (!filter(element))
                        continue;

                    Vector3 position = getter(element);
                    float dist = (position - point).sqrMagnitude;
                    if (dist < minDist)
                    {
                        closest = element;
                        minDist = dist;
                    }
                }
            }
            else
            {
                foreach (TElement element in targets)
                {
                    Vector3 position = getter(element);
                    float dist = (position - point).sqrMagnitude;
                    if (dist < minDist)
                    {
                        closest = element;
                        minDist = dist;
                    }
                }
            }

            return minDist != float.MaxValue;
        }

        public static TElement GetClosest<TElement>(this IEnumerable<TElement> targets, in Vector3 point, FuncIn<TElement, Vector3> getter,
            FuncIn<TElement, bool> filter = null)
        {
            GetClosest(targets, point, getter, out TElement value, filter);
            return value;
        }

        public static TElement GetClosest<TElement>(this IEnumerable<TElement> targets, in Vector3 point, FuncIn<TElement, bool> filter = null)
            where TElement : Component
        {
            GetClosest(targets, point, (in TElement component) => component.transform.position, out TElement closest, filter);
            return closest;
        }

        public static Vector3 GetClosest(this IEnumerable<Vector3> targets, in Vector3 point, FuncIn<Vector3, bool> filter = null)
        {
            GetClosest(targets, point, (in Vector3 vector3) => vector3, out Vector3 closest, filter);
            return closest;
        }


        public static bool GetFarthest<TElement>(this IEnumerable<TElement> targets, in Vector3 point, FuncIn<TElement, Vector3> getter,
            out TElement farthest, FuncIn<TElement, bool> filter = null)
        {
            farthest = default;
            float maxDist = float.MinValue;
            if (filter != null)
            {
                foreach (TElement element in targets)
                {
                    if (!filter(element))
                        continue;
                    Vector3 position = getter(element);
                    float dist = (position - point).sqrMagnitude;
                    if (dist > maxDist)
                    {
                        farthest = element;
                        maxDist = dist;
                    }
                }
            }
            else
                foreach (TElement element in targets)
                {
                    Vector3 position = getter(element);
                    float dist = (position - point).sqrMagnitude;
                    if (dist > maxDist)
                    {
                        farthest = element;
                        maxDist = dist;
                    }
                }

            return maxDist != float.MinValue;
        }

        public static TElement GetFarthest<TElement>(this IEnumerable<TElement> targets, in Vector3 point, FuncIn<TElement, bool> filter = null)
            where TElement : Component
        {
            GetFarthest(targets, point, (in TElement component) => component.transform.position, out TElement closest, filter);
            return closest;
        }
    }
}