﻿using System;
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



namespace GPPInstaller
{
    public partial class Form1 : Form
    {
        private Utility util;

        private int numOfFilesInDir = 0;

        public Form1()
        {
            InitializeComponent();

            //if (GlobalInfo.IsConnectedToInternet()) label2.Text = "Connected to internet";
            
            Directory.CreateDirectory(".\\GPPInstaller");

            util = new Utility(this);

            util.InitModList();
            util.RefreshModState();
            util.SetCheckBoxes(checkBox1, checkBox2, checkBox3, checkBox4);

            InitialCheckForErrors();

            //label1.Text = "KSP Version: " + util.GetVersionNumber() + " (" + util.GetEXE() + " bit)";
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
            util.SetCheckBoxes(checkBox1, checkBox2, checkBox3, checkBox4);
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

            applyButton.Enabled = true;

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
            int numSteps = util.NumberOfSteps();

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
            util.RefreshModState();
            util.ResetActionsToTake();

            exitButton.Enabled = false;
            cancelButton.Visible = true;
            pictureBox1.Visible = false;
            applyButton.Enabled = false;
            DisableCheckBoxes();

            util.ProcessActionToTake(checkBox1, checkBox2, checkBox3, checkBox4);
            util.UninstallMod();
            ProgressBar1Init();
            util.DownloadMod();
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
            util.WebClientCancel();
            util.ExtractCancel();
            util.InstallCancel();
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
            DisplayYellowWarning();

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

    }
}
