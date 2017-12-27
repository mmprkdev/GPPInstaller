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

// TODO: Maybe add a few extra "utility" mods
// (Kerbal Engineer, Kerbal Alarm Clock, etc)

// TODO: Give option to delete save files when uninstalling core mods or
// when installing a new version of GPP. 

// TODO: Write a bash script that adds all
// files in the github_repo\GPPInstaller dir automatically.

// TODO: Consider deleting the zip file after it is extracted

namespace GPPInstaller
{
    class Utility
    {
        private int modIndex = 0;

        private int kopericusIndex = 0;
        private int GPPIndex = 1;
        private int GPPTexturesIndex = 2;
        private int EVEIndex = 3;
        private int scattererIndex = 4;
        private int doeIndex = 5;
        private int cloudsLowResIndex = 6;
        private int cloudsHighResIndex = 7;

        private Form1 form1;

        public static List<Mod> modList = new List<Mod>();

        private WebClient webclient = new WebClient();

        public Utility(Form1 form1)
        {
            this.form1 = form1;

            webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webclient_DownloadProgressChanged);
            webclient.DownloadFileCompleted += new AsyncCompletedEventHandler(webclient_DownloadFileCompleted);
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

            RefreshModState();
        }

        private void RefreshModState()
        {
            // NOTE: Check if downloaded into .\GPPInstaller
            string kopernicusZip_gppInstaller = @".\GPPInstaller\Kopernicus-1.3.1-2.zip";
            string GPPZip_gppInstaller = @".\GPPInstaller\Galileos.Planet.Pack.1.5.88.zip";
            string gppTexturesZip_gppInstaller = @".\GPPInstaller\GPP_Textures-3.0.0.zip";

            string eveZip_gppInstaller = @".\GPPInstaller\EnvironmentalVisualEnhancements-1.2.2.1.zip";
            string scattererZip_gppInstaller = @".\GPPInstaller\scatterer-0.0320b.zip";
            string distantOEZip_gppInstaller = @".\GPPInstaller\DistantObject_1.9.1.zip";

            if (File.Exists(kopernicusZip_gppInstaller))
            {
                modList[kopericusIndex].State_Downloaded = true;
            }

            if (File.Exists(GPPZip_gppInstaller))
            {
                modList[GPPIndex].State_Downloaded = true;
            }

            if (File.Exists(gppTexturesZip_gppInstaller))
            {
                modList[GPPTexturesIndex].State_Downloaded = true;
            }

            if (File.Exists(eveZip_gppInstaller))
            {
                modList[EVEIndex].State_Downloaded = true;
            }

            if (File.Exists(scattererZip_gppInstaller))
            {
                modList[scattererIndex].State_Downloaded = true;
            }

            if (File.Exists(distantOEZip_gppInstaller))
            {
                modList[doeIndex].State_Downloaded = true;
            }

            if (modList[GPPIndex].State_Downloaded == true)
            {
                modList[cloudsLowResIndex].State_Downloaded = true;
                modList[cloudsHighResIndex].State_Downloaded = true;
            }

            // NOTE: Check if extracted into .\GPPInstaller
            string kopernicusDir_gppInstaller = @".\GPPInstaller\Kopernicus-1.3.1-2";
            string GPPDir_gppInstaller = @".\GPPInstaller\Galileos.Planet.Pack.1.5.88";
            string gppTexturesDir_gppInstaller = @".\GPPInstaller\GPP_Textures-3.0.0";

            string eveDir_gppInstaller = @".\GPPInstaller\EnvironmentalVisualEnhancements-1.2.2.1";
            string scattererDir_gppInstaller = @".\GPPInstaller\scatterer-0.0320b";
            string distantOEDir_gppInstaller = @".\GPPInstaller\DistantObject_1.9.1";


            if (Directory.Exists(kopernicusDir_gppInstaller))
            {
                modList[kopericusIndex].State_Extracted = true;
            }

            if (Directory.Exists(GPPDir_gppInstaller))
            {
                modList[GPPIndex].State_Extracted = true;
            }

            if (Directory.Exists(gppTexturesDir_gppInstaller))
            {
                modList[GPPTexturesIndex].State_Extracted = true;
            }

            if (Directory.Exists(eveDir_gppInstaller))
            {
                modList[EVEIndex].State_Extracted = true;
            }

            if (Directory.Exists(scattererDir_gppInstaller))
            {
                modList[scattererIndex].State_Extracted = true;
            }

            if (Directory.Exists(distantOEDir_gppInstaller))
            {
                modList[doeIndex].State_Extracted = true;
            }

            if (modList[GPPIndex].State_Extracted == true)
            {
                modList[cloudsLowResIndex].State_Extracted = true;
                modList[cloudsHighResIndex].State_Extracted = true;
            }

            // NOTE: Check if installed into .\GameData
            string kopernicusDir_gamedata = @".\GameData\Kopernicus";
            string modularFIDir_gamedata = @".\GameData\ModularFlightIntegrator";
            string modManagerFile_gamedata = @".\GameData\ModuleManager.2.8.1.dll";
            string GPPDir_gamedata = @".\GameData\GPP";
            string GPPTextures_gamedata = @".\GameData\GPP\GPP_Textures";

            string eveDir_gamedata = @".\GameData\EnvironmentalVisualEnhancements";
            string scattererDir_gamedata = @".\GameData\scatterer";
            string distantOEDir_gamedata = @".\GameData\DistantObject";

            string cloudsLowResConfig_gamedata = @".\GameData\GPP\GPP_Clouds\Configs\GPPClouds_LowRes.cfg";
            string cloudsHighResConfig_gamedata = @".\GameData\GPP\GPP_Clouds\Configs\GPPClouds_HighRes.cfg";

            if (Directory.Exists(kopernicusDir_gamedata) &&
                Directory.Exists(modularFIDir_gamedata) &&
                File.Exists(modManagerFile_gamedata))
            {
                modList[kopericusIndex].State_Installed = true;
            }

            if (Directory.Exists(GPPDir_gamedata))
            {
                modList[GPPIndex].State_Installed = true;
            }

            if (Directory.Exists(GPPTextures_gamedata))
            {
                modList[GPPTexturesIndex].State_Installed = true;
            }

            if (Directory.Exists(eveDir_gamedata))
            {
                modList[EVEIndex].State_Installed = true;
            }

            if (Directory.Exists(scattererDir_gamedata))
            {
                modList[scattererIndex].State_Installed = true;
            }

            if (Directory.Exists(distantOEDir_gamedata))
            {
                modList[doeIndex].State_Installed = true;
            }

            if (File.Exists(cloudsLowResConfig_gamedata))
            {
                modList[cloudsLowResIndex].State_Installed = true;
            }

            if (File.Exists(cloudsHighResConfig_gamedata))
            {
                modList[cloudsHighResIndex].State_Installed = true;
            }
        }

