using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;
using HtmlAgilityPack;
using System.Linq;

// NOTE: Adding a new mod requires you to make changes
// here:
// - Core.SetCheckBoxes()
// - Core.ProcessActionToTake()
// - Core.UninstallMod()
// - Core.AddLinks()
// - Core.AddExtractedPath()
// - Form1.DisableCheckBoxes()
// - Form1.EnableCheckBoxes()

// TODO: Handle situations where a download site may not be accessable (spacedock scatterer)

// TODO: Final bug fixing before release.

namespace GPPInstaller
{
    class Core
    {
        Form1 form1;

        BackgroundWorker workerExtract = new BackgroundWorker();
        BackgroundWorker workerInstall = new BackgroundWorker();

        WebClient webclient = new WebClient();

        public List<Mod> modList = new List<Mod>();

        List<string> failedLinks = new List<string>();

        int modIndex = 0;

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

            InitModList();

            ScrapeLinks();
            AddFieldsToModList();

            CheckForScraperErrors();

            RefreshModState();
        }

        public void InitModList()
        {
            modList.Add(new Mod()
            {
                ModType = "Core",
                ModName = "Kopernicus",
                DownloadAddress = "",
                ArchiveFileName = "",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "Kopernicus",
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
                DownloadAddress = "",
                ArchiveFileName = "",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "GPP",
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
                DownloadAddress = "",
                ArchiveFileName = "",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "GPP_Textures",
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
                DownloadAddress = "",
                ArchiveFileName = "",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "EnvironmentalVisualEnhancements",
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
                DownloadAddress = "",
                ArchiveFileName = "",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "scatterer",
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
                DownloadAddress = "",
                ArchiveFileName = "",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "DistantObject",
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
                ExtractedPath = "",
                InstallDirName = "GPP_Clouds",
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
                ExtractedPath = "",
                InstallDirName = "GPP_Clouds",
                InstallDestPath = @".\GameData\GPP",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModType = "Utility",
                ModName = "KerbalEngineer",
                DownloadAddress = "",
                ArchiveFileName = "",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "KerbalEngineer",
                InstallDestPath = @".\GameData",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

            modList.Add(new Mod()
            {
                ModType = "Utility",
                ModName = "KerbalAlarmClock",
                DownloadAddress = "",
                ArchiveFileName = "",
                ArchiveFilePath = @".\GPPInstaller",
                ExtractedDirName = "",
                ExtractedPath = @".\GPPInstaller",
                InstallDirName = "TriggerTech",
                InstallDestPath = @".\GameData",
                State_Downloaded = false,
                State_Extracted = false,
                State_Installed = false,
                ActionToTake = ""
            });

        }

        private void CheckForScraperErrors()
        {
            foreach (string url in failedLinks)
            {
                if (failedLinks.Any())
                {
                    form1.ErrorScraperFail("Failed to connect to: " + url);
                }
            }
        }

        public void ScrapeLinks()
        {
            modList[GlobalInfo.kopericusIndex].DownloadAddress = GetLink(GlobalInfo.kopernicusUrl, GlobalInfo.kopernicusXpath);
            modList[GlobalInfo.gppIndex].DownloadAddress = GetLink(GlobalInfo.gppUrl, GlobalInfo.gppXpath);
            modList[GlobalInfo.gppTexturesIndex].DownloadAddress = GetLink(GlobalInfo.gppTexturesUrl, GlobalInfo.gppTexturesXpath);
            modList[GlobalInfo.eveIndex].DownloadAddress = GetLink(GlobalInfo.eveUrl, GlobalInfo.eveXpath);
            modList[GlobalInfo.scattererIndex].DownloadAddress = GetLink(GlobalInfo.scattererUrl, GlobalInfo.scattererXpath);
            modList[GlobalInfo.doeIndex].DownloadAddress = GetLink(GlobalInfo.doeUrl, GlobalInfo.doeXpath);
            modList[GlobalInfo.kerIndex].DownloadAddress = GetLink(GlobalInfo.kerUrl, GlobalInfo.kerXpath);
            modList[GlobalInfo.kacIndex].DownloadAddress = GetLink(GlobalInfo.kacUrl, GlobalInfo.kacXpath);
        }

        private string GetLink(string url, string xpath)
        {
            HtmlWeb web = new HtmlWeb();

            try
            {
                var htmlDoc = web.Load(url);
                var anchor = htmlDoc.DocumentNode.SelectNodes(xpath);

                var outterHtml = anchor.Select(node => node.OuterHtml);
                string item = outterHtml.ElementAt(0);

                string link;

                if (url == "https://spacedock.info/mod/141/scatterer")
                {
                    int leadingEnd = item.IndexOf("h") - 1;
                    string href = item.Remove(0, leadingEnd + 1);
                    leadingEnd = href.IndexOf('"') + 1;
                    href = href.Remove(0, leadingEnd);
                    int trailingStart = href.LastIndexOf('"');
                    href = href.Remove(trailingStart, (href.Length - trailingStart));

                    link = "https://spacedock.info" + href;
                }
                else
                {
                    int leadingEnd = item.IndexOf('"');
                    link = item.Remove(0, leadingEnd + 1);
                    int trailingStart = link.IndexOf('"');
                    link = link.Remove(trailingStart, link.Length - trailingStart);
                    link = "https://github.com" + link;
                }

                return link;
            }
            catch (Exception e)
            {
                AddFailedLink(url);
                return "";
            }
        }

        public void AddFailedLink(string downloadLink)
        {
            failedLinks.Add(downloadLink);
        }

        

        private void AddFieldsToModList()
        {
            // ArchiveFileName
            for (int i = 0; i < modList.Count; i++)
            {
                if (i != GlobalInfo.cloudsLowResIndex && i != GlobalInfo.cloudsHighResIndex)
                {
                    if (modList[i].ModName == "Scatterer")
                    {
                        modList[i].ArchiveFileName = GetArchiveFileName(modList[i].DownloadAddress, true);
                    }
                    else
                    {
                        modList[i].ArchiveFileName = GetArchiveFileName(modList[i].DownloadAddress);
                    }

                    
                }
            }

            // ExtractedDirName
            for (int i = 0; i < modList.Count; i++)
            {
                if (i != GlobalInfo.cloudsLowResIndex && i != GlobalInfo.cloudsHighResIndex)
                {
                    modList[i].ExtractedDirName = GlobalInfo.RemoveZip(modList[i].ArchiveFileName);
                }
            }

            // ExtractedDirPath
            for (int i = 0; i < modList.Count; i++)
            {
                if (modList[i].ModType == "Clouds")
                {
                    modList[i].ExtractedPath = AddExtractedPath(modList[i].ModName, modList[1].ExtractedDirName);
                }
                else
                {
                    modList[i].ExtractedPath = AddExtractedPath(modList[i].ModName, modList[i].ExtractedDirName);
                }
            }
        }

        public string GetArchiveFileName(string downloadLink)
        {
            if (downloadLink != "")
            {
                int subStart = downloadLink.LastIndexOf('/') + 1;
                int length = downloadLink.Length - subStart;
                string archive = downloadLink.Substring(subStart, length);

                return archive;
            }
            else
            {
                return "";
            } 

        }

        public string GetArchiveFileName(string downloadLink, bool isScatter)
        {
            if (downloadLink != "")
            {
                string archive;

                int offset = downloadLink.IndexOf('/');
                offset = downloadLink.IndexOf('/', offset + 1);
                offset = downloadLink.IndexOf('/', offset + 1);
                offset = downloadLink.IndexOf('/', offset + 1);
                int count = downloadLink.Length - offset;
                archive = downloadLink.Remove(0, count);

                int start = archive.IndexOf('/');
                int end = archive.LastIndexOf('/');
                count = end - start;
                archive = archive.Remove(start, count);
                archive = archive.Replace('/', '-');
                archive += ".zip";

                return archive;
            }
            else return "";
        }

        private string AddExtractedPath(string modName, string extractedDirName)
        {
            if (modName == "CloudsLowRes")
            {
                return @".\GPPInstaller\" + extractedDirName + @"\Optional Mods\GPP_Clouds\Low-res Clouds_GameData inside\GameData\GPP";
            }
            else if (modName == "CloudsHighRes")
            {
                return @".\GPPInstaller\" + extractedDirName + @"\Optional Mods\GPP_Clouds\High-res Clouds_GameData inside\GameData\GPP";
            }
            else if (modName == "KerbalEngineer")
            {
                return @".\GPPInstaller\" + extractedDirName;
            }
            else if (modName == "GPP_Textures")
            {
                return @".\GPPInstaller\" + extractedDirName + @"\GameData\GPP";
            }
            else
            {
                return @".\GPPInstaller\" + extractedDirName + @"\GameData";
            }
        }


        public void RefreshModState()
        {
            // downloaded
            foreach (Mod mod in modList)
            {
                if (mod.ModName == "CloudsLowRes" &&
                    modList[GlobalInfo.gppIndex].State_Downloaded == true)
                {
                    mod.State_Downloaded = true;       
                }
                else mod.State_Downloaded = false;

                if (mod.ModName == "CloudsHighRes" &&
                    modList[GlobalInfo.gppIndex].State_Downloaded == true)
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
                if (Directory.Exists(mod.ExtractedPath + @"\" + mod.InstallDirName))
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
            CheckBox utilityCheckBox,
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

                if (mod.ModType == "Utility")
                {
                    if (mod.State_Installed == true)
                    {
                        utilityCheckBox.Checked = true;
                    }
                    else utilityCheckBox.Checked = false;
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
            CheckBox utilityCheckBox,
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

                    if (mod.ModType == "Utility" &&
                        utilityCheckBox.Checked == true)
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

                    if (mod.ModType == "Utility" &&
                        utilityCheckBox.Checked == false)
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
                        form1.ErrorNoInternetConnection("No internet connection detected.");
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
                GlobalInfo.DeleteAllZips(@".\GPPInstaller");

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
                    Debug.WriteLine(modList[modIndex].ModName);

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
                            // NOTE: It appears that when entering the worker for
                            // a second time (re-use the previous worker) the values
                            // of variables are re-used. 
                            // NOTE: The solution to this problem was to pass new arguments to the
                            // background worker DoWork event. Used a Tuple since you can only pass
                            // one arg.

                            var args = new Tuple<string, string>(zipFile, destDir);

                            // NOTE: in-case the zip file download failed
                            try
                            {
                                workerExtract.RunWorkerAsync(args);
                            }
                            catch (InvalidDataException exception)
                            {
                                form1.ErrorInvalidData("Download was incomplete.");
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

                GlobalInfo.DeleteAllZips(@".\GPPInstaller");

                InstallMod();
            }
        }

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
                    string sourceDirName = modList[modIndex].ExtractedPath;
                    string destDirName = modList[modIndex].InstallDestPath;

                    if (modList[modIndex].ModType == "Clouds")
                    {
                        if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);
                    }

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


        public string GetGPPVersion()
        {
            string dirName = modList[GlobalInfo.gppIndex].ExtractedDirName;
            int offset = dirName.LastIndexOf(".");
            offset = dirName.LastIndexOf(".", offset - 1);
            int leadingEnd = dirName.LastIndexOf(".", offset - 1) + 1;
            string result = dirName.Remove(0, leadingEnd);

            return result;
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
            
            if (modList[GlobalInfo.gppIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(".\\GameData\\GPP")) Directory.Delete(".\\GameData\\GPP", true);

                modList[GlobalInfo.gppIndex].State_Installed = false;
            }

            if (modList[GlobalInfo.eveIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\EnvironmentalVisualEnhancements")) Directory.Delete(@".\GameData\EnvironmentalVisualEnhancements", true);

                modList[GlobalInfo.eveIndex].State_Installed = false;
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

            if (modList[GlobalInfo.kerIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\KerbalEngineer")) Directory.Delete(@".\GameData\KerbalEngineer", true);
                File.Delete(@".\GameData\CHANGES.txt");
                File.Delete(@".\GameData\LICENCE.txt");
                File.Delete(@".\GameData\README.htm");

                modList[GlobalInfo.kerIndex].State_Installed = false;
            }

            if (modList[GlobalInfo.kacIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\TriggerTech")) Directory.Delete(@".\GameData\TriggerTech", true);

                modList[GlobalInfo.kacIndex].State_Installed = false;
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
    }

}
