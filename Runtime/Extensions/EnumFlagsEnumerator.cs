using System;
using System.Collections;
using System.Collections.Generic;

namespace Dythervin.Core.Extensions
{
    public struct EnumFlagsEnumerator<TEnum> : IEnumerator<TEnum>
        where TEnum : unmanaged, Enum
    {
        private EnumFlagsEnumerator _enumerator;

        public EnumFlagsEnumerator(TEnum value)
        {
            _enumerator = new EnumFlagsEnumerator(value.AsLong());
        }

        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }

        public void Reset()
        {
            _enumerator.Reset();
        }

        public TEnum Current => EnumCast.As<TEnum>(_enumerator.Current);

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            _enumerator.Dispose();
        }
    }

    /// <summary>
    /// Works for flag enums with values [0:long.MaxValue]
    /// </summary>
    public struct EnumFlagsEnumerator : IEnumerator<long>
    {
        private readonly long _value;
        private long _left;
        private long _current;

        public long Current
        {
            get
            {
                if (_current != 0)
                {
                    return _current;
                }

                throw new ArgumentOutOfRangeException();
            }
        }

        object IEnumerator.Current => Current;

        public EnumFlagsEnumerator(long value)
        {
            _value = value;
            _left = _value;
            _current = 0;
        }

        public bool MoveNext()
        {
            if (_left == 0)
            {
                return false;
            }

            long left = _left;
            _left &= _left - 1;
            _current = left - _left;
            return true;
        }

        public void Reset()
        {
            _left = _value;
        }

        public void Dispose()
        {
        }
    }
}