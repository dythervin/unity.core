using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Dythervin.Core.Extensions
{
    public static class Vector3Ext
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool InRange(in this Vector3 value, in Vector3 point, float range)
        {
            return (point - value).sqrMagnitude <= range * range;
        }
        
        public static float GetSum(in this Vector3 value)
        {
            return value.x + value.y + value.z;
        }

        public static bool PredictPos(in Vector3 aPos, in Vector3 v, Vector3 bPos, float speed, out Vector3 pos, out float time)
        {
            bPos = aPos - bPos;
            float a = speed * speed - v.x * v.x - v.y * v.y - v.z * v.z;
            float b = -2 * (bPos.x * v.x + bPos.y * v.y + bPos.z * v.z);
            float c = -(bPos.x * bPos.x + bPos.y * bPos.y + bPos.z * bPos.z);
            float root = math.sqrt(b * b - 4 * a * c);

            time = (-b - root) / (2 * a);
            float t1 = (-b + root) / (2 * a);
            if (time < 0 && t1 < 0)
            {
                pos = default;
                return false;
            }

            if (time >= 0 && t1 >= 0)
                time = math.min(time, t1);
            else if (t1 >= 0)
                time = t1;

            pos = aPos + v * time;
            return true;
        }
    }
}