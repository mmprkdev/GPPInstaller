using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPPInstaller
{
    class ModListInit : IModListInit
    {
        Form1 _form1;

        public ModListInit(Form1 form1)
        {
            _form1 = form1;
        }

        public void InitModList()
        {
            _form1.modList.Add(new Mod()
            {
                ModType = "Core",
                ModName = "Kopernicus",
                DownloadAddress = GlobalInfo.kopernicusDownloadLink,
                ArchiveFileName = GlobalInfo.kopernicusZip,
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = GlobalInfo.kopernicusExtracted,
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "Kopernicus",
                InstallDestPath = @".\GameData",
                InstallSourcePath = GlobalInfo.koperincusInstallSource,
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = GlobalInfo.kopernicusVersion,
                IsCurrentVersion = false
            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Core",
                ModName = "GPP",
                DownloadAddress = GlobalInfo.gppDownloadLink,
                ArchiveFileName = GlobalInfo.gppZip,
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = GlobalInfo.gppExtracted,
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "GPP",
                InstallDestPath = @".\GameData",
                InstallSourcePath = GlobalInfo.gppInstallSource,
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = GlobalInfo.gppVersion,
                IsCurrentVersion = false
            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Core",
                ModName = "GPP_Textures",
                DownloadAddress = GlobalInfo.gppTexturesDownloadLink,
                ArchiveFileName = GlobalInfo.gppTexturesZip,
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = GlobalInfo.gppTexturesExtracted,
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "GPP_Textures",
                InstallDestPath = @".\GameData\GPP",
                InstallSourcePath = GlobalInfo.gppTexturesInstallSource,
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = GlobalInfo.gppTexturesVersion,
                IsCurrentVersion = false
            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Visuals",
                ModName = "EVE",
                DownloadAddress = GlobalInfo.eveDownloadLink,
                ArchiveFileName = GlobalInfo.eveZip,
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = GlobalInfo.eveExtracted,
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "EnvironmentalVisualEnhancements",
                InstallDestPath = @".\GameData",
                InstallSourcePath = GlobalInfo.eveInstallSource,
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = GlobalInfo.eveVersion,
                IsCurrentVersion = false
            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Visuals",
                ModName = "Scatterer",
                DownloadAddress = GlobalInfo.scattererDownloadLink,
                ArchiveFileName = GlobalInfo.scattererZip,
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = GlobalInfo.scattererExtracted,
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "scatterer",
                InstallDestPath = @".\GameData",
                InstallSourcePath = GlobalInfo.scattererInstallSource,
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = GlobalInfo.scattererVersion,
                IsCurrentVersion = false

            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Visuals",
                ModName = "DistantObject",
                DownloadAddress = GlobalInfo.doeDownloadLink,
                ArchiveFileName = GlobalInfo.doeZip,
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = GlobalInfo.doeExtracted,
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "DistantObject",
                InstallDestPath = @".\GameData",
                InstallSourcePath = GlobalInfo.doeInstallSource,
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = GlobalInfo.doeVersion,
                IsCurrentVersion = false
            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Clouds",
                ModName = "CloudsLowRes",
                DownloadAddress = "",
                ArchiveFileName = "",
                ArchiveFilePath = "",
                ExtractedDirName = GlobalInfo.cloudsLowResExtracted,
                ExtractedPath = "",
                InstallDirName = "GPP_Clouds",
                InstallDestPath = @".\GameData\GPP",
                InstallSourcePath = GlobalInfo.cloudsLowResInstallSource,
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = ""
            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Clouds",
                ModName = "CloudsHighRes",
                DownloadAddress = "",
                ArchiveFileName = "",
                ArchiveFilePath = "",
                ExtractedDirName = GlobalInfo.cloudsHighResExtracted,
                ExtractedPath = "",
                InstallDirName = "GPP_Clouds",
                InstallDestPath = @".\GameData\GPP",
                InstallSourcePath = GlobalInfo.cloudsHighResInstallSource,
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = ""
            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Utility",
                ModName = "KerbalEngineer",
                DownloadAddress = GlobalInfo.kerDownloadLink,
                ArchiveFileName = GlobalInfo.kerZip,
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = GlobalInfo.kerExtracted,
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "KerbalEngineer",
                InstallDestPath = @".\GameData",
                InstallSourcePath = GlobalInfo.kerInstallSource,
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = GlobalInfo.kerVersion,
                IsCurrentVersion = false
            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Utility",
                ModName = "KerbalAlarmClock",
                DownloadAddress = GlobalInfo.kacDownloadLink,
                ArchiveFileName = GlobalInfo.kacZip,
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = GlobalInfo.kacExtracted,
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "TriggerTech",
                InstallDestPath = @".\GameData",
                InstallSourcePath = GlobalInfo.kacInstallSource,
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = GlobalInfo.kacVersion,
                IsCurrentVersion = false
            });

        }
    }
}
