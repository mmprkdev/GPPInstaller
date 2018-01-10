using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Net;
using HtmlAgilityPack;

namespace GPPInstaller
{
    public partial class Form1 : Form
    {
        private Core core;


        public Form1()
        {
            InitializeComponent();

            Directory.CreateDirectory(".\\GPPInstaller");

            core = new Core(this);

            core.SetCheckBoxes(checkBox1, checkBox2, checkBox3, checkBox4);

            InitialCheckForErrors();

            // TODO: Grab a dynamic copy of the current GPP version
            //GPP Installer(GPP v1.5.88) (KSP v1.3.1)
            //this.Text = "GPP Installer(GPP v" +  + ") (KSP v1.3.1)";
        }

        private void InitialCheckForErrors()
        {
            // Version
            string versionTarget = @".\readme.txt";

            if (!File.Exists(versionTarget))
            {
                DisplayError("Error: Could not determine KSP version. Make sure the \"readme.txt\" file exists.");
            }

            string[] readmeLines = File.ReadAllLines(versionTarget);

            string versionNumberLine = readmeLines[14];

            char[] versionChars = new char[5];
            for (int lineI = 8, charI = 0; lineI <= 12; lineI++, charI++)
            {
                versionChars[charI] = versionNumberLine[lineI];
            }

            string detectedVersionNumber = new string(versionChars);

            if (detectedVersionNumber != GlobalInfo.compatableKSPVersion)
            {
                DisplayError("Error: The detected KSP version " + detectedVersionNumber + " is not compatable. Version " + GlobalInfo.compatableKSPVersion + " is required.");
            }

            // EXE
            string exeTarget64 = @".\KSP_x64.exe";
            string exeTarget32 = @".\KSP.exe";
            string currentExe = "";

            if (File.Exists(exeTarget64))
            {
                currentExe = "64";

            }
            else if (File.Exists(exeTarget32))
            {
                currentExe = "32";
                DisplayError("Error: a 32 bit version of KSP was detected. GPP requires a 64 bit version of KSP in order to run.");
            }
            else
            {
                DisplayError("Error: could not determine the exe type. Make sure the KSP exicutable file exists.");
            }

            label1.Text = "KSP Version: " + detectedVersionNumber + " (" + currentExe + ") bit";
        }

