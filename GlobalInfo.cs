﻿using System.Runtime.InteropServices;
using System.IO;

namespace GPPInstaller
{
    static class GlobalInfo
    {
        // NOTE: These are all valid as of GPP v1.5.99 and GPPInstaller v1.0.0

        // Compatable KSP Versions
        public static string compatableKSPVersion = "1.3.1";
        public static string compatableKSPEXE = "64";

        // index for modList
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

        // download links GPPInstaller 1.0.0 
        public static string kopernicusDownloadLink = "https://github.com/Kopernicus/Kopernicus/releases/download/release-1.3.1-3/Kopernicus-1.3.1-3.zip";
        public static string gppDownloadLink = "https://github.com/Galileo88/Galileos-Planet-Pack/releases/download/1.6.0.1/Galileos-Planet-Pack-1.6.0.1.zip";
        public static string gppTexturesDownloadLink = "https://github.com/Galileo88/Galileos-Planet-Pack/releases/download/4.0.0/GPP_Textures-4.0.0.zip";
        public static string eveDownloadLink = "https://github.com/WazWaz/EnvironmentalVisualEnhancements/releases/download/EVE-1.2.2-1/EnvironmentalVisualEnhancements-1.2.2.1.zip";
        public static string scattererDownloadLink = "https://spacedock.info/mod/141/scatterer/download/0.0320b";
        public static string doeDownloadLink = "https://github.com/MOARdV/DistantObject/releases/download/v1.9.1/DistantObject_1.9.1.zip";
        public static string kerDownloadLink = "https://github.com/CYBUTEK/KerbalEngineer/releases/download/1.1.3.0/KerbalEngineer-1.1.3.0.zip";
        public static string kacDownloadLink = "https://github.com/TriggerAu/KerbalAlarmClock/releases/download/v3.8.5.0/KerbalAlarmClock_3.8.5.0.zip";

        // archive names GPPInstaller 1.0.0
        public static string kopernicusZip = "Kopernicus-1.3.1-3.zip";
        public static string gppZip = "Galileos-Planet-Pack-1.6.0.1.zip";
        public static string gppTexturesZip = "GPP_Textures-4.0.0.zip";
        public static string eveZip = "EnvironmentalVisualEnhancements-1.2.2.1.zip";
        public static string scattererZip = "scatterer-0.0320b.zip";
        public static string doeZip = "DistantObject_1.9.1.zip";
        public static string kerZip = "KerbalEngineer-1.1.3.0.zip";
        public static string kacZip = "KerbalAlarmClock_3.8.5.0.zip";

        // extracted names GPPInstaller 1.0.0
        public static string kopernicusExtracted = "Kopernicus-1.3.1-3";
        public static string gppExtracted = "Galileos-Planet-Pack-1.6.0.1";
        public static string gppTexturesExtracted = "GPP_Textures-4.0.0";
        public static string eveExtracted = "EnvironmentalVisualEnhancements-1.2.2.1";
        public static string scattererExtracted = "scatterer-0.0320b";
        public static string doeExtracted = "DistantObject_1.9.1";
        public static string cloudsLowResExtracted = "GPP_Clouds";
        public static string cloudsHighResExtracted = "GPP_Clouds";
        public static string kerExtracted = "KerbalEngineer-1.1.3.0";
        public static string kacExtracted = "KerbalAlarmClock_3.8.5.0";

        // release page
        public static string kopernicusUrl = "https://github.com/Kopernicus/Kopernicus/releases";
        public static string gppUrl = "https://github.com/Galileo88/Galileos-Planet-Pack/releases";
        public static string gppTexturesUrl = "https://github.com/Galileo88/Galileos-Planet-Pack/releases/tag/3.0.0";
        public static string eveUrl = "https://github.com/WazWaz/EnvironmentalVisualEnhancements/releases";
        public static string scattererUrl = "https://spacedock.info/mod/141/scatterer";
        public static string doeUrl = "https://github.com/MOARdV/DistantObject/releases";
        public static string kerUrl = "https://github.com/CYBUTEK/KerbalEngineer/releases";
        public static string kacUrl = "https://github.com/TriggerAu/KerbalAlarmClock/releases";

        // xpath to download
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
            int trailingStart = zipFile.LastIndexOf('.');
            int count = zipFile.Length - trailingStart;
            string result = zipFile.Remove(trailingStart, count);

            return result;
        }

    }
}
