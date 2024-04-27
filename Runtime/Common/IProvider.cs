namespace Dythervin
{
    public interface IProvider<out TData>
    {
        TData Value { get; }
    }
}