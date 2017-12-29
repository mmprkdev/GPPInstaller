using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPPInstaller
{
    static class GlobalInfo
    {
        // Compatable KSP Versions
        public static string[] compatableKSPVersions = { "1.3.1" };
        public static string [] compatableKSPEXEs = { "64" };

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
    }
}
