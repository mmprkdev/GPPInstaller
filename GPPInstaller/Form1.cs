using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Net;

// TODO: create a demo version that runs without KSP that can be showed off
// to employers, and a full working version. 

namespace GPPInstaller
{
    public partial class Form1 : Form
    {
        readonly IModState _modState;
        readonly IModListInit _modListInit;
        readonly ICheckBoxes _checkBoxes;
        readonly IActionToTake _actionToTake;
        readonly IInstaller _installer;
        readonly IUninstall _uninstall;
        readonly IProgressBarSteps _progressBarSteps;
        readonly IVersion _version;
        readonly ICheckExe _checkExe;
        readonly IConverter _converter;
        readonly IDataStorage _dataStorage;
        readonly IDownloader _downloader;

        public List<Mod> modList = new List<Mod>();
        public Dictionary<string, Mod> modDic = new Dictionary<string, Mod>();

        public Form1(
            IModState modState,
            IModListInit modListInit,
            ICheckBoxes checkBoxes,
            IActionToTake actionToTake,
            IInstaller installer,
            IUninstall uninstall,
            IProgressBarSteps progressBarSteps,
            IVersion version,
            ICheckExe checkExe,
            IConverter converter,
            IDataStorage dataStorage,
            IDownloader downloader)
        {
            InitializeComponent();

            _modState = modState;
            _modListInit = modListInit;
            _checkBoxes = checkBoxes;
            _actionToTake = actionToTake;
            _installer = installer;
            _uninstall = uninstall;
            _progressBarSteps = progressBarSteps;
            _version = version;
            _checkExe = checkExe;
            _converter = converter;
            _dataStorage = dataStorage;
            _downloader = downloader;
            
            OnInit();
        }

        public bool OnInit()
        {
            Directory.CreateDirectory(@".\GPPInstaller");
            Directory.CreateDirectory(@".\GameData");
            _modListInit.InitModList(this);
            InitModDic(ref modDic);
            _modState.SetModState(this);
            _checkBoxes.SetCheckBoxes(
                this,
                core_checkBox,
                utility_checkBox,
                visuals_checkBox,
                lowResClouds_checkBox,
                highResClouds_checkBox);

            return true;
        }

        void Form1_Load(object sender, EventArgs e)
        {
            Text = "GPP Installer (GPP v" + _version.GetModVersion(modList[1]) + ") (KSP v" + _version.GetKSPVersionNumber(this) + ")";
            applyButton.Enabled = false;
        }

        public void InitModDic(ref Dictionary<string, Mod> modDic)
        {
            for (int i = 0; i < modList.Count; i++)
            {
                modDic.Add(modList[i].ModName, modList[i]);
            }
        }

        public void PreInstall()
        {
            _modState.SetModState(this);
            _actionToTake.SetActionToTake(
                this,
                core_checkBox,
                utility_checkBox,
                visuals_checkBox,
                lowResClouds_checkBox,
                highResClouds_checkBox);
        }

        public void Install()
        {
            try
            {
                _installer.DownloadMod(this);
            }
            catch (WebException)
            {
                Error("Install failed. No internet connection detected...");
                EndOfInstall();
            }
            catch (InvalidDataException)
            {
                Error("Install failed. Unable to extract archives...");
                EndOfInstall();
            }
            catch (DirectoryNotFoundException)
            {
                Error("Install failed. Unable to locate extracted mod directory.");
                EndOfInstall();
            }
        }

        public void EndOfInstall()
        {
            _modState.SetModState(this);
            _checkBoxes.SetCheckBoxes(
                this,
                core_checkBox,
                utility_checkBox,
                visuals_checkBox,
                lowResClouds_checkBox,
                highResClouds_checkBox);
        }

        public void InsertVersionFile(Mod mod)
        {
            _version.InsertVersionFile(mod);
        }

        void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void core_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            applyButton.Enabled = true;

            if (!core_checkBox.Checked) visuals_checkBox.Checked = false;
        }