        public void RefreshCheckBoxes()
        {
            core.SetCheckBoxes(checkBox1, checkBox2, checkBox3, checkBox4);
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Core checkBox

            if (!checkBox1.Checked) checkBox2.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // Visuals checkBox

            if (checkBox2.Checked)
            {
                cloudTextureLabel.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;

                // NOTE:
                // Automatically check checkBox1 when checkBox2 is checked,
                // because we can only install the visual mods if the core mods
                // are also installed.
                if (!checkBox1.Checked) checkBox1.Checked = true;
            }
            else if (checkBox2.Checked == false)
            {
                cloudTextureLabel.Visible = false;

                checkBox3.Checked = false;
                checkBox4.Checked = false;

                checkBox3.Visible = false;
                checkBox4.Visible = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            // CloudsLowRes checkBox

            if (checkBox3.Checked) checkBox4.Checked = false;

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            // CloudsHighRes checkBox

            applyButton.Enabled = true;

            if (checkBox4.Checked) checkBox3.Checked = false;
        }

        private void DisableCheckBoxes()
        {
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            checkBox4.Enabled = false;
        }

        public void EnableCheckBoxes()
        {
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
        }

        public void ProgressBar1Init()
        {
            int numSteps = core.NumberOfSteps();

            progressBar1.Minimum = 0;
            progressBar1.Maximum = numSteps;
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            progressBar1.Visible = true;
        }

        public void ProgressBar1Step()
        {
            
            progressBar1.PerformStep();
        }

        public void ProgressLabelUpdate(string Message)
        {
            progressLabel.Visible = true;
            progressLabel.Text = Message;

        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            core.RefreshModState();
            core.ResetActionsToTake();

            exitButton.Enabled = false;
            cancelButton.Visible = true;
            pictureBox1.Visible = false;
            applyButton.Enabled = false;
            DisableCheckBoxes();

            core.ProcessActionToTake(checkBox1, checkBox2, checkBox3, checkBox4);
            core.UninstallMod();
            ProgressBar1Init();
            core.DownloadMod();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = @".\KSP_x64.exe";
            process.Start();

            Application.Exit();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            core.WebClientCancel();
            core.ExtractCancel();
            core.InstallCancel();
        }

        public void EnableApplyButton()
        {
            applyButton.Enabled = true;
        }

        public void RemoveProgressBar()
        {
            progressBar1.Visible = false;
            progressBar1.Value = 0;
        }

        public void DisplayGreenCheck()
        {
            pictureBox1.Image = Properties.Resources.checkmark_green;
            pictureBox1.Refresh();
            pictureBox1.Visible = true;
        }

        public void DisplayRedCheck()
        {
            pictureBox1.Image = Properties.Resources.checkmark_red;
            pictureBox1.Refresh();
            pictureBox1.Visible = true;
        }

        public void EnableExitButton()
        {
            exitButton.Enabled = true;
        }

        public void RemoveCancelButton()
        {
            cancelButton.Visible = false;
        }

        public void DisplayError(string message)
        {
            DisplayRedCheck();

            progressBar1.Visible = false;

            progressLabel.Visible = true;
            progressLabel.Text += message + "\n";

            DisableCheckBoxes();
            restartButton.Visible = true;
            applyButton.Visible = false;
        }

        public void DisplayYellowWarning()
        {
            pictureBox1.Image = Properties.Resources.warning2;
            pictureBox1.Refresh();
            pictureBox1.Visible = true;
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        //private void CheckVersions()
        //{
        //    string kopernicusUrl = "https://github.com/Kopernicus/Kopernicus/releases";
        //    string gppUrl = "https://github.com/Galileo88/Galileos-Planet-Pack/releases";
        //    string gppTexturesUrl = "https://github.com/Galileo88/Galileos-Planet-Pack/releases/tag/3.0.0";
        //    string eveUrl = "https://github.com/WazWaz/EnvironmentalVisualEnhancements/releases";
        //    string scattererUrl = "https://spacedock.info/mod/141/scatterer";
        //    string doeUrl = "https://github.com/MOARdV/DistantObject/releases";

        //    string kopernicusXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div[1]/div[2]/div[1]/h1/a";
        //    string gppXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div[1]/div[2]/div[1]/h1/a";
        //    string gppTexturesXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div/div[2]/div[1]/h1/a";
        //    string eveXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div[1]/div[2]/div[1]/h1/a";
        //    string scattererXpath = "/html/body/div[7]/div/div[2]/div/div[1]/div/div[2]/h2";
        //    string doeXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div[1]/div[2]/div[1]/h1/a";


        //    //*[@id="download-link-primary"]

        //    //GetVersionNumberGitHub(kopernicusUrl, kopernicusXpath, GlobalInfo.kopernicusVersion, "Kopernicus");
        //    //GetVersionNumberGitHub(gppUrl, gppXpath, GlobalInfo.gppVersion, "GPP");
        //    //GetVersionNumberGitHub(gppTexturesUrl, gppTexturesXpath, GlobalInfo.gppTexturesVersion, "GPP_Textures");
        //    //GetVersionNumberGitHub(eveUrl, eveXpath, GlobalInfo.eveVersion, "EVE");
        //    GetVersionNumberSpaceDockScatterer(scattererUrl, scattererXpath, GlobalInfo.scattererVersion);
        //    //GetVersionNumberGitHub(doeUrl, doeXpath, GlobalInfo.doeVersion, "DistantObject");

        //    label2.Text = "Finished";
        //}

        //private void GetVersionNumberGitHub(string url, string xpath, string appVersion, string modName)
        //{
        //    HtmlWeb web = new HtmlWeb();

        //    var htmlDoc = web.Load(url);

        //    var h1Anchor = htmlDoc.DocumentNode.SelectNodes(xpath);
        //    var title = h1Anchor.Select(node => node.InnerText);

        //    var item = title.ElementAt(0);

        //    int end = item.LastIndexOf(" ") + 1;
        //    int beginning = 0;
        //    int count = end - beginning;

        //    string versionNum = item.Remove(beginning, count);

        //    if (versionNum != appVersion) DisplayWarning(modName + " version might not be compatable.\n" +
        //        " Download the latest version of GPPInstaller to prevent any errors. ");

        //}

        //private void GetVersionNumberSpaceDockScatterer(string url, string xpath, string appVersion)
        //{
        //    HtmlWeb web = new HtmlWeb();

        //    var htmlDoc = web.Load(url);

        //    var h2Anchor = htmlDoc.DocumentNode.SelectNodes(xpath);
        //    var innerText = h2Anchor.Select(node => node.InnerText);

        //    var item = innerText.ElementAt(0);

        //    int leadingEnd = item.IndexOf(" ");
        //    string versionNum = item.Remove(0, leadingEnd + 1);
        //    int trailingStart = versionNum.IndexOf(" ");
        //    int trailingCount = (versionNum.Length) - trailingStart;

        //    versionNum = versionNum.Remove(trailingStart, trailingCount);


        //    // if (versionNum != appVersion) DisplayWarning("Newer Scatter version is available.
        //}

        private void CheckKopernicus()
        {

        }

        private void CheckGPP(HtmlWeb web)
        {
            
        }

        public void DisplayWarning(string message)
        {
            DisplayYellowWarning();

            progressLabel.Visible = true;
            progressLabel.Text += message + "\n";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
