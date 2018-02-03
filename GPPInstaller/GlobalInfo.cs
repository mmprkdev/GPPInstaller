using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;

namespace GPPInstaller
{
    // Global static variables and utility functions
    static class GlobalInfo
    {
        // Compatable KSP Versions
        public static string compatableKSPVersion = "1.3.1";
        public static string ksp64bitExeFile = @".\KSP_x64.exe";
        public static string ksp32bitExeFile = @".\KSP.exe";

        //public static string dropboxJsonDLLink = "https://www.dropbox.com/s/xf2iqhjzmzl4215/modList.json?dl=1";
        
        [DllImport("wininet.dll")]
        extern static bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool IsConnectedToInternet()
        {
            int description;
            return InternetGetConnectedState(out description, 0);
        }

        public static void DeleteAllZips(string dirPath)
        {
            DirectoryInfo dir = new DirectoryInfo(dirPath);

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.Extension == ".zip") File.Delete(file.FullName);
            }
        }

        public static string RemoveZip(string zipFile)
        {
            int trailingStart = zipFile.LastIndexOf('.');
            int count = zipFile.Length - trailingStart;
            string result = zipFile.Remove(trailingStart, count);

            return result;
        }

        public static string WebsiteNamePop(string url)
        {
            string websiteName;

            int leadStartIndex = 0;
            int leadEndIndex = url.IndexOf("/") + 2;
            int leadCount = leadEndIndex - leadStartIndex;
            websiteName = url.Remove(leadStartIndex, leadCount);

            int trailingStartIndex = websiteName.IndexOf("/");
            int trailingEndIndex = websiteName.Length;
            int trailingCount = trailingEndIndex - trailingStartIndex;
            websiteName = websiteName.Remove(trailingStartIndex, trailingCount);

            return websiteName;
        }

    }
}
