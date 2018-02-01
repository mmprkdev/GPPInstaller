using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPPInstaller
{
    public class Mod
    {
        // ActionToTake: "Install"
        //               "Uninstall"
        // States: "Downloaded" == true : The archive file is present in .\GPPInstaller
        //         "Extracted" == true  : An extracted dir exsists in .\GPPInstaller
        //         "Installed" == true  : All required dirs and files are present inside of .\GameData
        //         ""                   : Initial default state 
        // OldExtractedDirName: The name of the old 
        public string ModType { get; set; }
        public string ModName { get; set; }
        public string DownloadAddress { get; set; }
        public string ArchiveFilePath { get; set; }
        public string ArchiveFileName { get; set; }
        public string ExtractedPath { get; set; }
        public string ExtractedDirName { get; set; }
        public string InstallSourcePath { get; set; }
        public string InstallDestPath { get; set; }
        public string InstallDirName { get; set; }
        public bool State_Downloaded { get; set; }
        public bool State_Extracted { get; set; }
        public bool State_Installed { get; set; }
        public string ActionToTake { get; set; }
        public string VersionNumber { get; set; }
        public bool IsCurrentVersion { get; set; }
    }

}
