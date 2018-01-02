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

// TODO: Make the code for updating the checkboxes and 
// other stuff more dynamic (use a loop).

// TODO: Maybe add a few extra "utility" mods
// (Kerbal Engineer, Kerbal Alarm Clock, etc)

// TODO: Give option to delete save files when uninstalling core mods or
// when installing a new version of GPP. 

// TODO: Write a bash script that adds all
// files in the github_repo\GPPInstaller dir automatically.

// TODO: Get installation cancelation working

namespace GPPInstaller
{
    class Utility
    {
        private int numOfFilesInDir = 0;

        private int modIndex = 0;

        private BackgroundWorker workerExtract = new BackgroundWorker();
        private BackgroundWorker workerInstall = new BackgroundWorker();
        
        private Form1 form1;

        public List<Mod> modList = new List<Mod>();

        private WebClient webclient = new WebClient();

        public Utility(Form1 form1)
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
                ModPackName = "Clouds",
                ModName = "CloudsLowRes",
                DownloadAddress = "",
                ArchiveFileName = "",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModPackName = "Clouds",
                ModName = "CloudsHighRes",
                DownloadAddress = "",
                ArchiveFileName = "",
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
            // TODO: Maybe shorten this method by using a for loop

            // NOTE: Check if downloaded into .\GPPInstaller
            if (File.Exists(GlobalInfo.kopernicusZipPath))
            {
                modList[GlobalInfo.kopericusIndex].State_Downloaded = true;
            }
            else modList[GlobalInfo.kopericusIndex].State_Downloaded = false;

            if (File.Exists(GlobalInfo.gppZipPath))
            {
                modList[GlobalInfo.GPPIndex].State_Downloaded = true;
            }
            else modList[GlobalInfo.GPPIndex].State_Downloaded = false;

            if (File.Exists(GlobalInfo.gppTexturesZipPath))
            {
                modList[GlobalInfo.GPPTexturesIndex].State_Downloaded = true;
            }
            else modList[GlobalInfo.GPPTexturesIndex].State_Downloaded = false;

            if (File.Exists(GlobalInfo.eveZipPath))
            {
                modList[GlobalInfo.EVEIndex].State_Downloaded = true;
            }
            else modList[GlobalInfo.EVEIndex].State_Downloaded = false;

            if (File.Exists(GlobalInfo.scattererZipPath))
            {
                modList[GlobalInfo.scattererIndex].State_Downloaded = true;
            }
            else modList[GlobalInfo.scattererIndex].State_Downloaded = false;

            if (File.Exists(GlobalInfo.doeZipPath))
            {
                modList[GlobalInfo.doeIndex].State_Downloaded = true;
            }
            else modList[GlobalInfo.doeIndex].State_Downloaded = false;

            if (modList[GlobalInfo.GPPIndex].State_Downloaded == true)
            {
                modList[GlobalInfo.cloudsLowResIndex].State_Downloaded = true;
                modList[GlobalInfo.cloudsHighResIndex].State_Downloaded = true;
            }
            else
            {
                modList[GlobalInfo.cloudsLowResIndex].State_Downloaded = false;
                modList[GlobalInfo.cloudsHighResIndex].State_Downloaded = false;
            }

            // NOTE: Check if extracted into .\GPPInstaller
            if (Directory.Exists(GlobalInfo.kopernicusExtractPath))
            {
                modList[GlobalInfo.kopericusIndex].State_Extracted = true;
            }
            else modList[GlobalInfo.kopericusIndex].State_Extracted = false;

            if (Directory.Exists(GlobalInfo.gppExtractPath))
            {
                modList[GlobalInfo.GPPIndex].State_Extracted = true;
            }
            else modList[GlobalInfo.GPPIndex].State_Extracted = false;

            if (Directory.Exists(GlobalInfo.gppTexturesExtractPath))
            {
                modList[GlobalInfo.GPPTexturesIndex].State_Extracted = true;
            }
            else modList[GlobalInfo.GPPTexturesIndex].State_Extracted = false;