        void utility_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            applyButton.Enabled = true;
        }

        void visuals_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            applyButton.Enabled = true;

            if (visuals_checkBox.Checked)
            {
                cloudTextureLabel.Visible = true;
                lowResClouds_checkBox.Visible = true;
                highResClouds_checkBox.Visible = true;

                // Automatically check core_checkBox when visuals_checkBox is checked,
                // because the visual mods depend on the core mods
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

        void lowResClouds_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            applyButton.Enabled = true;

            if (lowResClouds_checkBox.Checked) highResClouds_checkBox.Checked = false;
        }

        void highResClouds_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            applyButton.Enabled = true;

            if (highResClouds_checkBox.Checked) lowResClouds_checkBox.Checked = false;
        }

        void DisableCheckBoxes()
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
            int numSteps = _progressBarSteps.NumberOfSteps(this);

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

        void applyButton_Click(object sender, EventArgs e)
        {
            PreInstall();

            exitButton.Enabled = false;
            cancelButton.Visible = true;
            pictureBox1.Visible = false;
            applyButton.Enabled = false;
            DisableCheckBoxes();
            ProgressBar1Init();

            _uninstall.UninstallMod(this);
            Install();
            
        }

        void exitButton_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = @".\KSP_x64.exe";
            process.Start();
            Application.Exit();
        }

        void cancelButton_Click(object sender, EventArgs e)
        {
            {
                _installer.WebClientCancel();
                _installer.ExtractCancel();
                //_installer.CopyCancel();
            }
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

        public void DisplayYellowWarning()
        {
            pictureBox1.Image = Properties.Resources.warning2;
            pictureBox1.Refresh();
            pictureBox1.Visible = true;
        }

        void restartButton_Click(object sender, EventArgs e)
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

        public void Error(string message)
        {
            DisplayRedCheck();
            progressBar1.Visible = false;
            progressLabel.Visible = true;
            progressLabel.Text += "Error: " + message + "\n";
            DisableCheckBoxes();
            restartButton.Visible = true;
            applyButton.Visible = false;
        }

        public void InstallSuccess()
        {
            RemoveProgressBar();
            cancelButton.Visible = false;
            DisplayGreenCheck();
            exitButton.Enabled = true;
            EnableCheckBoxes();
            ProgressLabelUpdate("All changes applied successfully.");
            applyButton.Enabled = false;
        }

        public void InstallCanceled()
        {
            DisplayRedCheck();
            cancelButton.Visible = false;
            exitButton.Enabled = true;
            RemoveProgressBar();
            applyButton.Enabled = true;
            EnableCheckBoxes();
        }

        // NOTE: these links might be subject to change. might want to fetch these from the romote sight as well.
        void kopernicusModNameLabel_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start("https://forum.kerbalspaceprogram.com/index.php?/topic/140580-131-kopernicus-release-3-nov-30/");
            }
            catch
            {
                // Error here?
            }
        }

        void gppModNameLabel_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start("https://forum.kerbalspaceprogram.com/index.php?/topic/152136-ksp-131-galileos-planet-pack-v160-16-jan-2018/");
            }
            catch
            {
            }
        }

        void kerModNameLabel_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start("https://forum.kerbalspaceprogram.com/index.php?/topic/17833-130-kerbal-engineer-redux-1130-2017-05-28/");
            }
            catch
            {
            }
        }

        void kacModNameLabel_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start("https://forum.kerbalspaceprogram.com/index.php?/topic/22809-13x-kerbal-alarm-clock-v3850-may-30/");
            }
            catch
            {
            }
        }

        void scattererModNameLabel_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start("https://forum.kerbalspaceprogram.com/index.php?/topic/103963-wip12213-scatterer-atmospheric-scattering-v00320-0320b-06072017-water-refraction/");
            }
            catch
            {
            }
        }

        void eveModNameLabel_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start("https://forum.kerbalspaceprogram.com/index.php?/topic/149733-13-122-environmentalvisualenhancements-122-1/");
            }
            catch
            {
            }
        }

        void doeModNameLabel_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start("https://forum.kerbalspaceprogram.com/index.php?/topic/89214-131-distant-object-enhancement-bis-v191-8-july-2017/");
            }
            catch
            {
            }
        }

        void kopernicusModNameLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        void kopernicusModNameLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        void gppModNameLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        void gppModNameLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        void kerModNameLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        void kerModNameLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        void kacModNameLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        void kacModNameLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        void scattererModNameLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        void scattererModNameLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        void eveModNameLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        void eveModNameLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        void doeModNameLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        void doeModNameLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

    }

}
