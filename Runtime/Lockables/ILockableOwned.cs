namespace Dythervin
{
    public interface ILockableOwned : ILockable
    {
        void AddLocker<T>(T owner)
            where T : class;

        void RemoveLocker<T>(T owner)
            where T : class;
    }
}