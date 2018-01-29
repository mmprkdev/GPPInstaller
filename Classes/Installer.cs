using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.IO.Compression;

namespace GPPInstaller
{
    class Installer : IInstaller
    {
        private readonly Form1 _form1;
        private int modIndex = 0;
        private readonly WebClient webclient;
        private readonly BackgroundWorker workerExtract;
        private readonly BackgroundWorker workerCopy;

        public Installer(Form1 form1)
        {
            _form1 = form1;
            webclient = new WebClient();
            workerExtract = new BackgroundWorker();
            workerCopy = new BackgroundWorker();

            webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webclient_DownloadProgressChanged);
            webclient.DownloadFileCompleted += new AsyncCompletedEventHandler(webclient_DownloadFileCompleted);
            workerExtract.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerExtract_RunWorkerCompleted);
            workerExtract.DoWork += new DoWorkEventHandler(workerExtract_DoWork);
            workerExtract.WorkerSupportsCancellation = true;

            workerCopy.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerCopy_RunWorkerCompleted);
            workerCopy.DoWork += new DoWorkEventHandler(workerCopy_DoWork);
            workerCopy.WorkerSupportsCancellation = true;
        }

        public void DownloadMod()
        {
            
            if (modIndex < _form1.modList.Count)
            {
                if (_form1.modList[modIndex].State_Extracted == false &&
                _form1.modList[modIndex].State_Downloaded == false &&
                _form1.modList[modIndex].ActionToTake == "Install" &&
                _form1.modList[modIndex].DownloadAddress != "")
                {
                    string downloadAddress = _form1.modList[modIndex].DownloadAddress;
                    string fileName = _form1.modList[modIndex].ArchiveFileName;
                    string downloadDest = @".\GPPInstaller\" + fileName;

                    Uri uri = new Uri(downloadAddress);

                    if (!GlobalInfo.IsConnectedToInternet())
                    {
                        webclient.CancelAsync();
                        _form1.InstallCanceled();
                        throw new WebException();
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
                modIndex = 0;
                ExtractMod();
            }
        }

        public void webclient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            string websiteName = GlobalInfo.WebsiteNamePop(_form1.modList[modIndex].DownloadAddress);
            string output = "Downloading: " + _form1.modList[modIndex].ArchiveFileName + " from " + websiteName + " " + e.ProgressPercentage + "% complete...";
            _form1.ProgressLabelUpdate(output);
        }

        public void webclient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                GlobalInfo.DeleteAllZips(@".\GPPInstaller");
                _form1.InstallCanceled();
                _form1.EndOfInstall();

                return;
            }

            _form1.modList[modIndex].State_Downloaded = true;
            _form1.ProgressBar1Step();
            modIndex++;
            DownloadMod();
        }

        public void WebClientCancel()
        {
            webclient.CancelAsync();
        }

        public void ExtractMod()
        {
            if (modIndex < _form1.modList.Count)
            {
                if (_form1.modList[modIndex].State_Extracted == false &&
                    _form1.modList[modIndex].ActionToTake == "Install" &&
                    _form1.modList[modIndex].State_Downloaded == true &&
                    _form1.modList[modIndex].ArchiveFileName != "")
                {
                    _form1.ProgressLabelUpdate("Extracting " + _form1.modList[modIndex].ModName + "...");

                    string fileName = _form1.modList[modIndex].ArchiveFileName;
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

                            try
                            {
                                workerExtract.RunWorkerAsync(args);
                            }
                            catch (Exception)
                            {
                                throw new InvalidDataException();
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
                GlobalInfo.DeleteAllZips(@".\GPPInstaller");
                CopyMod();
            }
        }

        public void workerExtract_DoWork(object sender, DoWorkEventArgs e)
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

        public void workerExtract_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                _form1.InstallCanceled();
                _form1.EndOfInstall();

                return;
            }

            _form1.modList[modIndex].State_Extracted = true;
            _form1.ProgressBar1Step();
            modIndex++;
            ExtractMod();
        }

        public void ExtractCancel()
        {
            workerExtract.CancelAsync();
        }

        public void CopyMod()
        {
            if (modIndex < _form1.modList.Count)
            {
                if (_form1.modList[modIndex].State_Installed == false &&
                    _form1.modList[modIndex].ActionToTake == "Install")
                {
                    _form1.ProgressLabelUpdate("Copying " + _form1.modList[modIndex].ModName + " to GameData...");

                    string sourceDirName = _form1.modList[modIndex].InstallSourcePath;
                    string destDirName = _form1.modList[modIndex].InstallDestPath;

                    if (_form1.modList[modIndex].ModType == "Clouds")
                    {
                        if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);
                    }

                    var args = new Tuple<string, string, bool, Mod>(sourceDirName, destDirName, true, _form1.modList[modIndex]);
                    workerCopy.RunWorkerAsync(args);
                }
                else
                {
                    modIndex++;
                    CopyMod();
                }
            }
            else
            {
                modIndex = 0;
                _form1.InstallSuccess();
                _form1.EndOfInstall();
            }
        }

        public void workerCopy_DoWork(object sender, DoWorkEventArgs e)
        {
            if (workerCopy.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            var args = (Tuple<string, string, bool, Mod>)e.Argument;

            string sourceDirName = args.Item1;
            string destDirName = args.Item2;
            bool copySubDirs = args.Item3;
            Mod mod = args.Item4;

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

        public void workerCopy_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                _form1.InstallCanceled();
                _form1.EndOfInstall();

                return;
            }
            _form1.InsertVersionFile(_form1.modList[modIndex]);
            _form1.modList[modIndex].State_Installed = true;
            _form1.ProgressBar1Step();
            modIndex++;
            CopyMod();
        }

        public void CopyCancel()
        {
            workerCopy.CancelAsync();
        }

        public void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            // TODO: catch this exception or don't throw it
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
    }
}
