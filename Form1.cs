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
                progressLabel.Text = "Downloading...";
                ProgressBar1Init();
                util.BuildModPack("Core");
                util.DownloadFiles();
            }
            else util.Uninstall("Core");

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

        private void ProgressBar1Init()
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 3;
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            progressBar1.Visible = true;
        }

        public void ProgressBar1Step()
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new MethodInvoker(delegate
                {
                    progressBar1.PerformStep();
                }));
            }
            else
            {
                progressBar1.PerformStep();
            }
        }

        public void ProgressLabelUpdate(string Message)
        {
            if (progressLabel.InvokeRequired)
            {
                progressLabel.Invoke(new MethodInvoker(delegate
                {
                    progressLabel.Visible = true;
                    progressLabel.Text = Message;
                }));
            }
            else
            {
                progressLabel.Visible = true;
                progressLabel.Text = Message;
            }
            
        }
    }
}
