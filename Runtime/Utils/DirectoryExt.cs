using System.IO;

namespace Dythervin.Core.Utils
{
    public static class DirectoryExt
    {
        public static void EnsureFolder(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}