            if (Directory.Exists(GlobalInfo.eveExtractPath))
            {
                modList[GlobalInfo.EVEIndex].State_Extracted = true;
            }
            else modList[GlobalInfo.EVEIndex].State_Extracted = false;

            if (Directory.Exists(GlobalInfo.scattererExtractPath))
            {
                modList[GlobalInfo.scattererIndex].State_Extracted = true;
            }
            else modList[GlobalInfo.scattererIndex].State_Extracted = false;

            if (Directory.Exists(GlobalInfo.doeExtractPath))
            {
                modList[GlobalInfo.doeIndex].State_Extracted = true;
            }
            else modList[GlobalInfo.doeIndex].State_Extracted = false;

            if (modList[GlobalInfo.GPPIndex].State_Extracted == true)
            {
                modList[GlobalInfo.cloudsLowResIndex].State_Extracted = true;
                modList[GlobalInfo.cloudsHighResIndex].State_Extracted = true;
            }
            else
            {
                modList[GlobalInfo.cloudsLowResIndex].State_Extracted = false;
                modList[GlobalInfo.cloudsHighResIndex].State_Extracted = false;
            }

            // NOTE: Check if installed into .\GameData
            if (Directory.Exists(GlobalInfo.kopernicusInstallPath) &&
                Directory.Exists(GlobalInfo.modularFIInstallPath) &&
                File.Exists(GlobalInfo.modManagerInstallPath))
            {
                modList[GlobalInfo.kopericusIndex].State_Installed = true;
            }
            else modList[GlobalInfo.kopericusIndex].State_Installed = false;

            if (Directory.Exists(GlobalInfo.gppInstallPath))
            {
                modList[GlobalInfo.GPPIndex].State_Installed = true;
            }
            else modList[GlobalInfo.GPPIndex].State_Installed = false;

            if (Directory.Exists(GlobalInfo.gppTexturesInstallPath))
            {
                modList[GlobalInfo.GPPTexturesIndex].State_Installed = true;
            }
            else modList[GlobalInfo.GPPTexturesIndex].State_Installed = false;

            if (Directory.Exists(GlobalInfo.eveInstallPath))
            {
                modList[GlobalInfo.EVEIndex].State_Installed = true;
            }
            else modList[GlobalInfo.EVEIndex].State_Installed = false;

            if (Directory.Exists(GlobalInfo.scattererInstallPath))
            {
                modList[GlobalInfo.scattererIndex].State_Installed = true;
            }
            else modList[GlobalInfo.scattererIndex].State_Installed = false;

            if (Directory.Exists(GlobalInfo.doeInstallPath))
            {
                modList[GlobalInfo.doeIndex].State_Installed = true;
            }
            else modList[GlobalInfo.doeIndex].State_Installed = false;

            if (File.Exists(GlobalInfo.cloudsLowResInstallPath))
            {
                modList[GlobalInfo.cloudsLowResIndex].State_Installed = true;
            }
            else modList[GlobalInfo.cloudsLowResIndex].State_Installed = false;

            if (File.Exists(GlobalInfo.cloudsHighResInstallPath))
            {
                modList[GlobalInfo.cloudsHighResIndex].State_Installed = true;
            }
            else modList[GlobalInfo.cloudsHighResIndex].State_Installed = false;
        }

