namespace Dythervin.Core
{
    public enum Priority : sbyte
    {
        Low = -64,
        Default = 0,
        High = 64,
    }

    public interface IPrioritized
    {
        Priority Priority { get; }
    }
}