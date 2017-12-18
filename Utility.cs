using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;


// TODO: 
// Start using modList to filter through different
// download and install procedures.

namespace GPPInstaller
{
    class Mod
    {
        public string ModPackName { get; set; }
        public string ModName { get; set; }
        public string DownloadAddress { get; set; }
        public string ArchiveFileName { get; set; }
        // States: "Downloaded" == true : The archive file is present in .\GPPInstaller
        //         "Extracted" == true  : An extracted dir exsists in .\GPPInstaller
        //         "Installed" == true  : All required dirs and files are present inside of .\GameData
        //         ""                   : Initial default state 
        //public string[3] CurrentState { get; set; }
        public bool State_Downloaded { get; set; }
        public bool State_Extracted { get; set; }
        public bool State_Installed { get; set; }
        // Actions: "Install"  : Install the mod
        //          "Uninstall": Uninstall the mod
        //          ""         : Take no action
        public string ActionToTake { get; set; }
    }

    class Utility
    {
        private static int modIndex = 0;
        public static List<Mod> modList = new List<Mod>();

        Dictionary<string, bool> utilProcessedOptions = new Dictionary<string, bool>();

        Form1 form1;

        public Utility(Form1 form1)
        {
            this.form1 = form1;
        }

        // TODO: left off here
        

        public void InitModList()
        {
            modList.Add(new Mod()
            {
                ModPackName = "Core",
                ModName = "Kopernicus",
                DownloadAddress = "https://github.com/Kopernicus/Kopernicus/releases/download/release-1.3.1-2/Kopernicus-1.3.1-2.zip",
                ArchiveFileName = "Kopernicus-1.3.1-2.zip",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModPackName = "Core",
                ModName = "GPP",
                DownloadAddress = "https://github.com/Galileo88/Galileos-Planet-Pack/releases/download/1.5.88/Galileos.Planet.Pack.1.5.88.zip",
                ArchiveFileName = "Galileos.Planet.Pack.1.5.88.zip",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModPackName = "Core",
                ModName = "GPP_Textures",
                DownloadAddress = "https://github.com/Galileo88/Galileos-Planet-Pack/releases/download/3.0.0/GPP_Textures-3.0.0.zip",
                ArchiveFileName = "GPP_Textures-3.0.0.zip",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModPackName = "Visuals",
                ModName = "EVE",
                DownloadAddress = "https://github.com/WazWaz/EnvironmentalVisualEnhancements/releases/download/EVE-1.2.2-1/EnvironmentalVisualEnhancements-1.2.2.1.zip",
                ArchiveFileName = "EnvironmentalVisualEnhancements-1.2.2.1.zip",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModPackName = "Visuals",
                ModName = "Scatterer",
                DownloadAddress = "https://spacedock.info/mod/141/scatterer/download/0.0320b",
                ArchiveFileName = "scatterer-0.0320b.zip",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModPackName = "Visuals",
                ModName = "DistantObjectEnhancement",
                DownloadAddress = "https://github.com/MOARdV/DistantObject/releases/download/v1.9.1/DistantObject_1.9.1.zip",
                ArchiveFileName = "DistantObject_1.9.1.zip",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModPackName = "Visuals",
                ModName = "CloudsLowRes",
                DownloadAddress = "",
                ArchiveFileName = "",
                State_Downloaded = true,
                State_Extracted = true,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModPackName = "Visuals",
                ModName = "CloudsHighRes",
                DownloadAddress = "",
                ArchiveFileName = "",
                State_Downloaded = true,
                State_Extracted = true,
                State_Installed = false,
                ActionToTake = ""
            });
        }

        public void DownloadFiles()
        {
            if (modList.Count == 0) return;

            form1.ProgressBar1Init(modList.Count);

            if ()
            {

            }

            //for (int i = 0; i < modPack.Count; i++)
            //{
            //    string downloadAddress = modPack[i].DownloadAddress;
            //    string fileName = modPack[i].FileName;

            //    string gppDir = ".\\GPPInstaller";
            //    string downloadDest = gppDir + "\\" + fileName;

            //    if (!File.Exists(downloadDest))
            //    {
            //        WebClient webclient = new WebClient();
            //        Uri uri = new Uri(downloadAddress);

            //        webclient.DownloadFileCompleted += DownloadFileComplete(modPack[i], form1);
            //        webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
            //        //await Task.Factory.StartNew(() => webclient.DownloadFileAsync(uri, downloadDest));
            //        // TODO: Try placing logic here that is currently being run in the closure. 
            //    }
            //    else
            //    {
            //        modIndex++;
            //        form1.ProgressBar1Step();

            //        if (modIndex >= modPack.Count)
            //        {
            //            form1.ProgressLabelUpdate("All Downloads complete.");

            //            ExtractFiles();
            //        }
            //    }
            //}
            // NOTE: After await, the rest of the function is still exicuted, still not sure what
            // the threads are doing.
        }

        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            string output = e.ProgressPercentage + "% complete...";
            form1.ProgressLabelUpdate(output);
        }

