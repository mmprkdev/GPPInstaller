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

//TODO: Utilize more built in helper methods when getting and
// setting information about directory paths. (look at DirecoryCopy()
// for good examples)

namespace GPPInstaller
{
    public partial class Form1 : Form
    {
        public static string MAIN_DIR = Directory.GetCurrentDirectory();

        WebClient webclient;

        public Form1()
        {
            InitializeComponent();

            // Check for ksp version number using the readme.txt file
            label1.Text = "KSP Version: " + GetVersionNumber();

            // Create a mod storage dir
            Directory.CreateDirectory(MAIN_DIR + "\\GPPInstaller");
        }

        private string GetVersionNumber()
        {
            //string currentDir = Directory.GetCurrentDirectory();
            string target = MAIN_DIR + "\\readme.txt";

            if (!File.Exists(target))
            {
                // Make a popup window for this error
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


        // Starts intallation process
        private void button1_Click(object sender, EventArgs e)
        {
            // Check to make sure the Gamedata folder is clean
            // TODO: Make a good error checking system.
            CheckGameData();

            // Download and install compatable version of Kopernicus
            //DownLoadAndInstallKopernicus();
            DownloadAndIntall("Kopernicus");

            // 
        }

        private void CheckGameData()
        {
            string targetDir = Directory.GetCurrentDirectory() + "\\GameData";
            if (!Directory.Exists(targetDir))
            {
                // Throw an error 
                DisplayError("GameData folder does not exist.");
                return;
            }

            string[] directories = Directory.GetDirectories(targetDir);
            string[] files = Directory.GetFiles(targetDir);
            string squadDir = targetDir + "\\Squad";
            if (directories[0] != squadDir)
            {
                DisplayError("GameData does not contain vanilla squad folders.");
                return;
            }

            if (directories.Length > 1)
            {
                DisplayError("GameData is not clean.");
                return;
            }
            
            if (files.Length > 0)
            {
                DisplayError("GameData is not clean.");
                return;
            }

        }

        private void DisplayError(string errorMessage)
        {
            errorLabel.Visible = true;
            errorLabel.Text = "Error: " + errorMessage;
        }

        private void DownloadAndIntall(string modName)
        {
            webclient = new WebClient();

            if (modName == "Kopernicus")
            {
                string downloadAddress = "https://github.com/Kopernicus/Kopernicus/releases/download/release-1.3.1-2/Kopernicus-1.3.1-2.zip";
                string fileName = "Kopernicus-1.3.1-2.zip";
                string gppDir = @".\GPPInstaller";

                webclient.DownloadFile(downloadAddress, fileName);

                try
                {
                    string sourceFile = @".\" + fileName;
                    string destFile = gppDir + "\\" + fileName;

                    if (File.Exists(destFile))
                    {
                        File.Delete(destFile);
                    }

                    File.Move(sourceFile, destFile);
                }
                catch (Exception e)
                {
                    DisplayError(e.ToString());
                }
            }
        }

        private void DownLoadAndInstallKopernicus()
        {
            webclient = new WebClient();
            string downloadAddress = "https://github.com/Kopernicus/Kopernicus/releases/download/release-1.3.1-2/Kopernicus-1.3.1-2.zip";
            string fileName = "Kopernicus-1.3.1-2.zip";
            string gppDir = MAIN_DIR + "\\GPPInstaller";

            webclient.DownloadFile(downloadAddress, fileName);

            try
            {
                string sourceFile = MAIN_DIR + "\\" + fileName;
                string destFile = gppDir + "\\" + fileName;

                if (File.Exists(destFile))
                {
                    File.Delete(destFile);
                }

                File.Move(sourceFile, destFile);
            }
            catch (Exception e)
            {
                DisplayError(e.ToString());
            }

            // NOTE: Needed to add a reference to System.IO.Compression.FileSystem
            // in order to get ZipFile to work. Right click on References in the Solution
            // Explorer to add references.
            string dirName = fileName.Remove(18, 4);
            string destDir = gppDir + "\\" + dirName;
            DirectoryInfo destDirInfo = new DirectoryInfo(destDir);

            if (destDirInfo.Exists)
            {
                Directory.Delete(destDir, true);
            }

            string zipFile = gppDir + "\\" + fileName;
            Directory.CreateDirectory(gppDir + "\\" + dirName);
            ZipFile.ExtractToDirectory(zipFile, destDir);
            

            string sourceDirName = gppDir + "\\" + dirName + "\\GameData";
            string destDirName = MAIN_DIR + "\\GameData";
            DirectoryCopy(sourceDirName, destDirName, true);
            
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
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
            foreach(FileInfo file in files)
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
