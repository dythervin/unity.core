using System;
using System.Collections;
using System.Collections.Generic;

namespace Dythervin
{
    public struct EnumFlagsByteEnumerator<TEnum> : IEnumerator<TEnum>
        where TEnum : unmanaged, Enum
    {
        private EnumFlagsByteEnumerator _enumerator;

        public EnumFlagsByteEnumerator(TEnum value)
        {
            _enumerator = new EnumFlagsByteEnumerator(value.AsByte());
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

    public struct EnumFlagsByteEnumerator : IEnumerator<byte>
    {
        private readonly byte _value;
        private byte _left;
        private byte _current;

        public byte Current
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

        public EnumFlagsByteEnumerator(byte value)
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

            byte left = _left;
            _left &= (byte)(_left - 1);
            _current = (byte)(left - _left);
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


    public readonly struct EnumFlagsByteEnumerable<TEnum> : IEnumerable<TEnum>
        where TEnum : unmanaged, Enum
    {
        private readonly TEnum _enum;

        public EnumFlagsByteEnumerable(TEnum @enum)
        {
            _enum = @enum;
        }

        IEnumerator<TEnum> IEnumerable<TEnum>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public EnumFlagsByteEnumerator<TEnum> GetEnumerator()
        {
            return new EnumFlagsByteEnumerator<TEnum>(_enum);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public struct EnumFlagsUInt16Enumerator<TEnum> : IEnumerator<TEnum>
        where TEnum : unmanaged, Enum
    {
        private EnumFlagsUInt16Enumerator _enumerator;

        public EnumFlagsUInt16Enumerator(TEnum value)
        {
            _enumerator = new EnumFlagsUInt16Enumerator(value.AsUshort());
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

    public struct EnumFlagsUInt16Enumerator : IEnumerator<ushort>
    {
        private readonly ushort _value;
        private ushort _left;
        private ushort _current;

        public ushort Current
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

        public EnumFlagsUInt16Enumerator(ushort value)
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

            ushort left = _left;
            _left &= (ushort)(_left - 1);
            _current = (ushort)(left - _left);
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


    public readonly struct EnumFlagsUInt16Enumerable<TEnum> : IEnumerable<TEnum>
        where TEnum : unmanaged, Enum
    {
        private readonly TEnum _enum;

        public EnumFlagsUInt16Enumerable(TEnum @enum)
        {
            _enum = @enum;
        }

        IEnumerator<TEnum> IEnumerable<TEnum>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public EnumFlagsUInt16Enumerator<TEnum> GetEnumerator()
        {
            return new EnumFlagsUInt16Enumerator<TEnum>(_enum);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public struct EnumFlagsUInt32Enumerator<TEnum> : IEnumerator<TEnum>
        where TEnum : unmanaged, Enum
    {
        private EnumFlagsUInt32Enumerator _enumerator;

        public EnumFlagsUInt32Enumerator(TEnum value)
        {
            _enumerator = new EnumFlagsUInt32Enumerator(value.AsUint());
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

    public struct EnumFlagsUInt32Enumerator : IEnumerator<uint>
    {
        private readonly uint _value;
        private uint _left;
        private uint _current;

        public uint Current
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

        public EnumFlagsUInt32Enumerator(uint value)
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

            uint left = _left;
            _left &= (uint)(_left - 1);
            _current = (uint)(left - _left);
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


    public readonly struct EnumFlagsUInt32Enumerable<TEnum> : IEnumerable<TEnum>
        where TEnum : unmanaged, Enum
    {
        private readonly TEnum _enum;

        public EnumFlagsUInt32Enumerable(TEnum @enum)
        {
            _enum = @enum;
        }

        IEnumerator<TEnum> IEnumerable<TEnum>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public EnumFlagsUInt32Enumerator<TEnum> GetEnumerator()
        {
            return new EnumFlagsUInt32Enumerator<TEnum>(_enum);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public struct EnumFlagsUInt64Enumerator<TEnum> : IEnumerator<TEnum>
        where TEnum : unmanaged, Enum
    {
        private EnumFlagsUInt64Enumerator _enumerator;

        public EnumFlagsUInt64Enumerator(TEnum value)
        {
            _enumerator = new EnumFlagsUInt64Enumerator(value.AsUlong());
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

    public struct EnumFlagsUInt64Enumerator : IEnumerator<ulong>
    {
        private readonly ulong _value;
        private ulong _left;
        private ulong _current;

        public ulong Current
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

        public EnumFlagsUInt64Enumerator(ulong value)
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

            ulong left = _left;
            _left &= (ulong)(_left - 1);
            _current = (ulong)(left - _left);
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


    public readonly struct EnumFlagsUInt64Enumerable<TEnum> : IEnumerable<TEnum>
        where TEnum : unmanaged, Enum
    {
        private readonly TEnum _enum;

        public EnumFlagsUInt64Enumerable(TEnum @enum)
        {
            _enum = @enum;
        }

        IEnumerator<TEnum> IEnumerable<TEnum>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public EnumFlagsUInt64Enumerator<TEnum> GetEnumerator()
        {
            return new EnumFlagsUInt64Enumerator<TEnum>(_enum);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public struct EnumFlagsSByteEnumerator<TEnum> : IEnumerator<TEnum>
        where TEnum : unmanaged, Enum
    {
        private EnumFlagsSByteEnumerator _enumerator;

        public EnumFlagsSByteEnumerator(TEnum value)
        {
            _enumerator = new EnumFlagsSByteEnumerator(value.AsSbyte());
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

    public struct EnumFlagsSByteEnumerator : IEnumerator<sbyte>
    {
        private readonly sbyte _value;
        private sbyte _left;
        private sbyte _current;

        public sbyte Current
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

        public EnumFlagsSByteEnumerator(sbyte value)
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

            sbyte left = _left;
            _left &= (sbyte)(_left - 1);
            _current = (sbyte)(left - _left);
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


    public readonly struct EnumFlagsSByteEnumerable<TEnum> : IEnumerable<TEnum>
        where TEnum : unmanaged, Enum
    {
        private readonly TEnum _enum;

        public EnumFlagsSByteEnumerable(TEnum @enum)
        {
            _enum = @enum;
        }

        IEnumerator<TEnum> IEnumerable<TEnum>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public EnumFlagsSByteEnumerator<TEnum> GetEnumerator()
        {
            return new EnumFlagsSByteEnumerator<TEnum>(_enum);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public struct EnumFlagsInt16Enumerator<TEnum> : IEnumerator<TEnum>
        where TEnum : unmanaged, Enum
    {
        private EnumFlagsInt16Enumerator _enumerator;

        public EnumFlagsInt16Enumerator(TEnum value)
        {
            _enumerator = new EnumFlagsInt16Enumerator(value.AsShort());
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

    public struct EnumFlagsInt16Enumerator : IEnumerator<short>
    {
        private readonly short _value;
        private short _left;
        private short _current;

        public short Current
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

        public EnumFlagsInt16Enumerator(short value)
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

            short left = _left;
            _left &= (short)(_left - 1);
            _current = (short)(left - _left);
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


    public readonly struct EnumFlagsInt16Enumerable<TEnum> : IEnumerable<TEnum>
        where TEnum : unmanaged, Enum
    {
        private readonly TEnum _enum;

        public EnumFlagsInt16Enumerable(TEnum @enum)
        {
            _enum = @enum;
        }

        IEnumerator<TEnum> IEnumerable<TEnum>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public EnumFlagsInt16Enumerator<TEnum> GetEnumerator()
        {
            return new EnumFlagsInt16Enumerator<TEnum>(_enum);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public struct EnumFlagsInt32Enumerator<TEnum> : IEnumerator<TEnum>
        where TEnum : unmanaged, Enum
    {
        private EnumFlagsInt32Enumerator _enumerator;

        public EnumFlagsInt32Enumerator(TEnum value)
        {
            _enumerator = new EnumFlagsInt32Enumerator(value.AsInt());
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

    public struct EnumFlagsInt32Enumerator : IEnumerator<int>
    {
        private readonly int _value;
        private int _left;
        private int _current;

        public int Current
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

        public EnumFlagsInt32Enumerator(int value)
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

            int left = _left;
            _left &= (int)(_left - 1);
            _current = (int)(left - _left);
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


    public readonly struct EnumFlagsInt32Enumerable<TEnum> : IEnumerable<TEnum>
        where TEnum : unmanaged, Enum
    {
        private readonly TEnum _enum;

        public EnumFlagsInt32Enumerable(TEnum @enum)
        {
            _enum = @enum;
        }

        IEnumerator<TEnum> IEnumerable<TEnum>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public EnumFlagsInt32Enumerator<TEnum> GetEnumerator()
        {
            return new EnumFlagsInt32Enumerator<TEnum>(_enum);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public struct EnumFlagsInt64Enumerator<TEnum> : IEnumerator<TEnum>
        where TEnum : unmanaged, Enum
    {
        private EnumFlagsInt64Enumerator _enumerator;

        public EnumFlagsInt64Enumerator(TEnum value)
        {
            _enumerator = new EnumFlagsInt64Enumerator(value.AsLong());
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

    public struct EnumFlagsInt64Enumerator : IEnumerator<long>
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

        public EnumFlagsInt64Enumerator(long value)
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
            _left &= (long)(_left - 1);
            _current = (long)(left - _left);
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


    public readonly struct EnumFlagsInt64Enumerable<TEnum> : IEnumerable<TEnum>
        where TEnum : unmanaged, Enum
    {
        private readonly TEnum _enum;

        public EnumFlagsInt64Enumerable(TEnum @enum)
        {
            _enum = @enum;
        }

        IEnumerator<TEnum> IEnumerable<TEnum>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public EnumFlagsInt64Enumerator<TEnum> GetEnumerator()
        {
            return new EnumFlagsInt64Enumerator<TEnum>(_enum);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public static class EnumFlagsExtensions
    {
        public static EnumFlagsByteEnumerable<TEnum> ToEnumerableByte<TEnum>(this TEnum @enum)
            where TEnum : unmanaged, Enum
        {
            return new EnumFlagsByteEnumerable<TEnum>(@enum);
        }

        public static EnumFlagsUInt16Enumerable<TEnum> ToEnumerableUInt16<TEnum>(this TEnum @enum)
            where TEnum : unmanaged, Enum
        {
            return new EnumFlagsUInt16Enumerable<TEnum>(@enum);
        }

        public static EnumFlagsUInt32Enumerable<TEnum> ToEnumerableUInt32<TEnum>(this TEnum @enum)
            where TEnum : unmanaged, Enum
        {
            return new EnumFlagsUInt32Enumerable<TEnum>(@enum);
        }

        public static EnumFlagsUInt64Enumerable<TEnum> ToEnumerableUInt64<TEnum>(this TEnum @enum)
            where TEnum : unmanaged, Enum
        {
            return new EnumFlagsUInt64Enumerable<TEnum>(@enum);
        }

        public static EnumFlagsSByteEnumerable<TEnum> ToEnumerableSByte<TEnum>(this TEnum @enum)
            where TEnum : unmanaged, Enum
        {
            return new EnumFlagsSByteEnumerable<TEnum>(@enum);
        }

        public static EnumFlagsInt16Enumerable<TEnum> ToEnumerableInt16<TEnum>(this TEnum @enum)
            where TEnum : unmanaged, Enum
        {
            return new EnumFlagsInt16Enumerable<TEnum>(@enum);
        }

        public static EnumFlagsInt32Enumerable<TEnum> ToEnumerableInt32<TEnum>(this TEnum @enum)
            where TEnum : unmanaged, Enum
        {
            return new EnumFlagsInt32Enumerable<TEnum>(@enum);
        }

        public static EnumFlagsInt64Enumerable<TEnum> ToEnumerableInt64<TEnum>(this TEnum @enum)
            where TEnum : unmanaged, Enum
        {
            return new EnumFlagsInt64Enumerable<TEnum>(@enum);
        }

    }
}