using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace GPPInstaller
{
    static class GlobalInfo
    {
        // NOTE: These are all valid as of GPP v1.5.3

        // Compatable KSP Versions
        public static string compatableKSPVersion = "1.3.1";
        public static string compatableKSPEXE = "64";

        public static int kopericusIndex = 0;
        public static int GPPIndex = 1;
        public static int GPPTexturesIndex = 2;
        public static int EVEIndex = 3;
        public static int scattererIndex = 4;
        public static int doeIndex = 5;
        public static int cloudsLowResIndex = 6;
        public static int cloudsHighResIndex = 7;

        // Zip Path
        public static string kopernicusZipPath = @".\GPPInstaller\Kopernicus-1.3.1-2.zip";
        public static string gppZipPath = @".\GPPInstaller\Galileos.Planet.Pack.1.5.88.zip";
        public static string gppTexturesZipPath = @".\GPPInstaller\GPP_Textures-3.0.0.zip";
        public static string eveZipPath = @".\GPPInstaller\EnvironmentalVisualEnhancements-1.2.2.1.zip";
        public static string scattererZipPath = @".\GPPInstaller\scatterer-0.0320b.zip";
        public static string doeZipPath = @".\GPPInstaller\DistantObject_1.9.1.zip";

        // Extract Path
        public static string kopernicusExtractPath = @".\GPPInstaller\Kopernicus-1.3.1-2";
        public static string gppExtractPath = @".\GPPInstaller\Galileos.Planet.Pack.1.5.88";
        public static string gppTexturesExtractPath = @".\GPPInstaller\GPP_Textures-3.0.0";
        public static string eveExtractPath = @".\GPPInstaller\EnvironmentalVisualEnhancements-1.2.2.1";
        public static string scattererExtractPath = @".\GPPInstaller\scatterer-0.0320b";
        public static string doeExtractPath = @".\GPPInstaller\DistantObject_1.9.1";

        // Install Path
        public static string kopernicusInstallPath = @".\GameData\Kopernicus";
        public static string modularFIInstallPath = @".\GameData\ModularFlightIntegrator";
        public static string modManagerInstallPath = @".\GameData\ModuleManager.2.8.1.dll";
        public static string gppInstallPath = @".\GameData\GPP";
        public static string gppTexturesInstallPath = @".\GameData\GPP\GPP_Textures";
        public static string eveInstallPath = @".\GameData\EnvironmentalVisualEnhancements";
        public static string scattererInstallPath = @".\GameData\scatterer";
        public static string doeInstallPath = @".\GameData\DistantObject";
        public static string cloudsLowResInstallPath = @".\GameData\GPP\GPP_Clouds\Configs\GPPClouds_LowRes.cfg";
        public static string cloudsHighResInstallPath = @".\GameData\GPP\GPP_Clouds\Configs\GPPClouds_HighRes.cfg";

        // Number of files in dir
        public static int kopernicusNumOfFiles = 217;
        public static int gppNumOfFiles = 1258;
        public static int gppTexturesNumOfFiles = 126;
        public static int eveNumOfFiles = 14;
        public static int scattererNumOfFiles = 35;
        public static int doeNumOfFiles = 16;

        // Download Links (GPP version 1.5.88)
        public static string kopernicusLink = "https://github.com/Kopernicus/Kopernicus/releases/download/release-1.3.1-2/Kopernicus-1.3.1-2.zip";
        public static string gppLink = "https://github.com/Galileo88/Galileos-Planet-Pack/releases/download/1.5.88/Galileos.Planet.Pack.1.5.88.zip";
        public static string gppTexturesLink = "https://github.com/Galileo88/Galileos-Planet-Pack/releases/download/3.0.0/GPP_Textures-3.0.0.zip";
        public static string eveLink = "https://github.com/WazWaz/EnvironmentalVisualEnhancements/releases/download/EVE-1.2.2-1/EnvironmentalVisualEnhancements-1.2.2.1.zip";
        public static string scattererLink = "https://spacedock.info/mod/141/scatterer/download/0.0320b";
        public static string doeLink = "https://github.com/MOARdV/DistantObject/releases/download/v1.9.1/DistantObject_1.9.1.zip";

        // Download Page


        // Current version numbers of mods (tested as of 1/6/17)
        public static string kopernicusVersion = "1.3.1-2"; // outdated 
        public static string gppVersion = "1.5.88";
        public static string gppTexturesVersion = "3.0.0";
        public static string eveVersion = "1.2.2-1";
        public static string scattererVersion = "0.0329b";
        public static string doeVersion = "1.9.1";

        // Download xpaths


        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool IsConnectedToInternet()
        {
            int description;
            return InternetGetConnectedState(out description, 0);
        }
    }
}
