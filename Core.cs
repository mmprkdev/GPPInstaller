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

// TODO: Use xpath to grab the download links, insted of using
// static links defined in GlobalInfo. 

// TODO: reorganize the code base. Group similar functions
// out into class files.

// TODO: Final bug fixing before release.

namespace GPPInstaller
{
    class Core
    {
        private int numOfFilesInDir = 0;

        private int modIndex = 0;

        private BackgroundWorker workerExtract = new BackgroundWorker();
        private BackgroundWorker workerInstall = new BackgroundWorker();
        
        private Form1 form1;

        public List<Mod> modList = new List<Mod>();

        private WebClient webclient = new WebClient();

        public Core(Form1 form1)
        {
            this.form1 = form1;

            webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webclient_DownloadProgressChanged);
            webclient.DownloadFileCompleted += new AsyncCompletedEventHandler(webclient_DownloadFileCompleted);

            workerExtract.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerExtract_RunWorkerCompleted);
            workerExtract.DoWork += new DoWorkEventHandler(workerExtract_DoWork);
            workerExtract.WorkerSupportsCancellation = true;

            workerInstall.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerInstall_RunWorkerCompleted);
            workerInstall.DoWork += new DoWorkEventHandler(workerInstall_DoWork);
            workerInstall.WorkerSupportsCancellation = true;
        }

        public void InitModList()
        {
            modList.Add(new Mod()
            {
                ModType = "Core",
                ModName = "Kopernicus",
                DownloadAddress = GlobalInfo.kopernicusLink,
                ArchiveFileName = "Kopernicus-1.3.1-2.zip",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "Kopernicus-1.3.1-2",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "Kopernicus",
                InstallSourcePath = @".\GPPInstaller\Kopernicus-1.3.1-2\GameData\",
                InstallDestPath = @".\GameData",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModType = "Core",
                ModName = "GPP",
                DownloadAddress = GlobalInfo.gppLink,
                ArchiveFileName = "Galileos.Planet.Pack.1.5.88.zip",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "Galileos.Planet.Pack.1.5.88",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "GPP",
                InstallSourcePath = @".\GPPInstaller\Galileos.Planet.Pack.1.5.88\GameData",
                InstallDestPath = @".\GameData",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModType = "Core",
                ModName = "GPP_Textures",
                DownloadAddress = GlobalInfo.gppTexturesLink,
                ArchiveFileName = "GPP_Textures-3.0.0.zip",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "GPP_Textures-3.0.0",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "GPP_Textures",
                InstallSourcePath = @".\GPPInstaller\GPP_Textures-3.0.0\GameData\GPP",
                InstallDestPath = @".\GameData\GPP",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModType = "Visuals",
                ModName = "EVE",
                DownloadAddress = GlobalInfo.eveLink,
                ArchiveFileName = "EnvironmentalVisualEnhancements-1.2.2.1.zip",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "EnvironmentalVisualEnhancements-1.2.2.1",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "EnvironmentalVisualEnhancements",
                InstallSourcePath = @".\GPPInstaller\EnvironmentalVisualEnhancements-1.2.2.1\GameData",
                InstallDestPath = @".\GameData",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModType = "Visuals",
                ModName = "Scatterer",
                DownloadAddress = GlobalInfo.scattererLink,
                ArchiveFileName = "scatterer-0.0320b.zip",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "scatterer-0.0320b",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "scatterer",
                InstallSourcePath = @".\GPPInstaller\scatterer-0.0320b\GameData",
                InstallDestPath = @".\GameData",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModType = "Visuals",
                ModName = "DistantObjectEnhancement",
                DownloadAddress = GlobalInfo.doeLink,
                ArchiveFileName = "DistantObject_1.9.1.zip",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "DistantObject_1.9.1",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "DistantObject",
                InstallSourcePath = @".\GPPInstaller\DistantObject_1.9.1\GameData",
                InstallDestPath = @".\GameData",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModType = "Clouds",
                ModName = "CloudsLowRes",
                DownloadAddress = "",
                ArchiveFileName = "",
                ArchiveFilePath = "",
                ExtractedDirName = "GPP_Clouds",
                ExtractedPath = @".\GPPInstaller\Galileos.Planet.Pack.1.5.88\Optional Mods\GPP_Clouds\Low-res Clouds_GameData inside\GameData\GPP",
                InstallDirName = "GPP_Clouds",
                InstallSourcePath = @".\GPPInstaller\Galileos.Planet.Pack.1.5.88\Optional Mods\GPP_Clouds\Low-res Clouds_GameData inside\GameData\GPP",
                InstallDestPath = @".\GameData\GPP",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModType = "Clouds",
                ModName = "CloudsHighRes",
                DownloadAddress = "",
                ArchiveFileName = "",
                ArchiveFilePath = "",
                ExtractedDirName = "GPP_Clouds",
                ExtractedPath = @".\GPPInstaller\Galileos.Planet.Pack.1.5.88\Optional Mods\GPP_Clouds\High-res Clouds_GameData inside\GameData\GPP",
                InstallDirName = "GPP_Clouds",
                InstallSourcePath = @".\GPPInstaller\Galileos.Planet.Pack.1.5.88\Optional Mods\GPP_Clouds\High-res Clouds_GameData inside\GameData\GPP",
                InstallDestPath = @".\GameData\GPP",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            //modList.Add(new Mod()
            //{
            //    ModPackName = "Utility",
            //    ModName = "KerbalEngineer",
            //    DownloadAddress = "https://github.com/CYBUTEK/KerbalEngineer/releases/download/1.1.3.0/KerbalEngineer-1.1.3.0.zip",
            //    ArchiveFileName = "KerbalEngineer-1.1.3.0.zip",
            //    State_Downloaded = false,
            //    State_Extracted = false,
            //    State_Installed = false,
            //    ActionToTake = ""
            //});

            RefreshModState();
        }

        public void RefreshModState()
        {
            // downloaded
            foreach (Mod mod in modList)
            {
                if (mod.ModName == "CloudsLowRes" &&
                    modList[GlobalInfo.GPPIndex].State_Downloaded == true)
                {
                    mod.State_Downloaded = true;       
                }
                else mod.State_Downloaded = false;

                if (mod.ModName == "CloudsHighRes" &&
                    modList[GlobalInfo.GPPIndex].State_Downloaded == true)
                {
                    mod.State_Downloaded = true;
                }
                else mod.State_Downloaded = false;

                if (File.Exists(mod.ArchiveFilePath + @"\" + mod.ArchiveFileName))
                {
                    mod.State_Downloaded = true;
                }
                else
                {
                    mod.State_Downloaded = false;
                }
            }

            // extracted
            foreach (Mod mod in modList)
            {
                if (Directory.Exists(mod.ExtractedPath + @"\" + mod.ExtractedDirName))
                {
                    mod.State_Extracted = true;
                }
                else
                {
                    mod.State_Extracted = false;
                }
            }

            // installed
            foreach (Mod mod in modList)
            {
                if (mod.ModType == "Clouds")
                {
                    if (mod.ModName == "CloudsLowRes")
                    {
                        if (File.Exists(@".\GameData\GPP\GPP_Clouds\Configs\GPPClouds_LowRes.cfg"))
                        {
                            mod.State_Installed = true;
                        }
                        else mod.State_Installed = false;
                    }

                    if (mod.ModName == "CloudsHighRes")
                    {
                        if (File.Exists(@".\GameData\GPP\GPP_Clouds\Configs\GPPClouds_HighRes.cfg"))
                        {
                            mod.State_Installed = true;
                        }
                        else mod.State_Installed = false;
                    }
                }
                else
                {
                    if (Directory.Exists(mod.InstallDestPath + @"\" + mod.InstallDirName))
                    {
                        mod.State_Installed = true;
                    }
                    else
                    {
                        mod.State_Installed = false;
                    }
                }
            }
        }

        public void SetCheckBoxes(
            CheckBox coreCheckBox,
            CheckBox visualsCheckBox,
            CheckBox cloudsLowResCheckBox,
            CheckBox cloudsHighResCheckBox)
        {
            foreach (Mod mod in modList)
            {
                if (mod.ModType == "Core")
                {
                    if (mod.State_Installed == true)
                    {
                        coreCheckBox.Checked = true;
                    }
                    else coreCheckBox.Checked = false;
                }

                if (mod.ModType == "Visuals") 
                {
                    if (mod.State_Installed == true)
                    {
                        visualsCheckBox.Checked = true;
                    }
                    else
                    {
                        visualsCheckBox.Checked = false;
                        cloudsHighResCheckBox.Checked = false;
                        cloudsLowResCheckBox.Checked = false;
                    } 
                }

                if (mod.ModName == "CloudsLowRes")
                {
                    if (mod.State_Installed == true)
                    {
                        cloudsLowResCheckBox.Checked = true;
                    }
                    else cloudsLowResCheckBox.Checked = false;
                }
                
                if (mod.ModName == "CloudsHighRes")
                {
                    if (mod.State_Installed == true)
                    {
                        cloudsHighResCheckBox.Checked = true;
                    }
                    else cloudsHighResCheckBox.Checked = false;
                }
            }
        }


        public void ProcessActionToTake(
            CheckBox coreCheckBox,
            CheckBox visualsCheckBox,
            CheckBox cloudsLowResCheckBox,
            CheckBox cloudsHighResCheckBox)
        {
            foreach (Mod mod in modList)
            { 
                if (mod.State_Installed == false)
                {
                    if (mod.ModType == "Core" &&
                    coreCheckBox.Checked == true)
                    {
                        mod.ActionToTake = "Install";
                    }

                    if (mod.ModType == "Visuals" &&
                             visualsCheckBox.Checked)
                    {
                        mod.ActionToTake = "Install";
                    }

                    if (mod.ModName == "CloudsLowRes" &&
                        cloudsLowResCheckBox.Checked)
                    {
                        mod.ActionToTake = "Install";
                    }

                    if (mod.ModName == "CloudsHighRes" &&
                        cloudsHighResCheckBox.Checked)
                    {
                        mod.ActionToTake = "Install";
                    }
                }

                if (mod.State_Installed == true)
                {
                    if (mod.ModType == "Core" &&
                        coreCheckBox.Checked == false)
                    {
                        mod.ActionToTake = "Uninstall";
                    }

                    if (mod.ModType == "Visuals" &&
                        visualsCheckBox.Checked == false)
                    {
                        mod.ActionToTake = "Uninstall";
                    }

                    if (mod.ModName == "CloudsLowRes" &&
                        cloudsLowResCheckBox.Checked == false)
                    {
                        mod.ActionToTake = "Uninstall";
                    }

                    if (mod.ModName == "CloudsHighRes" &&
                        cloudsHighResCheckBox.Checked == false)
                    {
                        mod.ActionToTake = "Uninstall";
                    }
                }
            }
        }

        public void CancelProcess()
        {
            modIndex = 0;
            form1.RemoveCancelButton();
            form1.EnableExitButton();
            form1.RemoveProgressBar();
            form1.EnableApplyButton();
            form1.EnableCheckBoxes();
            RefreshModState();
            form1.RefreshCheckBoxes();
        }

        public void ResetActionsToTake()
        {
            foreach (Mod mod in modList)
            {
                mod.ActionToTake = "";
            }
        }

        public void DownloadMod()
        {
            if (modIndex < modList.Count)
            {
                if (modList[modIndex].State_Extracted == false &&
                modList[modIndex].State_Downloaded == false &&
                modList[modIndex].ActionToTake == "Install" &&
                modList[modIndex].DownloadAddress != "")
                {
                    string downloadAddress = modList[modIndex].DownloadAddress;
                    string fileName = modList[modIndex].ArchiveFileName;
                    string downloadDest = @".\GPPInstaller\" + fileName;

                    Uri uri = new Uri(downloadAddress);

                    if (!GlobalInfo.IsConnectedToInternet())
                    {
                        ErrorNoInternetConnection();
                        webclient.CancelAsync();
                        CancelProcess();

                        return;
                    }

                    webclient.DownloadFileAsync(uri, downloadDest);
                }
                else
                {
                    modIndex++;

                    DownloadMod();
                }
            }
            else
            {
                form1.ProgressLabelUpdate("Extracting files...");
                modIndex = 0;
                // TODO: add this to the release version
                //DeleteAllZips(@".\GPPInstaller");
                ExtractMod();
            }
        }

        private void webclient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            string websiteName = WebsiteNamePop(modList[modIndex].DownloadAddress);
            string output = "Downloading: " + modList[modIndex].ArchiveFileName + " from " + websiteName + " " + e.ProgressPercentage + "% complete...";
            form1.ProgressLabelUpdate(output);
        }

        private void webclient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                form1.ProgressLabelUpdate("Installation canceled.");
                form1.DisplayRedCheck();
                DeleteAllZips(@".\GPPInstaller");

                CancelProcess();

                return;
            }

            modList[modIndex].State_Downloaded = true;
            form1.ProgressBar1Step();
            modIndex++;
            DownloadMod();
        }

        public void WebClientCancel()
        {
            webclient.CancelAsync();
        }

        private string WebsiteNamePop(string url)
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

        // NOTE: Need to change the archive file name for Cloud mods
        // to empty string
        private void ExtractMod()
        {
            if (modIndex < modList.Count)
            {
                if (modList[modIndex].State_Extracted == false &&
                    modList[modIndex].ActionToTake == "Install" &&
                    modList[modIndex].State_Downloaded == true &&
                    modList[modIndex].ArchiveFileName != "")
                {
                    Debug.WriteLine(modList[modIndex].ModName);

                    string fileName = modList[modIndex].ArchiveFileName;
                    int fileNameLength = fileName.Length;
                    string dirName = fileName.Remove((fileNameLength - 4), 4);
                    string destDir = @".\GPPInstaller\" + dirName;

                    //string destDir = modList[modIndex].ExtractedPath + modList[modIndex].ExtractedDirName;

                    DirectoryInfo destDirInfo = new DirectoryInfo(destDir);

                    string zipFile = @".\GPPInstaller\" + fileName;
                    
                    if (!destDirInfo.Exists)
                    {
                        Directory.CreateDirectory(destDir);

                        if (File.Exists(zipFile))
                        {
                            // NOTE: It appears that when we enter the worker for
                            // a second time (re-use the previous worker) the values
                            // of variables are re-used. Might need to create a new worker
                            // for each new loop through, or somehow flush all data from the
                            // worker.
                            // NOTE: The solution to this problem was to pass new arguments to the
                            // background worker DoWork event, insted of modifying existing variables.

                            var args = new Tuple<string, string>(zipFile, destDir);

                            try
                            {
                                workerExtract.RunWorkerAsync(args);
                            }
                            catch (InvalidDataException exception)
                            {
                                ErrorInvalidData();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    modIndex++;
                    ExtractMod();
                }
            }
            else
            {
                modIndex = 0;
                form1.ProgressLabelUpdate("Copying to GameData...");
                InstallMod();
            }
        }

        // NOTE: When there are 2 processes, the first process does not
        // complete fully. It get's past "Clouds"

        private void workerExtract_DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (Tuple<string, string>)e.Argument;

            using (var archive = ZipFile.OpenRead(args.Item1))
            {
                foreach (var entry in archive.Entries)
                {
                    if (workerExtract.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    if (entry.IsFolder())
                    {
                        Directory.CreateDirectory(Path.Combine(args.Item2, entry.FullName));
                    }
                    else
                    {
                        entry.ExtractToFile(Path.Combine(args.Item2, entry.FullName));
                    }
                }
            }
            if (e.Cancel) Directory.Delete(args.Item2, true);
        }

        private void workerExtract_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                modIndex = 0;

                form1.ProgressLabelUpdate("Installation canceled.");
                form1.DisplayRedCheck();
                form1.EnableCheckBoxes();
                form1.EnableApplyButton();
                form1.EnableExitButton();
                form1.RemoveCancelButton();
                form1.RemoveProgressBar();

                RefreshModState();
                form1.RefreshCheckBoxes();

                return;
            }

            modList[modIndex].State_Extracted = true;
            form1.ProgressBar1Step();
            modIndex++;
            ExtractMod();
        }

        public void ExtractCancel()
        {
            workerExtract.CancelAsync();
        }

        private void InstallMod()
        {
            if (modIndex < modList.Count)
            {
                if (modList[modIndex].State_Installed == false &&
                    modList[modIndex].ActionToTake == "Install")
                {
                    string sourceDirName = modList[modIndex].InstallSourcePath;
                    string destDirName = modList[modIndex].InstallDestPath;

                    if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);

                    var args = new Tuple<string, string, bool>(sourceDirName, destDirName, true);
                    workerInstall.RunWorkerAsync(args);
                }
                else
                {
                    modIndex++;
                    InstallMod();
                }
            }
            else
            {
                modIndex = 0;
                ResetActionsToTake();

                form1.RemoveProgressBar();
                form1.RemoveCancelButton();
                form1.DisplayGreenCheck();
                form1.EnableExitButton();
                form1.EnableCheckBoxes();
                form1.EnableApplyButton();
                form1.ProgressLabelUpdate("All changes applied successfully.");
            }

        }

        // NOTE: Installation cancelation is set up to work, but it the CancelationPending
        // property does not get set for some reason when CancelAsync is called. For right now
        // I am just going to leave it.
        private void workerInstall_DoWork(object sender, DoWorkEventArgs e)
        {

            if (workerInstall.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            var args = (Tuple<string, string, bool>)e.Argument;

            string sourceDirName = args.Item1;
            string destDirName = args.Item2;
            bool copySubDirs = args.Item3;

            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist: " + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destDirName))
            {
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

            if (e.Cancel) Directory.Delete(destDirName);
        }

        private void workerInstall_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                modIndex = 0;

                form1.ProgressLabelUpdate("Installation canceled.");
                form1.DisplayRedCheck();
                form1.EnableCheckBoxes();
                form1.EnableApplyButton();
                form1.EnableExitButton();
                form1.RemoveCancelButton();
                form1.RemoveProgressBar();

                RefreshModState();
                form1.RefreshCheckBoxes();
                
                return;
            }

            modList[modIndex].State_Installed = true;
            form1.ProgressBar1Step();
            modIndex++;
            InstallMod();
        }

        public void InstallCancel()
        {
            workerInstall.CancelAsync();
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
                return "Error: could not find exe.";

            }

            string result = "64";
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

        public void UninstallMod()
        {
            if (modList[GlobalInfo.kopericusIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(".\\GameData\\Kopernicus")) Directory.Delete(".\\GameData\\Kopernicus", true);
                if (Directory.Exists(".\\GameData\\ModularFlightIntegrator")) Directory.Delete(".\\GameData\\ModularFlightIntegrator", true);
                File.Delete(".\\GameData\\ModuleManager.2.8.1.dll");
                File.Delete(".\\GameData\\ModuleManagerLicense.md");
                File.Delete(".\\GameData\\ModuleManager.ConfigCache");
                File.Delete(".\\GameData\\ModuleManager.ConfigSHA");
                File.Delete(".\\GameData\\ModuleManager.Physics");
                File.Delete(".\\GameData\\ModuleManager.TechTree");

                modList[GlobalInfo.kopericusIndex].State_Installed = false;
            }
            
            if (modList[GlobalInfo.GPPIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(".\\GameData\\GPP")) Directory.Delete(".\\GameData\\GPP", true);

                modList[GlobalInfo.GPPIndex].State_Installed = false;
            }

            if (modList[GlobalInfo.EVEIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\EnvironmentalVisualEnhancements")) Directory.Delete(@".\GameData\EnvironmentalVisualEnhancements", true);

                modList[GlobalInfo.EVEIndex].State_Installed = false;
            }

            if (modList[GlobalInfo.scattererIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\scatterer")) Directory.Delete(@".\GameData\scatterer", true);

                modList[GlobalInfo.scattererIndex].State_Installed = false;
            }

            if (modList[GlobalInfo.doeIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\DistantObject")) Directory.Delete(@".\GameData\DistantObject", true);

                modList[GlobalInfo.doeIndex].State_Installed = false;
            }

            if (modList[GlobalInfo.cloudsLowResIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);

                modList[GlobalInfo.cloudsLowResIndex].State_Installed = false;
            }

            if (modList[GlobalInfo.cloudsHighResIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);

                modList[GlobalInfo.cloudsHighResIndex].State_Installed = false;
            }
        }

        public int NumberOfSteps()
        {
            int result = 0;

            foreach (Mod mod in modList)
            {
                // Downloads
                if (mod.State_Downloaded == false &&
                mod.ActionToTake == "Install" &&
                mod.DownloadAddress != "")
                {
                    result++;
                }

                // Extractions
                if (mod.State_Extracted == false &&
                    mod.ActionToTake == "Install" &&
                    mod.ArchiveFileName != "")
                {
                    result++;
                }

                // Installations
                if (mod.State_Installed == false &&
                    mod.ActionToTake == "Install")
                {
                    if (mod.ModType != "Clouds")
                    {
                        result++;
                    }
                    else
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        private void DeleteAllZips(string dirPath)
        {
            DirectoryInfo dir = new DirectoryInfo(dirPath);

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.Extension == ".zip") File.Delete(file.FullName);
            }
        }

        private void NumberOfFilesInDir(string dirPath)
        {
            DirectoryInfo dir = new DirectoryInfo(dirPath);

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                numOfFilesInDir++;
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            foreach (DirectoryInfo subDir in dirs)
            {
                NumberOfFilesInDir(subDir.FullName);
            }
        }

        public void ErrorNoInternetConnection()
        {
            form1.ProgressLabelUpdate("Error: No internet connection was detected. An internet connection is\n" +
                " required to download the mod files.");
            form1.DisplayYellowWarning();
            form1.RemoveProgressBar();
            form1.RemoveCancelButton();
        }

        public void ErrorInvalidData()
        {
            form1.ProgressLabelUpdate("Error: Download was incomplete.");
            form1.DisplayYellowWarning();
            form1.RemoveProgressBar();
            form1.RemoveCancelButton();
        }
    }

    class Mod
    {
        // States: "Downloaded" == true : The archive file is present in .\GPPInstaller
        //         "Extracted" == true  : An extracted dir exsists in .\GPPInstaller
        //         "Installed" == true  : All required dirs and files are present inside of .\GameData
        //         ""                   : Initial default state 
        // InstallTheMod:   true : install the mod
        //                  false: uninstall the mod
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
    }

    static class ZipArchiEntryExtensions
    {
        public static bool IsFolder(this ZipArchiveEntry entry)
        {
            return entry.FullName.EndsWith("/");
        }
    }
}
