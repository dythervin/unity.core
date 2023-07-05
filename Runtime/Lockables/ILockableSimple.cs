namespace Dythervin.Core.Lockables
{
    public interface ILockableSimple : ILockable
    {
        ILockableSimple SetLock(bool isLocked);
    }
}