namespace Dythervin.Core.Lockables
{
    public interface ILockableToken : ILockable
    {
        ILockToken CreateLocker();
    }
}