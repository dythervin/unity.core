using System;

namespace Dythervin.Core
{
    public interface IDisposableExt : IDisposable
    {
        bool IsDisposed { get; }
    }

    public static class DisposableExtExtensions
    {
        public static bool TryDispose(this IDisposableExt disposableExt)
        {
            if (disposableExt.IsDisposed)
                return false;

            disposableExt.Dispose();
            return true;
        }

        public static void AssertNotDisposed(this IDisposableExt disposableExt)
        {
            if (disposableExt.IsDisposed)
                throw new Exception("Already disposed");
        }
    }
}