        public static AsyncCompletedEventHandler DownloadFileComplete(Mod mod, Form1 form1)
        {
            // NOTE: Closure example
            Action<object, AsyncCompletedEventArgs> action = (sender, e) =>
            {
                switch (mod.ModName)
                {
                    case "Kopernicus":
                        mod.State = "Downloaded";
                        break;
                    case "GPP":
                        mod.State = "Downloaded";
                        break;
                    case "GPP_Textures":
                        mod.State = "Downloaded";
                        break;
                    default:
                        break;
                }

                form1.ProgressBar1Step();

                modIndex++;

                if (modIndex >= modList.Count)
                {
                    form1.ProgressLabelUpdate("All Downloads complete.");

                    Utility util = new Utility(form1);
                    util.ExtractFiles();
                }
            };
            return new AsyncCompletedEventHandler(action);
        }

        private async void ExtractFiles()
        {
            modIndex = 0;

            for (int i = 0; i < modList.Count; i++)
            {
                string fileName = modList[i].ArchiveFileName;
                int fileNameLength = fileName.Length;
                string dirName = fileName.Remove((fileNameLength - 4), 4);
                string destDir = @".\GPPInstaller\" + dirName;

                DirectoryInfo destDirInfo = new DirectoryInfo(destDir);

                string zipFile = @".\GPPInstaller\" + fileName;

                if (!destDirInfo.Exists)
                {
                    Directory.CreateDirectory(destDir);
                    // TODO: Fix "Central directory corrupt" exception
                    await Task.Factory.StartNew(() => ZipFile.ExtractToDirectory(zipFile, destDir));

                    modIndex++;
                    form1.ProgressBar1Step();
                    if (modIndex >= modList.Count)
                    {
                        form1.ProgressLabelUpdate("All files extracted");

                        InstallMods();
                    }
                        
                }
                else
                {
                    modIndex++;
                    form1.ProgressBar1Step();
                    if (modIndex >= modList.Count)
                    {
                        form1.ProgressLabelUpdate("All files extracted");

                        InstallMods();
                    }
                }
            }
        }

        private async void InstallMods()
        {
            modIndex = 0;

            string sourceDirName;
            string destDirName;

            for (int i = 0; i < modList.Count; i++)
            {
                string modName = modList[i].ModName;
                string fileName = modList[i].ArchiveFileName;
                int fileNameLength = fileName.Length;
                string dirName = fileName.Remove((fileNameLength - 4), 4);

                if (modName == "GPP_Textures")
                {
                    sourceDirName = @".\GPPInstaller\" + dirName + @"\GameData\GPP";
                    destDirName = @".\GameData\GPP";
                }
                else
                {
                    sourceDirName = @".\GPPInstaller\" + @"\" + dirName + @"\GameData";
                    destDirName = @".\GameData";
                }

                string cloudDirSource;
                string cloudDirDest;

                if (utilProcessedOptions.ContainsKey("CloudsLowRes"))
                {
                    if (utilProcessedOptions["CloudsLowRes"] == true)
                    {
                        if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);

                        cloudDirSource = @".\GPPInstaller\Galileos.Planet.Pack.1.5.88\Optional Mods\GPP_Clouds\Low-res Clouds_GameData inside\GameData\GPP";
                        cloudDirDest = @".\GameData\GPP";

                        DirectoryCopy(cloudDirSource, cloudDirDest, true);
                    }
                }

                if (utilProcessedOptions.ContainsKey("CloudsHighRes"))
                {
                    if (utilProcessedOptions["CloudsHighRes"] == true)
                    {
                        if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);

                        cloudDirSource = @".\GPPInstaller\Galileos.Planet.Pack.1.5.88\Optional Mods\GPP_Clouds\High-res Clouds_GameData inside\GameData\GPP";
                        cloudDirDest = @".\GameData\GPP";

                        DirectoryCopy(cloudDirSource, cloudDirDest, true);
                    }
                }
                

