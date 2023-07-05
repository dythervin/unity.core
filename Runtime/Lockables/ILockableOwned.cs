namespace Dythervin.Core.Lockables
{
    public interface ILockableOwned : ILockable
    {
        void AddLocker<T>(T owner)
            where T : class;

        void RemoveLocker<T>(T owner)
            where T : class;
    }
}