        public void SetCheckBoxes(
            CheckBox coreCheckBox,
            CheckBox visualsCheckBox,
            CheckBox cloudsLowResCheckBox,
            CheckBox cloudsHighResCheckBox)
        {
            if (modList[kopericusIndex].State_Installed == true &&
                modList[GPPIndex].State_Installed == true &&
                modList[GPPTexturesIndex].State_Installed == true)
            {
                coreCheckBox.Checked = true;
            }
            else coreCheckBox.Checked = false;

            if (modList[EVEIndex].State_Installed == true &&
                modList[scattererIndex].State_Installed == true &&
                modList[doeIndex].State_Installed == true)
            {
                visualsCheckBox.Checked = true;
            }
            else visualsCheckBox.Checked = false;

            if (modList[cloudsLowResIndex].State_Installed == true)
            {
                cloudsLowResCheckBox.Checked = true;
            }
            else cloudsLowResCheckBox.Checked = false;

            if (modList[cloudsHighResIndex].State_Installed == true)
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
            // TODO: When coreCheckBox is selected, make sure to
            // set all core mods to be installed

            foreach (Mod mod in modList)
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

            // Set to install
            //foreach (Mod mod in modList)
            //{
            //    if (mod.ModPackName == "Core" &&
            //        mod.State_Installed == false &&
            //        coreCheckBox.Checked == true)
            //    {
            //        mod.ActionToTake = "Install";
            //    }

            //    if (mod.ModPackName == "Visuals" &&
            //        mod.State_Installed == false &&
            //        visualsCheckBox.Checked)
            //    {
            //        mod.ActionToTake = "Install";
            //    }

            //    if (mod.ModName == "CloudsLowRes" &&
            //        mod.State_Installed == false &&
            //        cloudsLowResCheckBox.Checked)
            //    {
            //        mod.ActionToTake = "Install";
            //    }

            //    if (mod.ModName == "CloudsHighRes" &&
            //        mod.State_Installed == false &&
            //        cloudsHighResCheckBox.Checked)
            //    {
            //        mod.ActionToTake = "Install";
            //    }
            //}

            //// Set to uninstall
            //foreach (Mod mod in modList)
            //{
            //    if (mod.ModPackName == "Core" &&
            //        mod.State_Installed == true &&
            //        coreCheckBox.Checked == false)
            //    {
            //        mod.ActionToTake = "Uninstall";
            //    }

            //    if (mod.ModPackName == "Visuals" &&
            //        mod.State_Installed == true &&
            //        visualsCheckBox.Checked == false)
            //    {
            //        mod.ActionToTake = "Uninstall";
            //    }

            //    if (mod.ModName == "CloudsLowRes" &&
            //        mod.State_Installed == true &&
            //        cloudsLowResCheckBox.Checked == false)
            //    {
            //        mod.ActionToTake = "Uninstall";
            //    }

            //    if (mod.ModName == "CloudsHighRes" &&
            //        mod.State_Installed == true &&
            //        cloudsHighResCheckBox.Checked == false)
            //    {
            //        mod.ActionToTake = "Uninstall";
            //    }
            //}
        }

