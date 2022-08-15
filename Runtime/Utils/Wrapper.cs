namespace Dythervin.Core.Utils
{
    [System.Serializable]
    public struct Wrapper<T>
    {
        public T value;

        public Wrapper(T value)
        {
            this.value = value;
        }

        public static implicit operator Wrapper<T>(T value)
        {
            return new Wrapper<T>(value);
        }
        public static implicit operator T(Wrapper<T> value)
        {
            return value.value;
        }
    }
}