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

// TODO: Make a good error checking system.

namespace GPPInstaller
{
    public partial class Form1 : Form
    {
        Utility util;
        string[] installedModPacks = new string[2];
        Dictionary<string, string> selectedModPacks = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();

            util = new Utility(this);

            label1.Text = "KSP Version: " + util.GetVersionNumber() + " (" + util.GetEXE() + ")";

            Directory.CreateDirectory(".\\GPPInstaller");

            installedModPacks = util.CurrentInstalledModPacks();
            CheckInstalledMods(installedModPacks);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = false;

            if (checkBox1.Checked)
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 9; // 3 mods * 3 steps
                progressBar1.Value = 0;
                progressBar1.Step = 1;
                progressBar1.Visible = true;

                util.BuildModPack("Core");

                int index = 0;
                util.Download(index);
            }
            else util.Uninstall("Core");



            //if (checkBox2.Checked)
            //{
            //    util.DownloadAndInstall("Core");
            //}
            //else util.Uninstall("");

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
            button1.Enabled = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void CheckInstalledMods(string[] installedModPacks)
        {
            if (installedModPacks[0] == "Core")
            {
                checkBox1.Checked = true;
            }
            
            if (installedModPacks[1] == "VisualEnhacements")
            {
                checkBox2.Checked = true;
            }

            button1.Enabled = false;
        }

        public void ProgressBarStep()
        {
            progressBar1.PerformStep();
        }

        public void ProgressLabelUpdate(string Message)
        {
            progressLabel.Visible = true;
            progressLabel.Text = Message;
        }
    }
}
