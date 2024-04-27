namespace Dythervin
{
    public static class LockableSimpleExtensions
    {
        public static ILockableSimple Lock(this ILockableSimple lockable)
        {
            return lockable.ForceLock(true);
        }

        public static ILockableSimple Unlock(this ILockableSimple lockable)
        {
            return lockable.ForceLock(false);
        }
    }
}