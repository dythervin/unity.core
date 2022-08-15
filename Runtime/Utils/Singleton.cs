namespace Dythervin.Core.Utils
{
    public abstract class Singleton<T>
        where T : Singleton<T>, new()
    {
        public static readonly T Instance;

        static Singleton()
        {
            Instance = new T();
            Instance.Init();
        }

        protected virtual void Init() { }
    }
}