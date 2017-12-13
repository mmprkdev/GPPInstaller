using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;


namespace GPPInstaller
{
    class Mod
    {
        public string ModPackName { get; set; }
        public string ModName { get; set; }
        public string DownloadAddress { get; set; }
        public string FileName { get; set; }
        public string State { get; set; }
    }

    class Utility
    {
        private static int modIndex = 0;
        public static List<Mod> modPack = new List<Mod>();

        Form1 form1;

        public Utility(Form1 form1)
        {
            this.form1 = form1;
        }

        // NOTE: Different States: Uninstalled (default), Downloaded, Installed
        public void BuildModPack(string modPackName)
        {
            if (modPackName == "Core")
            {
                modPack.Add(new Mod()
                {
                    ModPackName = "Core",
                    ModName = "Kopernicus",
                    DownloadAddress = "https://github.com/Kopernicus/Kopernicus/releases/download/release-1.3.1-2/Kopernicus-1.3.1-2.zip",
                    FileName = "Kopernicus-1.3.1-2.zip",
                    State = "Uninstalled"
                });

                modPack.Add(new Mod()
                {
                    ModPackName = "Core",
                    ModName = "GPP",
                    DownloadAddress = "https://github.com/Galileo88/Galileos-Planet-Pack/releases/download/1.5.88/Galileos.Planet.Pack.1.5.88.zip",
                    FileName = "Galileos.Planet.Pack.1.5.88.zip",
                    State = "Uninstalled"
                });

                modPack.Add(new Mod()
                {
                    ModPackName = "Core",
                    ModName = "GPP_Textures",
                    DownloadAddress = "https://github.com/Galileo88/Galileos-Planet-Pack/releases/download/3.0.0/GPP_Textures-3.0.0.zip",
                    FileName = "GPP_Textures-3.0.0.zip",
                    State = "Uninstalled"
                });
            }
            
            // Clouds, Auroae, and Shadows
            // 1.) Choose cloud textures
            // 2.) Download and install EVE


            //modPack.Add(new Mod()
            //{
            //    ModName = ""
            //})

            // Scatterer
            // 1.) Download and install Scatterer

            // PlanetShine
            // 1.) Download and install PlanetShine

            // DistantObjectEnhancement
            // 1.) Download and install DistantObjectEnhancement
            
        }
        
        public async void DownloadFiles()
        {
            for (int i = 0; i < modPack.Count; i++)
            {
                string downloadAddress = modPack[i].DownloadAddress;
                string fileName = modPack[i].FileName;

                string gppDir = ".\\GPPInstaller";
                string downloadDest = gppDir + "\\" + fileName;

                if (!File.Exists(downloadDest))
                {
                    WebClient webclient = new WebClient();
                    Uri uri = new Uri(downloadAddress);

                    webclient.DownloadFileCompleted += DownloadFileComplete(modPack[i], form1);
                    webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
                    await Task.Factory.StartNew(() => webclient.DownloadFileAsync(uri, downloadDest));
                }
            }
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

                if (modIndex >= modPack.Count)
                {
                    form1.ProgressLabelUpdate("All Downloads complete.");
                }
            };
            return new AsyncCompletedEventHandler(action);
        }

