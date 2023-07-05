using System;
using System.Collections;
using System.Collections.Generic;

namespace Dythervin.Core.Extensions
{
    public readonly struct EnumFlagsEnumerable<TEnum> : IEnumerable<TEnum>
        where TEnum : unmanaged, Enum
    {
        private readonly TEnum _enum;

        public EnumFlagsEnumerable(TEnum @enum)
        {
            _enum = @enum;
        }

        IEnumerator<TEnum> IEnumerable<TEnum>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public EnumFlagsEnumerator<TEnum> GetEnumerator()
        {
            return new EnumFlagsEnumerator<TEnum>(_enum);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}