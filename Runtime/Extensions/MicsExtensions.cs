namespace Dythervin.Core.Utils
{
    public static class MicsExtensions
    {
        public static byte ToByte(this bool b)
        {
            return b ? (byte)1 : (byte)0;
        }

        public static bool ToBool(this byte b)
        {
            return b != 0;
        }
    }
}