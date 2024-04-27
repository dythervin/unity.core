using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Dythervin
{
    public interface IDisposableExt : IDisposable
    {
        bool IsDisposed { get; }
    }

    public static class DisposableExtExtensions
    {
        public static readonly IDisposable Empty = new EmptyDisposable();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("disposable")]
        public static T? Lifetime<T>(this T? disposable, IDisposableHandler handler)
            where T : class, IDisposable
        {
            if (disposable != null)
                handler.Add(disposable);

            return disposable;
        }

        public static void TryDispose<T>(this T disposableExt)
            where T : class
        {
            if (disposableExt is IDisposable disposable)
                disposable.Dispose();
        }

        public static void AssertNotDisposed<T>(this T disposableExt)
            where T : IDisposableExt
        {
            if (disposableExt.IsDisposed)
                throw new ObjectDisposedException(disposableExt.GetType().FullName);
        }

        [Conditional(Symbols.LOG_VERBOSITY.DEBUG)]
        public static void LogWarningIfDisposed<T>(this T disposableExt)
            where T : IDisposableExt
        {
            if (disposableExt.IsDisposed)
                Logger.Debug.LogWarning("Already disposed");
        }

        private class EmptyDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}