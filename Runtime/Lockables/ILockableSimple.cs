namespace Dythervin
{
    public interface ILockableSimple : ILockable
    {
        ILockableSimple ForceLock(bool isLocked);
    }
}