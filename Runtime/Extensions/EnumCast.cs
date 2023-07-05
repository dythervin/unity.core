using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Dythervin.Core.Extensions
{
    public static class EnumCast
    {
        private static byte ToByteUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, byte>(ref value);
        }

        public static byte ToByte(this Enum value)
        {
            return Convert.ToByte(value);
        }
        
        private static TEnum ToUnsafe<TEnum>(byte value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<byte, TEnum>(ref value);
        }

        public static byte ToByte<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(byte))
                return ToByteUnsafe(value);

            switch (size)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum To<TEnum>(byte value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size <= sizeof(byte))
                return ToUnsafe<TEnum>(value);

            switch (size)
            {
                case 2:
                    return ToUnsafe<TEnum>((ushort)value);
                case 4:
                    return ToUnsafe<TEnum>((uint)value);
                case 8:
                    return ToUnsafe<TEnum>((ulong)value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        private static ushort ToUshortUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, ushort>(ref value);
        }

        public static ushort ToUshort(this Enum value)
        {
            return Convert.ToUInt16(value);
        }
        
        private static TEnum ToUnsafe<TEnum>(ushort value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<ushort, TEnum>(ref value);
        }

        public static ushort ToUshort<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(ushort))
                return ToUshortUnsafe(value);

            switch (size)
            {
                case 1:
                    return ToByteUnsafe(value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum To<TEnum>(ushort value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size <= sizeof(ushort))
                return ToUnsafe<TEnum>(value);

            switch (size)
            {
                case 4:
                    return ToUnsafe<TEnum>((uint)value);
                case 8:
                    return ToUnsafe<TEnum>((ulong)value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        private static uint ToUintUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, uint>(ref value);
        }

        public static uint ToUint(this Enum value)
        {
            return Convert.ToUInt32(value);
        }
        
        private static TEnum ToUnsafe<TEnum>(uint value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<uint, TEnum>(ref value);
        }

        public static uint ToUint<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(uint))
                return ToUintUnsafe(value);

            switch (size)
            {
                case 1:
                    return ToByteUnsafe(value);
                case 2:
                    return ToUshortUnsafe(value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum To<TEnum>(uint value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size <= sizeof(uint))
                return ToUnsafe<TEnum>(value);

            switch (size)
            {
                case 8:
                    return ToUnsafe<TEnum>((ulong)value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        private static ulong ToUlongUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, ulong>(ref value);
        }

        public static ulong ToUlong(this Enum value)
        {
            return Convert.ToUInt64(value);
        }
        
        private static TEnum ToUnsafe<TEnum>(ulong value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<ulong, TEnum>(ref value);
        }

        public static ulong ToUlong<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(ulong))
                return ToUlongUnsafe(value);

            switch (size)
            {
                case 1:
                    return ToByteUnsafe(value);
                case 2:
                    return ToUshortUnsafe(value);
                case 4:
                    return ToUintUnsafe(value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum To<TEnum>(ulong value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size <= sizeof(ulong))
                return ToUnsafe<TEnum>(value);

            switch (size)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        private static sbyte ToSbyteUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, sbyte>(ref value);
        }

        public static sbyte ToSbyte(this Enum value)
        {
            return Convert.ToSByte(value);
        }
        
        private static TEnum ToUnsafe<TEnum>(sbyte value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<sbyte, TEnum>(ref value);
        }

        public static sbyte ToSbyte<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(sbyte))
                return ToSbyteUnsafe(value);

            switch (size)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum To<TEnum>(sbyte value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size <= sizeof(sbyte))
                return ToUnsafe<TEnum>(value);

            switch (size)
            {
                case 2:
                    return ToUnsafe<TEnum>((short)value);
                case 4:
                    return ToUnsafe<TEnum>((int)value);
                case 8:
                    return ToUnsafe<TEnum>((long)value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        private static short ToShortUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, short>(ref value);
        }

        public static short ToShort(this Enum value)
        {
            return Convert.ToInt16(value);
        }
        
        private static TEnum ToUnsafe<TEnum>(short value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<short, TEnum>(ref value);
        }

        public static short ToShort<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(short))
                return ToShortUnsafe(value);

            switch (size)
            {
                case 1:
                    return ToSbyteUnsafe(value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum To<TEnum>(short value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size <= sizeof(short))
                return ToUnsafe<TEnum>(value);

            switch (size)
            {
                case 4:
                    return ToUnsafe<TEnum>((int)value);
                case 8:
                    return ToUnsafe<TEnum>((long)value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        private static int ToIntUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, int>(ref value);
        }

        public static int ToInt(this Enum value)
        {
            return Convert.ToInt32(value);
        }
        
        private static TEnum ToUnsafe<TEnum>(int value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<int, TEnum>(ref value);
        }

        public static int ToInt<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(int))
                return ToIntUnsafe(value);

            switch (size)
            {
                case 1:
                    return ToSbyteUnsafe(value);
                case 2:
                    return ToShortUnsafe(value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum To<TEnum>(int value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size <= sizeof(int))
                return ToUnsafe<TEnum>(value);

            switch (size)
            {
                case 8:
                    return ToUnsafe<TEnum>((long)value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        private static long ToLongUnsafe<TEnum>(this TEnum value)
        {
            return UnsafeUtility.As<TEnum, long>(ref value);
        }

        public static long ToLong(this Enum value)
        {
            return Convert.ToInt64(value);
        }
        
        private static TEnum ToUnsafe<TEnum>(long value)
            where TEnum : Enum
        {
            return UnsafeUtility.As<long, TEnum>(ref value);
        }

        public static long ToLong<TEnum>(this TEnum value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size >= sizeof(long))
                return ToLongUnsafe(value);

            switch (size)
            {
                case 1:
                    return ToSbyteUnsafe(value);
                case 2:
                    return ToShortUnsafe(value);
                case 4:
                    return ToIntUnsafe(value);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public static TEnum To<TEnum>(long value)
            where TEnum : unmanaged, Enum
        {
            int size = UnsafeUtility.SizeOf<TEnum>();
            if (size <= sizeof(long))
                return ToUnsafe<TEnum>(value);

            switch (size)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

    }
}