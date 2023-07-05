using Dythervin.Core.Lockables;

namespace Dythervin.Core
{
    public static class LockableSimpleExtensions
    {
        public static ILockableSimple Lock(this ILockableSimple lockable)
        {
            return lockable.SetLock(true);
        }

        public static ILockableSimple Unlock(this ILockableSimple lockable)
        {
            return lockable.SetLock(false);
        }
    }
}