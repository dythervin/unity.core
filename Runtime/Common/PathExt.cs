using System.IO;

namespace Dythervin
{
    public static class PathExt
    {
        private static readonly char[]
            _pathSeparators = { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };

        public static string GetFileNameWithoutAllExtensions(string path)
        {
            int fileNameStart = path.LastIndexOfAny(_pathSeparators) + 1;
            int fileNameEnd = path.IndexOf('.', fileNameStart);
            return path.Substring(fileNameStart, fileNameEnd - fileNameStart);
        }

        public static void GetSize(DirectoryInfo directory, out long filesCount, out long size)
        {
            size = 0;
            filesCount = 0;

            foreach (FileInfo file in directory.GetFiles("*", SearchOption.AllDirectories))
            {
                size += file.Length;
                filesCount++;
            }
        }
    }
}