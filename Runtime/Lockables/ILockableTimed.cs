using Unity.Mathematics;

namespace Dythervin.Core.Lockables
{
    public interface ILockableTimed : ILockable
    {
        float LockDurationLeft { get; }

        void Lock(float duration);
    }

    public static class LockableTimedExtensions
    {
        public static void LockAtLeastFor(this ILockableTimed lockableTimed, float duration)
        {
            lockableTimed.Lock(math.max(duration, lockableTimed.LockDurationLeft));
        }

        public static void AppendLock(this ILockableTimed lockableTimed, float duration)
        {
            lockableTimed.Lock(duration + lockableTimed.LockDurationLeft);
        }

        public static void ClearLock(this ILockableTimed lockableTimed)
        {
            lockableTimed.Lock(0);
        }
    }
}