        public async void DownloadAndInstall(string modPackName)
        {
            int debugCounter = 0;

            
            for (int i = 0; i < modPack.Count; i++)
            {

                string modName = modPack[i].ModName;
                string downloadAddress = modPack[i].DownloadAddress;
                string fileName = modPack[i].FileName;

                string gppDir = ".\\GPPInstaller";
                string downloadDest = gppDir + "\\" + fileName;

                //NOTE: downloadfileasync appears to be timing out for the GPP download 
                if (!File.Exists(downloadDest))
                {
                    //DownloadFileBackground(downloadAddress, downloadDest);
                }
                
                // STEP 1
                //form1.ProgressBarStep();
                debugCounter++;
                Debug.WriteLine("Step: " + debugCounter.ToString());
                //StepDescription(modPackName, modName, debugCounter);

                int fileNameLength = fileName.Length;
                string dirName = fileName.Remove((fileNameLength - 4), 4);
                string destDir = gppDir + "\\" + dirName;

                DirectoryInfo destDirInfo = new DirectoryInfo(destDir);

                // NOTE: overwritting data for now, might want to handle
                // this differently.
                //if (destDirInfo.Exists)
                //{
                //    Directory.Delete(destDir, true);
                //}

                //string zipFile = gppDir + "\\" + fileName;
                //Directory.CreateDirectory(gppDir + "\\" + dirName);
                //await Task.Factory.StartNew(() => ZipFile.ExtractToDirectory(zipFile, destDir));

                string zipFile = gppDir + "\\" + fileName;
                
                if (!destDirInfo.Exists)
                {
                    Directory.CreateDirectory(gppDir + "\\" + dirName);
                    await Task.Factory.StartNew(() => ZipFile.ExtractToDirectory(zipFile, destDir));
                }

                // STEP 2
                debugCounter++;
                Debug.WriteLine("Step: " + debugCounter.ToString());
                //form1.ProgressBarStep();
                //StepDescription(modPackName, modName, debugCounter);


                //TODO: Might want to delete the zip file after exctraction.

                string sourceDirName;
                string destDirName;
                if (modName == "GPP_Textures")
                {
                    sourceDirName = gppDir + "\\" + dirName + "\\GameData\\GPP";
                    destDirName = ".\\GameData\\GPP";
                }
                else
                {
                    sourceDirName = gppDir + "\\" + dirName + "\\GameData";
                    destDirName = ".\\GameData";
                }

                await Task.Factory.StartNew(() => DirectoryCopy(sourceDirName, destDirName, true));
                // STEP 3
                debugCounter++;
                Debug.WriteLine("Step: " + debugCounter.ToString());
                //form1.ProgressBarStep();
                //StepDescription(modPackName, modName, debugCounter);
            }
            Debug.WriteLine("Install finished");

            // NOTE: doing this here to refreash modPack
            // in case there are multiple mod pack downloads.
            modPack = new List<Mod>();
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


        public void Uninstall(string modPackName)
        {
            if (modPackName == "Core")
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
            }

            Debug.WriteLine("Uninstallation complete");
            //form1.ProgressLabelUpdate("Changes successfully applied!");

            // TODO: VisualEnhacements
        }

        public string[] CurrentInstalledModPacks()
        {
            string[] result = new string[2];

            int coreModCount = 0, visualEnhacementModCount = 0;


            string gameDataPath = ".\\GameData";
            DirectoryInfo gameDataDir = new DirectoryInfo(gameDataPath);

            DirectoryInfo[] gameDataSubDirs = gameDataDir.GetDirectories();
            foreach (DirectoryInfo d in gameDataSubDirs)
            {
                if (d.Name != "Squad")
                {
                    if (d.Name == "Kopernicus" || d.Name == "GPP") coreModCount++;
                    // implement for visual enhacements
                }
            }

            if (coreModCount == 2)
            {
                result[0] = "Core";
            }
            else result[0] = "";

            return result;
        }

        //private void StepDescription(string modPackName, string modName, int Step)
        //{
        //    if (modPackName == "Core")
        //    {
        //        if (Step == 0 || Step == 3 || Step == 6)
        //        {
        //            form1.ProgressLabelUpdate("Downloading " + modName + "...");
        //        }

        //        if (Step == 1 || Step == 4 || Step == 7)
        //        {
        //            form1.ProgressLabelUpdate("Extracting " + modName + "...");
        //        }

        //        if (Step == 2 || Step == 5 || Step == 8)
        //        {
        //            form1.ProgressLabelUpdate("Copying " + modName + " into GameData...");
        //        }

        //        if (Step == 9)
        //        {
        //            form1.ProgressLabelUpdate("Changes successfully applied!");
        //        }
        //    }

        //}
    }
}
