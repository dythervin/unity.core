namespace Dythervin
{
    public static class ObjectPoolExtensions
    {
        public static DisposeHandler<(object obj, IObjectPool pool)> Get(this IObjectPool pool,
            out object obj)
        {
            obj = pool.Get();
            return DisposeHandlerHelper.Create((obj, pool), tuple => tuple.pool.Return(tuple.obj));
        }

        public static DisposeHandler<(T obj, IObjectPool<T> pool)> Get<T>(this IObjectPool<T> pool,
            out T obj)
            where T : class
        {
            obj = pool.Get();
            return DisposeHandlerHelper.Create((obj, pool), tuple => tuple.pool.Return(tuple.obj));
        }

        public static void Return(this IObjectPool pool, ref object element)
        {
            pool.Return(element);
            element = null!;
        }

        public static void Return<T>(this IObjectPool<T> pool, ref T element)
            where T : class
        {
            pool.Return(element);
            element = null!;
        }

        public static bool TryReturn<T>(this IObjectPool<T> pool, ref T? element)
            where T : class
        {
            if (!pool.TryReturn(element))
                return false;

            element = null;
            return true;
        }
    }
}