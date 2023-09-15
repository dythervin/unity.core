namespace Dythervin.Core
{
    public abstract class Singleton<T, TImpl>
        where T : class
        where TImpl : Singleton<T, TImpl>, T, new()
    {
        public static readonly T Instance;

        static Singleton()
        {
            TImpl instance = new TImpl();
            Instance = instance;
            instance.Init();
        }

        protected virtual void Init()
        {
        }
    }

    public class Singleton<T> : Singleton<T, T>
        where T : Singleton<T, T>, new()
    {
    }
}