using System.IO.Compression;

namespace GPPInstaller
{
    static class ZipArchiEntryExtensions
    {
        public static bool IsFolder(this ZipArchiveEntry entry)
        {
            return entry.FullName.EndsWith("/");
        }
    }
}
