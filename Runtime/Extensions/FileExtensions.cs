using System.IO;

namespace Dythervin
{
    public static class FileExtensions
    {
        public static bool HasFlagFast(this FileAttributes attributes, FileAttributes flag)
        {
            return (attributes & flag) == flag;
        }
    }
}