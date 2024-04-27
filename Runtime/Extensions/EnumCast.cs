using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Dythervin
{
    public static class EnumCast
    {
        private static byte AsByteUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, byte>(ref value);
        }

        public static byte AsByte(this Enum value)
        {
            return Convert.ToByte(value);
        }
        
        private static TEnum AsUnsafe<TEnum>(byte value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<byte, TEnum>(ref value);
        }

        public static byte AsByte<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(byte))
                return AsByteUnsafe(value);

            switch (size)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum As<TEnum>(byte value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();

            switch (size)
            {
                case sizeof(byte):
                    return AsUnsafe<TEnum>(checked((byte)value));
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        private static ushort AsUshortUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, ushort>(ref value);
        }

        public static ushort AsUshort(this Enum value)
        {
            return Convert.ToUInt16(value);
        }
        
        private static TEnum AsUnsafe<TEnum>(ushort value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<ushort, TEnum>(ref value);
        }

        public static ushort AsUshort<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(ushort))
                return AsUshortUnsafe(value);

            switch (size)
            {
                case sizeof(byte):
                    return AsByteUnsafe(value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum As<TEnum>(ushort value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();

            switch (size)
            {
                case sizeof(ushort):
                    return AsUnsafe<TEnum>(checked((ushort)value));
                case sizeof(byte):
                    return AsUnsafe<TEnum>(checked((byte)value));
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        private static uint AsUintUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, uint>(ref value);
        }

        public static uint AsUint(this Enum value)
        {
            return Convert.ToUInt32(value);
        }
        
        private static TEnum AsUnsafe<TEnum>(uint value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<uint, TEnum>(ref value);
        }

        public static uint AsUint<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(uint))
                return AsUintUnsafe(value);

            switch (size)
            {
                case sizeof(byte):
                    return AsByteUnsafe(value);
                case sizeof(ushort):
                    return AsUshortUnsafe(value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum As<TEnum>(uint value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();

            switch (size)
            {
                case sizeof(uint):
                    return AsUnsafe<TEnum>(checked((uint)value));
                case sizeof(ushort):
                    return AsUnsafe<TEnum>(checked((ushort)value));
                case sizeof(byte):
                    return AsUnsafe<TEnum>(checked((byte)value));
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        private static ulong AsUlongUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, ulong>(ref value);
        }

        public static ulong AsUlong(this Enum value)
        {
            return Convert.ToUInt64(value);
        }
        
        private static TEnum AsUnsafe<TEnum>(ulong value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<ulong, TEnum>(ref value);
        }

        public static ulong AsUlong<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(ulong))
                return AsUlongUnsafe(value);

            switch (size)
            {
                case sizeof(byte):
                    return AsByteUnsafe(value);
                case sizeof(ushort):
                    return AsUshortUnsafe(value);
                case sizeof(uint):
                    return AsUintUnsafe(value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum As<TEnum>(ulong value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();

            switch (size)
            {
                case sizeof(ulong):
                    return AsUnsafe<TEnum>(checked((ulong)value));
                case sizeof(uint):
                    return AsUnsafe<TEnum>(checked((uint)value));
                case sizeof(ushort):
                    return AsUnsafe<TEnum>(checked((ushort)value));
                case sizeof(byte):
                    return AsUnsafe<TEnum>(checked((byte)value));
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        private static sbyte AsSbyteUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, sbyte>(ref value);
        }

        public static sbyte AsSbyte(this Enum value)
        {
            return Convert.ToSByte(value);
        }
        
        private static TEnum AsUnsafe<TEnum>(sbyte value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<sbyte, TEnum>(ref value);
        }

        public static sbyte AsSbyte<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(sbyte))
                return AsSbyteUnsafe(value);

            switch (size)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum As<TEnum>(sbyte value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();

            switch (size)
            {
                case sizeof(sbyte):
                    return AsUnsafe<TEnum>(checked((sbyte)value));
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        private static short AsShortUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, short>(ref value);
        }

        public static short AsShort(this Enum value)
        {
            return Convert.ToInt16(value);
        }
        
        private static TEnum AsUnsafe<TEnum>(short value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<short, TEnum>(ref value);
        }

        public static short AsShort<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(short))
                return AsShortUnsafe(value);

            switch (size)
            {
                case sizeof(sbyte):
                    return AsSbyteUnsafe(value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum As<TEnum>(short value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();

            switch (size)
            {
                case sizeof(short):
                    return AsUnsafe<TEnum>(checked((short)value));
                case sizeof(sbyte):
                    return AsUnsafe<TEnum>(checked((sbyte)value));
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        private static int AsIntUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, int>(ref value);
        }

        public static int AsInt(this Enum value)
        {
            return Convert.ToInt32(value);
        }
        
        private static TEnum AsUnsafe<TEnum>(int value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<int, TEnum>(ref value);
        }

        public static int AsInt<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(int))
                return AsIntUnsafe(value);

            switch (size)
            {
                case sizeof(sbyte):
                    return AsSbyteUnsafe(value);
                case sizeof(short):
                    return AsShortUnsafe(value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum As<TEnum>(int value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();

            switch (size)
            {
                case sizeof(int):
                    return AsUnsafe<TEnum>(checked((int)value));
                case sizeof(short):
                    return AsUnsafe<TEnum>(checked((short)value));
                case sizeof(sbyte):
                    return AsUnsafe<TEnum>(checked((sbyte)value));
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        private static long AsLongUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, long>(ref value);
        }

        public static long AsLong(this Enum value)
        {
            return Convert.ToInt64(value);
        }
        
        private static TEnum AsUnsafe<TEnum>(long value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<long, TEnum>(ref value);
        }

        public static long AsLong<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(long))
                return AsLongUnsafe(value);

            switch (size)
            {
                case sizeof(sbyte):
                    return AsSbyteUnsafe(value);
                case sizeof(short):
                    return AsShortUnsafe(value);
                case sizeof(int):
                    return AsIntUnsafe(value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum As<TEnum>(long value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();

            switch (size)
            {
                case sizeof(long):
                    return AsUnsafe<TEnum>(checked((long)value));
                case sizeof(int):
                    return AsUnsafe<TEnum>(checked((int)value));
                case sizeof(short):
                    return AsUnsafe<TEnum>(checked((short)value));
                case sizeof(sbyte):
                    return AsUnsafe<TEnum>(checked((sbyte)value));
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

    }
}