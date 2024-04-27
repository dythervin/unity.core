namespace Dythervin
{
    public interface ILockableToken : ILockable
    {
        ILockToken CreateLocker();
    }
}