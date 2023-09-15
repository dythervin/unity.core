using System.Runtime.CompilerServices;
using UnityEngine;
using Random = System.Random;

namespace Dythervin.Core.Extensions
{
    public static partial class RandomExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 NextVector2(this Random random)
        {
            return new Vector2(random.NextFloat(), random.NextFloat());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 NextVector3(this Random random)
        {
            return new Vector3(random.NextFloat(), random.NextFloat(), random.NextFloat());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion NextQuaternion(this Random random)
        {
            return new Quaternion(random.NextFloat(), random.NextFloat(), random.NextFloat(), random.NextFloat());
        }
    }
}