        private void ResetActionsToTake()
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
                if (modList[modIndex].State_Downloaded == false &&
                modList[modIndex].ActionToTake == "Install" &&
                modList[modIndex].DownloadAddress != "")
                {
                    string downloadAddress = modList[modIndex].DownloadAddress;
                    string fileName = modList[modIndex].ArchiveFileName;
                    string downloadDest = @".\GPPInstaller\" + fileName;

                    Uri uri = new Uri(downloadAddress);

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
            //if (cancelWebclient == true)
            //{
            //    DownloadMod(true);
            //    File.Delete(@".\GPPInstaller\" + modList[modIndex].ArchiveFileName);

            //    cancelWebclient = false;
            //    modIndex = 0;
            //    form1.DisplayRedCheck();
            //    form1.ProgressLabelUpdate("Installation canceled.");

            //    return;
            //}

            string websiteName = WebsiteNamePop(modList[modIndex].DownloadAddress);
            string output = "Downloading: " + modList[modIndex].ArchiveFileName + " from " + websiteName + " " + e.ProgressPercentage + "% complete...";
            form1.ProgressLabelUpdate(output);
        }

        private void webclient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                // delete partially downloaded file
                File.Delete(@".\GPPInstaller\" + modList[modIndex].ArchiveFileName);

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
                    Debug.WriteLine(modList[modIndex].ModName);
                    // extract the file
                    string fileName = modList[modIndex].ArchiveFileName;
                    int fileNameLength = fileName.Length;
                    string dirName = fileName.Remove((fileNameLength - 4), 4);
                    string destDir = @".\GPPInstaller\" + dirName;

                    DirectoryInfo destDirInfo = new DirectoryInfo(destDir);

                    string zipFile = @".\GPPInstaller\" + fileName;

                    if (!destDirInfo.Exists) // TODO: maybe get rid of this check
                    {
                        Directory.CreateDirectory(destDir);

                        BackgroundWorker workerExtract = new BackgroundWorker();
                        workerExtract.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerExtract_RunWorkerCompleted);

                        workerExtract.DoWork += (o, e) =>
                        {
                            ZipFile.ExtractToDirectory(zipFile, destDir);
                        };
                        workerExtract.RunWorkerAsync();
                        //// TODO: Fix "Central directory corrupt" exception
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

        private void workerExtract_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                string fileName = modList[modIndex].ArchiveFileName;
                int fileNameLength = fileName.Length;
                string dirName = fileName.Remove((fileNameLength - 4), 4);
                if (Directory.Exists(@".\GPPInstaller\" + dirName)) Directory.Delete(@".\GPPInstaller\" + dirName, true);

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

        //public void ExtractCancel()
        //{
        //    workerExtract.CancelAsync();
        //}

        private void InstallMod()
        {
            if (modIndex < modList.Count)
            {
                BackgroundWorker workerInstallMod = new BackgroundWorker();
                workerInstallMod.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerInstallMods_RunWorkerCompleted);

                if (modList[modIndex].State_Installed == false &&
                    modList[modIndex].ActionToTake == "Install")
                {
                    if (modList[modIndex].ModPackName != "Clouds")
                    {
                        string sourceDirName;
                        string destDirName;

                        // TODO: Maybe remove these variables and just give the
                        // full string paths to source and dest dir
                        string fileName = modList[modIndex].ArchiveFileName;
                        int fileNameLength = fileName.Length;
                        string dirName = fileName.Remove((fileNameLength - 4), 4);
                        if (modList[modIndex].ModName == "GPP_Textures")
                        {
                            sourceDirName = @".\GPPInstaller\" + dirName + @"\GameData\GPP";
                            destDirName = @".\GameData\GPP";

                            workerInstallMod.DoWork += (o, e) =>
                            {
                                DirectoryCopy(sourceDirName, destDirName, true);
                            };
                            workerInstallMod.RunWorkerAsync();
                        }
                        else
                        {
                            sourceDirName = @".\GPPInstaller\" + dirName + @"\GameData";
                            destDirName = @".\GameData";

                            workerInstallMod.DoWork += (o, e) =>
                            {
                                DirectoryCopy(sourceDirName, destDirName, true);
                            };
                            workerInstallMod.RunWorkerAsync();
                        }
                    }
                    else
                    {
                        string cloudDirSource;
                        string cloudDirDest;
                        // install cloud mods
                        if (modList[modIndex].ModName == "CloudsLowRes")
                        {
                            if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);

                            cloudDirSource = @".\GPPInstaller\Galileos.Planet.Pack.1.5.88\Optional Mods\GPP_Clouds\Low-res Clouds_GameData inside\GameData\GPP";
                            cloudDirDest = @".\GameData\GPP";

                            workerInstallMod.DoWork += (o, e) =>
                            {
                                DirectoryCopy(cloudDirSource, cloudDirDest, true);
                            };
                            workerInstallMod.RunWorkerAsync();
                            // TODO: left off here
                        }
                        else if (modList[modIndex].ModName == "CloudsHighRes")
                        {
                            if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);

                            cloudDirSource = @".\GPPInstaller\Galileos.Planet.Pack.1.5.88\Optional Mods\GPP_Clouds\High-res Clouds_GameData inside\GameData\GPP";
                            cloudDirDest = @".\GameData\GPP";

                            workerInstallMod.DoWork += (o, e) =>
                            {
                                DirectoryCopy(cloudDirSource, cloudDirDest, true);
                            };
                            workerInstallMod.RunWorkerAsync();
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
                form1.ProgressLabelUpdate("All changes applied.");
            }
            
        }

        private void workerInstallMods_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            modList[modIndex].State_Installed = true;
            form1.ProgressBar1Step();
            modIndex++;
            InstallMod();
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

        public void Uninstall()
        {
            if (modList[kopericusIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(".\\GameData\\Kopernicus")) Directory.Delete(".\\GameData\\Kopernicus", true);
                if (Directory.Exists(".\\GameData\\ModularFlightIntegrator")) Directory.Delete(".\\GameData\\ModularFlightIntegrator", true);
                File.Delete(".\\GameData\\ModuleManager.2.8.1.dll");
                File.Delete(".\\GameData\\ModuleManagerLicense.md");
                File.Delete(".\\GameData\\ModuleManager.ConfigCache");
                File.Delete(".\\GameData\\ModuleManager.ConfigSHA");
                File.Delete(".\\GameData\\ModuleManager.Physics");
                File.Delete(".\\GameData\\ModuleManager.TechTree");

                modList[kopericusIndex].State_Installed = false;
            }
            
            if (modList[GPPIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(".\\GameData\\GPP")) Directory.Delete(".\\GameData\\GPP", true);

                modList[GPPIndex].State_Installed = false;
            }

            if (modList[EVEIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\EnvironmentalVisualEnhancements")) Directory.Delete(@".\GameData\EnvironmentalVisualEnhancements", true);

                modList[EVEIndex].State_Installed = false;
            }

            if (modList[scattererIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\scatterer")) Directory.Delete(@".\GameData\scatterer", true);

                modList[scattererIndex].State_Installed = false;
            }

            if (modList[doeIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\DistantObject")) Directory.Delete(@".\GameData\DistantObject", true);

                modList[doeIndex].State_Installed = false;
            }

            if (modList[cloudsLowResIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);

                modList[cloudsLowResIndex].State_Installed = false;
            }

            if (modList[cloudsHighResIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);

                modList[cloudsHighResIndex].State_Installed = false;
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
    }

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
}
