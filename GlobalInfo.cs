using System.Runtime.InteropServices;
using System.IO;

namespace GPPInstaller
{
    static class GlobalInfo
    {
        // NOTE: These are all valid as of GPP v1.5.99 and GPPInstaller v1.0.0

        // Compatable KSP Versions
        public static string compatableKSPVersion = "1.3.1";
        public static string compatableKSPEXE = "64";

        public static int kopericusIndex = 0;
        public static int gppIndex = 1;
        public static int gppTexturesIndex = 2;
        public static int eveIndex = 3;
        public static int scattererIndex = 4;
        public static int doeIndex = 5;
        public static int cloudsLowResIndex = 6;
        public static int cloudsHighResIndex = 7;
        public static int kerIndex = 8;
        public static int kacIndex = 9;

        public static string kopernicusUrl = "https://github.com/Kopernicus/Kopernicus/releases";
        public static string gppUrl = "https://github.com/Galileo88/Galileos-Planet-Pack/releases";
        public static string gppTexturesUrl = "https://github.com/Galileo88/Galileos-Planet-Pack/releases/tag/3.0.0";
        public static string eveUrl = "https://github.com/WazWaz/EnvironmentalVisualEnhancements/releases";
        public static string scattererUrl = "https://spacedock.info/mod/141/scatterer";
        public static string doeUrl = "https://github.com/MOARdV/DistantObject/releases";
        public static string kerUrl = "https://github.com/CYBUTEK/KerbalEngineer/releases";
        public static string kacUrl = "https://github.com/TriggerAu/KerbalAlarmClock/releases";

        public static string kopernicusXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div[1]/div[2]/div[2]/ul/li[1]/a";
        public static string gppXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div[1]/div[2]/div[2]/ul/li[1]/a";
        public static string gppTexturesXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div/div[2]/div[2]/ul/li[1]/a";
        public static string eveXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div[1]/div[2]/div[2]/ul/li[1]/a";
        public static string scattererXpath = "/html[1]/body[1]/div[3]/div[1]/div[2]/a[1]";
        public static string doeXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div[1]/div[2]/div[2]/ul/li[1]/a";
        public static string kerXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div[1]/div[2]/div[2]/ul/li[1]/a";
        public static string kacXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div[1]/div[2]/div[2]/ul/li[1]/a";

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

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
            if (zipFile != "")
            {
                int trailingStart = zipFile.LastIndexOf('.');
                int count = zipFile.Length - trailingStart;
                string result = zipFile.Remove(trailingStart, count);

                return result;
            }
            else return "";

        }

    }
}
