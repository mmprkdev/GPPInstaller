using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace GPPInstaller
{
    public partial class Form1 : Form
    {
        Core core;

        public Form1()
        {
            InitializeComponent();

            Directory.CreateDirectory(".\\GPPInstaller");

            core = new Core(this);

            core.SetCheckBoxes(core_checkBox, utility_checkBox, visuals_checkBox, lowResClouds_checkBox, highResClouds_checkBox);

            InitialCheckForErrors();

            Text = "GPP Installer(GPP v" + core.GetGPPVersion() + ") (KSP v" + core.GetKSPVersionNumber() + ")";

            DisableApplyButton();
        }

        private void InitialCheckForErrors()
        {
            // Version
            string versionTarget = @".\readme.txt";

            if (!File.Exists(versionTarget))
            {
                DisplayError("Could not determine KSP version. Make sure the \"readme.txt\" file exists.");
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
                DisplayError("The detected KSP version " + detectedVersionNumber + " is not compatable. Version " + GlobalInfo.compatableKSPVersion + " is required.");
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
                DisplayError("32 bit version of KSP was detected. GPP requires a 64 bit version of KSP in order to run.");
            }
            else
            {
                DisplayError("Could not determine the exe type. Make sure the KSP exicutable file exists.");
            }

        }

        public void RefreshCheckBoxes()
        {
            core.SetCheckBoxes(core_checkBox, utility_checkBox, visuals_checkBox, lowResClouds_checkBox, highResClouds_checkBox);
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void core_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            EnableApplyButton();

            if (!core_checkBox.Checked) visuals_checkBox.Checked = false;
        }

        private void utility_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            EnableApplyButton();
        }

        private void visuals_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            EnableApplyButton();

            if (visuals_checkBox.Checked)
            {
                cloudTextureLabel.Visible = true;
                lowResClouds_checkBox.Visible = true;
                highResClouds_checkBox.Visible = true;

                // Automatically check core_checkBox when visuals_checkBox is checked,
                // because we can only install the visual mods if the core mods
                // are also installed.
                if (!core_checkBox.Checked) core_checkBox.Checked = true;
            }
            else if (visuals_checkBox.Checked == false)
            {
                cloudTextureLabel.Visible = false;

                lowResClouds_checkBox.Checked = false;
                highResClouds_checkBox.Checked = false;

                lowResClouds_checkBox.Visible = false;
                highResClouds_checkBox.Visible = false;
            }
        }

        private void lowResClouds_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            EnableApplyButton();

            if (lowResClouds_checkBox.Checked) highResClouds_checkBox.Checked = false;

        }

        private void highResClouds_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            EnableApplyButton();

            applyButton.Enabled = true;

            if (highResClouds_checkBox.Checked) lowResClouds_checkBox.Checked = false;
        }

        private void DisableCheckBoxes()
        {
            core_checkBox.Enabled = false;
            utility_checkBox.Enabled = false;
            visuals_checkBox.Enabled = false;
            lowResClouds_checkBox.Enabled = false;
            highResClouds_checkBox.Enabled = false;
        }

        public void EnableCheckBoxes()
        {
            core_checkBox.Enabled = true;
            utility_checkBox.Enabled = true;
            visuals_checkBox.Enabled = true;
            lowResClouds_checkBox.Enabled = true;
            highResClouds_checkBox.Enabled = true;
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

            core.ProcessActionToTake(core_checkBox, utility_checkBox, visuals_checkBox, lowResClouds_checkBox, highResClouds_checkBox);
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
            progressLabel.Text += "Error: " + message + "\n";

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

        public void DisplayWarning(string message)
        {
            DisplayYellowWarning();

            progressLabel.Visible = true;
            progressLabel.Text += message + "\n";
        }

        public void DisableApplyButton()
        {
            applyButton.Enabled = false;
        }

        
    }
}
