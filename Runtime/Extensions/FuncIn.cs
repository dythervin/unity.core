namespace Dythervin.Core.Extensions
{
    public delegate TOut FuncIn<T, out TOut>(in T value);
}