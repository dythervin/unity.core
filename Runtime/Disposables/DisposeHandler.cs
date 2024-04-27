using System;

namespace Dythervin
{
    public struct DisposeHandlerHelper
    {
        public static DisposeHandler<T> Create<T>(T data, Action<T> onDispose)
        {
            return new DisposeHandler<T>(data, onDispose);
        }
    }

    public struct DisposeHandler<TData> : IDisposableExt
    {
        private readonly TData _data;
        private Action<TData>? _onDispose;

        public DisposeHandler(TData data, Action<TData> onDispose)
        {
            _data = data;
            _onDispose = onDispose ?? throw new ArgumentNullException(nameof(onDispose));
        }

        public bool IsDisposed => _onDispose == null;

        public void Dispose()
        {
            if (_onDispose == null)
                return;

            _onDispose(_data);
            _onDispose = null;
        }
    }
}