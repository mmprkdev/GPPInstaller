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

        public void InitModList(Form1 form1)
        {
            _form1 = form1;

            _form1.modList.Add(new Mod()
            {
                ModType = "Core",
                ModName = "Kopernicus",
                DownloadAddress = "https://github.com/Kopernicus/Kopernicus/releases/download/release-1.3.1-3/Kopernicus-1.3.1-3.zip",
                ArchiveFileName = "Kopernicus-1.3.1-3.zip",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "Kopernicus-1.3.1-3",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "Kopernicus",
                InstallDestPath = @".\GameData",
                InstallSourcePath = @".\GPPInstaller\Kopernicus-1.3.1-3\GameData",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = "1.3.1-3",
                IsCurrentVersion = false
            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Core",
                ModName = "GPP",
                DownloadAddress = "https://github.com/Galileo88/Galileos-Planet-Pack/releases/download/1.6.0.1/Galileos-Planet-Pack-1.6.0.1.zip",
                ArchiveFileName = "Galileos-Planet-Pack-1.6.0.1.zip",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "Galileos-Planet-Pack-1.6.0.1",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "GPP",
                InstallDestPath = @".\GameData",
                InstallSourcePath = @".\GPPInstaller\Galileos-Planet-Pack-1.6.0.1\GameData",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = "1.6.0.1",
                IsCurrentVersion = false
            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Core",
                ModName = "GPP_Textures",
                DownloadAddress = "https://github.com/Galileo88/Galileos-Planet-Pack/releases/download/4.0.0/GPP_Textures-4.0.0.zip",
                ArchiveFileName = "GPP_Textures-4.0.0.zip",
                ArchiveFilePath = @".\GameData",
                ExtractedDirName = "",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "GPP_Textures",
                InstallDestPath = @".\GameData\GPP",
                InstallSourcePath = @".\GPPInstaller\GPP_Textures-4.0.0\GameData\GPP",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = "4.0.0",
                IsCurrentVersion = false
            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Core",
                ModName = "KSCSwitcher",
                DownloadAddress = "",
                ArchiveFileName = "",
                ArchiveFilePath = "",
                ExtractedDirName = "KSCSwitcher",
                ExtractedPath = @".\GPPInstaller\Galileos-Planet-Pack-1.6.0.1\Optional Mods\GPP_KSCSwitcher\GameData",
                InstallDirName = "KSCSwitcher",
                InstallDestPath = @".\GameData",
                InstallSourcePath = @".\GPPInstaller\Galileos-Planet-Pack-1.6.0.1\Optional Mods\GPP_KSCSwitcher\GameData",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = "",
                IsCurrentVersion = false
            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Visuals",
                ModName = "EVE",
                DownloadAddress = "https://github.com/WazWaz/EnvironmentalVisualEnhancements/releases/download/EVE-1.2.2-1/EnvironmentalVisualEnhancements-1.2.2.1.zip",
                ArchiveFileName = "EnvironmentalVisualEnhancements-1.2.2.1.zip",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "EnvironmentalVisualEnhancements-1.2.2.1",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "EnvironmentalVisualEnhancements",
                InstallDestPath = @".\GameData",
                InstallSourcePath = @".\GPPInstaller\EnvironmentalVisualEnhancements-1.2.2.1\GameData",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = "1.2.2.1",
                IsCurrentVersion = false
            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Visuals",
                ModName = "Scatterer",
                DownloadAddress = "https://spacedock.info/mod/141/scatterer/download/0.0320b",
                ArchiveFileName = "scatterer-0.0320b.zip",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "scatterer-0.0320b",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "scatterer",
                InstallDestPath = @".\GameData",
                InstallSourcePath = @".\GPPInstaller\scatterer-0.0320b\GameData",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = "0.0320b",
                IsCurrentVersion = false

            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Visuals",
                ModName = "SigmaSkyBox",
                DownloadAddress = "https://github.com/Sigma88/Sigma-Replacements/releases/download/B_v0.2.0/Sigma-Replacements_SkyBox.v0.2.0.zip",
                ArchiveFileName = "Sigma-Replacements_SkyBox.v0.2.0.zip",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "Sigma-Replacements_SkyBox.v0.2.0",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "Sigma",
                InstallDestPath = @".\GameData",
                InstallSourcePath = @".\GPPInstaller\Sigma-Replacements_SkyBox.v0.2.0\GameData",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = "0.2.0",
                IsCurrentVersion = false
            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Clouds",
                ModName = "CloudsLowRes",
                DownloadAddress = "",
                ArchiveFileName = "",
                ArchiveFilePath = "",
                ExtractedDirName = "GPP_Clouds",
                ExtractedPath = @".\GPPInstaller\Galileos-Planet-Pack-1.6.0.1\Optional Mods\GPP_Clouds\Low-res Clouds_GameData inside\GameData\GPP",
                InstallDirName = "GPP_Clouds",
                InstallDestPath = @".\GameData\GPP",
                InstallSourcePath = @".\GPPInstaller\Galileos-Planet-Pack-1.6.0.1\Optional Mods\GPP_Clouds\Low-res Clouds_GameData inside\GameData\GPP",
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
                ExtractedDirName = "GPP_Clouds",
                ExtractedPath = @".\GPPInstaller\Galileos-Planet-Pack-1.6.0.1\Optional Mods\GPP_Clouds\High-res Clouds_GameData inside\GameData\GPP",
                InstallDirName = "GPP_Clouds",
                InstallDestPath = @".\GameData\GPP",
                InstallSourcePath = @".\GPPInstaller\Galileos-Planet-Pack-1.6.0.1\Optional Mods\GPP_Clouds\High-res Clouds_GameData inside\GameData\GPP",
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
                DownloadAddress = "https://github.com/CYBUTEK/KerbalEngineer/releases/download/1.1.3.0/KerbalEngineer-1.1.3.0.zip",
                ArchiveFileName = "KerbalEngineer-1.1.3.0.zip",
                ArchiveFilePath = "KerbalEngineer-1.1.3.0",
                ExtractedDirName = "KerbalEngineer-1.1.3.0",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "KerbalEngineer",
                InstallDestPath = @".\GameData",
                InstallSourcePath = @".\GPPInstaller\KerbalEngineer-1.1.3.0",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = "1.1.3.0",
                IsCurrentVersion = false
            });

            _form1.modList.Add(new Mod()
            {
                ModType = "Utility",
                ModName = "KerbalAlarmClock",
                DownloadAddress = "https://github.com/TriggerAu/KerbalAlarmClock/releases/download/v3.8.5.0/KerbalAlarmClock_3.8.5.0.zip",
                ArchiveFileName = "KerbalAlarmClock_3.8.5.0.zip",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "KerbalAlarmClock_3.8.5.0",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "TriggerTech",
                InstallDestPath = @".\GameData",
                InstallSourcePath = @".\GPPInstaller\KerbalAlarmClock_3.8.5.0\GameData",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = "",
                VersionNumber = "3.8.5.0",
                IsCurrentVersion = false
            });
        }
    }
}
