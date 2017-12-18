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

        // TODO: Maybe go back to this method of processing selected options
        Dictionary<string, bool> currentlyInstalledOptions = new Dictionary<string, bool>();
        Dictionary<string, bool> newlySelectedOptions = new Dictionary<string, bool>();

        public Form1()
        {
            InitializeComponent();

            Directory.CreateDirectory(".\\GPPInstaller");

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            CurrentlyInstalledOptionsInit();
            SelectedOptionsInit();
            CurrentlyInstalledOptionsCheck();

            InitCheckBoxes();

            util = new Utility(this);

            util.InitModList();

            label1.Text = "KSP Version: " + util.GetVersionNumber() + " (" + util.GetEXE() + ")";

            

        }



        //private void button1_Click(object sender, EventArgs e)
        //{
        //    CurrentlyInstalledOptionsCheck();

        //    if (checkBox1.Checked)
        //    {
        //        newlySelectedOptions["Core"] = true;
        //    }

        //    if (checkBox2.Checked)
        //    {   
        //        newlySelectedOptions["Visuals"] = true;
        //
        //        if (checkBox3.Checked)
        //        {
        //            newlySelectedOptions["CloudsLowRes"] = true;
        //        }
        //        else if (checkBox4.Checked)
        //        {
        //            newlySelectedOptions["CloudsHighRes"] = true;
        //        }
        //    }

        //    util.ProcessOptions(currentlyInstalledOptions, newlySelectedOptions);

        //}

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

            if (!checkBox1.Checked) checkBox2.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;

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
                checkBox3.Visible = false;
                checkBox4.Visible = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;

            if (checkBox3.Checked) checkBox4.Checked = false;

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;

            if (checkBox4.Checked) checkBox3.Checked = false;
        }

        public void ProgressBar1Init(int modCount)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = modCount * 3;
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

        private void SelectedOptionsInit()
        {
            newlySelectedOptions.Add("Core", false);
            newlySelectedOptions.Add("Visuals", false);
            newlySelectedOptions.Add("CloudsLowRes", false);
            newlySelectedOptions.Add("CloudsHighRes", false);
        }

        private void CurrentlyInstalledOptionsInit()
        {
            currentlyInstalledOptions.Add("Core", false);
            currentlyInstalledOptions.Add("Visuals", false);
            currentlyInstalledOptions.Add("CloudsLowRes", false);
            currentlyInstalledOptions.Add("CloudsHighRes", false);
        }

        private void CurrentlyInstalledOptionsCheck()
        {
            string kopernicusDir = @".\GameData\Kopernicus";
            string GPPDir = @".\GameData\GPP";
            string modularFIDir = @".\GameData\ModularFlightIntegrator";
            string modManagerFile = @".\GameData\ModuleManager.2.8.1.dll";

            string eveDir = @".\GameData\EnvironmentalVisualEnhancements";
            string scattererDir = @".\GameData\scatterer";
            string distantOEDir = @".\GameData\DistantObject";

            string cloudsLowResConfig = @".\GameData\GPP\GPP_Clouds\Configs\GPPClouds_LowRes.cfg";
            string cloudsHighResConfig = @".\GameData\GPP\GPP_Clouds\Configs\GPPClouds_HighRes.cfg";

            if (Directory.Exists(kopernicusDir) &&
                Directory.Exists(GPPDir) &&
                Directory.Exists(modularFIDir) &&
                File.Exists(modManagerFile))
            {
                currentlyInstalledOptions["Core"] = true;
            }
            else currentlyInstalledOptions["Core"] = false;

            if (Directory.Exists(eveDir) &&
                Directory.Exists(scattererDir) &&
                Directory.Exists(distantOEDir))
            {
                currentlyInstalledOptions["Visuals"] = true;
            }
            else currentlyInstalledOptions["Visuals"] = false;

            if (File.Exists(cloudsLowResConfig) && 
                currentlyInstalledOptions["Visuals"] == true)
            {
                currentlyInstalledOptions["CloudsLowRes"] = true;
            }
            else currentlyInstalledOptions["CloudsLowRes"] = false;

            if (File.Exists(cloudsHighResConfig) &&
                currentlyInstalledOptions["Visuals"] == true)
            {
                currentlyInstalledOptions["CloudsHighRes"] = true;
            }
            else currentlyInstalledOptions["CloudsHighRes"] = false;
        }

        private void InitCheckBoxes()
        {
            if (currentlyInstalledOptions["Core"] == true) checkBox1.Checked = true;
            if (currentlyInstalledOptions["Visuals"] == true) checkBox2.Checked = true;
            if (currentlyInstalledOptions["CloudsLowRes"] == true) checkBox3.Checked = true;
            if (currentlyInstalledOptions["CloudsHighRes"] == true) checkBox4.Checked = true;

            button1.Enabled = false;
        }

        private void NewlySelectedOptionsCheck()
        {
            if (checkBox1.Checked)
            {
                newlySelectedOptions["Core"] = true;
            }

            if (checkBox2.Checked)
            {
                newlySelectedOptions["Visuals"] = true;

                if (checkBox3.Checked)
                {
                    newlySelectedOptions["CloudsLowRes"] = true;
                }
                else if (checkBox4.Checked)
                {
                    newlySelectedOptions["CloudsHighRes"] = true;
                }
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == false)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                backgroundWorker1.CancelAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            CurrentlyInstalledOptionsCheck();
            NewlySelectedOptionsCheck();

            Dictionary<string, bool> processedOptions = util.ProcessOptions(currentlyInstalledOptions, newlySelectedOptions);

            util.BuildInstallPack(processedOptions);

            util.Uninstall(processedOptions);

            util.DownloadFiles();

        }
    }
}