                await Task.Factory.StartNew(() => DirectoryCopy(sourceDirName, destDirName, true));

                Debug.WriteLine("Line after DirectoryCopy");
                modIndex++;
                form1.ProgressBar1Step();

                if (modIndex >= modList.Count) form1.ProgressLabelUpdate("All mods installed");
            }
            
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist: " + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destDirName))
            {
                //throw new DirectoryNotFoundException("Destination directory does not exist: " + destDirName);
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subDir.Name);
                    DirectoryCopy(subDir.FullName, tempPath, copySubDirs);
                }
            }
        }

        public string GetEXE()
        {
            string target = ".\\KSP_x64.exe";

            if (!File.Exists(target))
            {
                return "Error: 64 bit version of KSP required.";
            }

            string result = "64 bit";
            return result;
        }

        public string GetVersionNumber()
        {
            string target = ".\\readme.txt";

            if (!File.Exists(target))
            {
                return "Error: Could not determine KSP version";
            }

            string[] readmeLines = File.ReadAllLines(target);

            string versionNumberLine = readmeLines[14];

            char[] versionChars = new char[5];
            for (int lineI = 8, charI = 0; lineI <= 12; lineI++, charI++)
            {
                versionChars[charI] = versionNumberLine[lineI];
            }

            string versionNumber = new string(versionChars);

            return versionNumber;
        }


        public void Uninstall(Dictionary<string, bool> processedOptions)
        {
            if (processedOptions.ContainsKey("Core") &&
                processedOptions["Core"] == false)
            {
                if (Directory.Exists(".\\GameData\\Kopernicus")) Directory.Delete(".\\GameData\\Kopernicus", true);
                if (Directory.Exists(".\\GameData\\ModularFlightIntegrator")) Directory.Delete(".\\GameData\\ModularFlightIntegrator", true);
                if (Directory.Exists(".\\GameData\\GPP")) Directory.Delete(".\\GameData\\GPP", true);

                File.Delete(".\\GameData\\ModuleManager.2.8.1.dll");
                File.Delete(".\\GameData\\ModuleManagerLicense.md");
                File.Delete(".\\GameData\\ModuleManager.ConfigCache");
                File.Delete(".\\GameData\\ModuleManager.ConfigSHA");
                File.Delete(".\\GameData\\ModuleManager.Physics");
                File.Delete(".\\GameData\\ModuleManager.TechTree");

                Debug.WriteLine("Core uninstallation complete");
            }

            if (processedOptions.ContainsKey("Visuals") &&
                processedOptions["Visuals"] == false)
            {
                if (Directory.Exists(@".\GameData\DistantObject")) Directory.Delete(@".\GameData\DistantObject", true);
                if (Directory.Exists(@".\GameData\EnvironmentalVisualEnhancements")) Directory.Delete(@".\GameData\EnvironmentalVisualEnhancements", true);
                if (Directory.Exists(@".\GameData\scatterer")) Directory.Delete(@".\GameData\scatterer", true);

                Debug.WriteLine("Visuals uninstallation complete");
            }

            if (processedOptions.ContainsKey("CloudsLowRes") &&
                processedOptions["CloudsLowRes"] == false)
            {
                if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);
            }

            if (processedOptions.ContainsKey("CloudsHighRes") &&
                processedOptions["CloudsHighRes"] == false)
            {
                if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);
            }
        }

        public Dictionary<string, bool> ProcessOptions(Dictionary<string, bool> currentlyInstalledOptions, 
            Dictionary<string, bool> newlySelectedOptions)
        {
            //Dictionary<string, bool> processedOptions = new Dictionary<string, bool>();

            var processedOptions = newlySelectedOptions.Where(entry => currentlyInstalledOptions[entry.Key] != entry.Value)
                .ToDictionary(entry => entry.Key, entry => entry.Value);

            return processedOptions;
            //utilProcessedOptions = processedOptions;

            //Uninstall(processedOptions);

            //BuildInstallPack(processedOptions);

            //numberOfTasks = modPack.Count * 3;
            

            //DownloadFiles();

            //Debug.WriteLine("End of ProcessOptions()");
        }
    }
}
