using System.Runtime.CompilerServices;
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
    }
}