        public void SetCheckBoxes(
            CheckBox coreCheckBox,
            CheckBox visualsCheckBox,
            CheckBox cloudsLowResCheckBox,
            CheckBox cloudsHighResCheckBox)
        {
            if (modList[GlobalInfo.kopericusIndex].State_Installed == true &&
                modList[GlobalInfo.GPPIndex].State_Installed == true &&
                modList[GlobalInfo.GPPTexturesIndex].State_Installed == true)
            {
                coreCheckBox.Checked = true;
            }
            else coreCheckBox.Checked = false;

            if (modList[GlobalInfo.EVEIndex].State_Installed == true &&
                modList[GlobalInfo.scattererIndex].State_Installed == true &&
                modList[GlobalInfo.doeIndex].State_Installed == true)
            {
                visualsCheckBox.Checked = true;
            }
            else visualsCheckBox.Checked = false;

            if (modList[GlobalInfo.cloudsLowResIndex].State_Installed == true)
            {
                cloudsLowResCheckBox.Checked = true;
            }
            else cloudsLowResCheckBox.Checked = false;

            if (modList[GlobalInfo.cloudsHighResIndex].State_Installed == true)
            {
                cloudsHighResCheckBox.Checked = true;
            }
            else cloudsHighResCheckBox.Checked = false;
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
                    if (mod.ModPackName == "Core" &&
                    coreCheckBox.Checked == true)
                    {
                        mod.ActionToTake = "Install";
                    }

                    if (mod.ModPackName == "Visuals" &&
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
                    if (mod.ModPackName == "Core" &&
                        coreCheckBox.Checked == false)
                    {
                        mod.ActionToTake = "Uninstall";
                    }

                    if (mod.ModPackName == "Visuals" &&
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

                    // TODO: test for the Invalid data exception, disconnect internet
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

        private void ExtractMod()
        {
            if (modIndex < modList.Count)
            {
                if (modList[modIndex].State_Extracted == false &&
                    modList[modIndex].ActionToTake == "Install" &&
                    modList[modIndex].State_Downloaded == true &&
                    modList[modIndex].ArchiveFileName != "")
                {


                    string fileName = modList[modIndex].ArchiveFileName;
                    int fileNameLength = fileName.Length;
                    string dirName = fileName.Remove((fileNameLength - 4), 4);
                    string destDir = @".\GPPInstaller\" + dirName;

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
                form1.ProgressLabelUpdate("Copying mods to GameData...");
                InstallMod();
            }
        }

        private void workerExtract_DoWork(object sender, DoWorkEventArgs e)
        {
            // NOTE: The args Tuple resurfaces here
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
                    if (modList[modIndex].ModPackName != "Clouds")
                    {
                        string sourceDirName;
                        string destDirName;

                        string fileName = modList[modIndex].ArchiveFileName;
                        int fileNameLength = fileName.Length;
                        string dirName = fileName.Remove((fileNameLength - 4), 4);
                        if (modList[modIndex].ModName == "GPP_Textures")
                        {
                            sourceDirName = @".\GPPInstaller\" + dirName + @"\GameData\GPP";
                            destDirName = @".\GameData\GPP";

                            var args = new Tuple<string, string, bool>(sourceDirName, destDirName, true);
                            workerInstall.RunWorkerAsync(args);
                        }
                        else
                        {
                            sourceDirName = @".\GPPInstaller\" + dirName + @"\GameData";
                            destDirName = @".\GameData";

                            var args = new Tuple<string, string, bool>(sourceDirName, destDirName, true);
                            workerInstall.RunWorkerAsync(args);
                        }
                    }
                    else
                    {
                        string cloudDirSource;
                        string cloudDirDest;
                        if (modList[modIndex].ModName == "CloudsLowRes")
                        {
                            if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);

                            cloudDirSource = @".\GPPInstaller\Galileos.Planet.Pack.1.5.88\Optional Mods\GPP_Clouds\Low-res Clouds_GameData inside\GameData\GPP";
                            cloudDirDest = @".\GameData\GPP";

                            var args = new Tuple<string, string, bool>(cloudDirSource, cloudDirDest, true);
                            workerInstall.RunWorkerAsync(args);
                        }
                        else if (modList[modIndex].ModName == "CloudsHighRes")
                        {
                            if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);

                            cloudDirSource = @".\GPPInstaller\Galileos.Planet.Pack.1.5.88\Optional Mods\GPP_Clouds\High-res Clouds_GameData inside\GameData\GPP";
                            cloudDirDest = @".\GameData\GPP";

                            var args = new Tuple<string, string, bool>(cloudDirSource, cloudDirDest, true);
                            workerInstall.RunWorkerAsync(args);
                        }
                    }
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
                    if (mod.ModPackName != "Clouds")
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
        public string ModPackName { get; set; }
        public string ModName { get; set; }
        public string DownloadAddress { get; set; }
        public string ArchiveFileName { get; set; }
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
