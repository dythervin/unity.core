namespace Dythervin.Core
{
    public interface IListener
    {
        void Notify();
    }

    public interface IListener<in T>
    {
        void Notify(T value);
    }
}