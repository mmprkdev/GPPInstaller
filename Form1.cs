using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Net;

// TODO: consider instantiating core in the Program class

// TODO: have a list of all working mod downloads hosted on github in a json or txt file

// TODO: consider removing all private keywords

// TODO: insert using keywords to properly dispose
// of streams. (are there any streams?)

// TODO: add ksc swither to the core install pack

// TODO: add Pood's Milky Way Skybox

// TODO: get rid of distant object

// TODO: release GPPInstaller on GitHub

// TODO: Create an updater. Detect whether or not a new version of the
// the instller is available to download. Inform the user that installing
// a new version will require them to re-download mods and potentially break
// existing saved games. 

// TODO (maybe): make clouds an optional mod type along with kscSwitcher

namespace GPPInstaller
{
    public partial class Form1 : Form
    {
        private readonly IModListInit _modlistInit;
        private readonly IModState _modState;
        private readonly ICheckBoxes _checkboxes;
        private readonly IActionToTake _actionToTake;
        private readonly IInstaller _installer;
        private readonly IUninstall _uninstall;
        private readonly IProgressBarSteps _progressBarSteps;
        private readonly IModVersion _modVersion;
        private readonly ICheckExe _checkExe;
        private readonly IKSPVersion _kspVersion;
        private readonly IConverter _converter;

        public List<Mod> modList = new List<Mod>();

        public Form1()
        {
            InitializeComponent();

            Directory.CreateDirectory(@".\GPPInstaller");
            Directory.CreateDirectory(@".\GameData");

            // TODO: don't pass through form1. just pass through
            // modlist when the methods are called. Instantiate these
            // classes in the form1 instantiation.
            // NOTE: actually, i need form1 for Errors, maybe
            // do errors another way???
            // Maybe have these be child classes insted of interfaces???
            _modlistInit = new ModListInit(this);
            _modState = new ModState(this);
            _checkboxes = new CheckBoxes(this);
            _actionToTake = new ActionToTake(this);
            _installer = new Installer(this);
            _uninstall = new Uninstall(this);
            _progressBarSteps = new ProgressBarSteps(this);
            _modVersion = new ModVersion();
            _kspVersion = new KSPVersion(this);
            _checkExe = new CheckExe(this);
            _converter = new Converter();

            if (!OnStart())
            {
                Error("Failed to initialize Core...");
                EndOfInstall();
            }
        }

        public bool OnStart()
        {
            _modlistInit.InitModList();
            _modState.SetModState();
            _checkboxes.SetCheckBoxes(
                core_checkBox,
                utility_checkBox,
                visuals_checkBox,
                lowResClouds_checkBox,
                highResClouds_checkBox);

            // TODO: Left off here
            string json = _converter.SerializeModListToJson(modList);
            string targetFilePath = @".\GPPInstaller\modList.json";
            try
            {
                using (FileStream stream = File.Open(targetFilePath, FileMode.Create, FileAccess.Write))
                using (var sw = new StreamWriter(stream))
                {
                    sw.Write(json);
                    sw.Close();
                }
            }
            catch (UnauthorizedAccessException)
            {
                Error("UnauthorizedAccessException");
            }

            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "GPP Installer (GPP v" + _modVersion.GetModVersion(modList[GlobalInfo.gppIndex]) + ") (KSP v" + _kspVersion.GetKSPVersionNumber() + ")";
            applyButton.Enabled = false;
        }

        public void PreInstall()
        {
            _actionToTake.SetActionToTake(
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
                _installer.DownloadMod();
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
            _modState.SetModState();
            _checkboxes.SetCheckBoxes(
                core_checkBox,
                utility_checkBox,
                visuals_checkBox,
                lowResClouds_checkBox,
                highResClouds_checkBox);
        }

        public void InsertVersionFile(Mod mod)
        {
            _modVersion.InsertVersionFile(mod);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void core_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            applyButton.Enabled = true;

            if (!core_checkBox.Checked) visuals_checkBox.Checked = false;
        }

        private void utility_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            applyButton.Enabled = true;
        }

        private void visuals_checkBox_CheckedChanged(object sender, EventArgs e)
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

        private void lowResClouds_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            applyButton.Enabled = true;

            if (lowResClouds_checkBox.Checked) highResClouds_checkBox.Checked = false;
        }

        private void highResClouds_checkBox_CheckedChanged(object sender, EventArgs e)
        {
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
            int numSteps = _progressBarSteps.NumberOfSteps();

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
            PreInstall();

            exitButton.Enabled = false;
            cancelButton.Visible = true;
            pictureBox1.Visible = false;
            applyButton.Enabled = false;
            DisableCheckBoxes();
            ProgressBar1Init();

            _uninstall.UninstallMod();
            Install();
            
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
        private void kopernicusModNameLabel_MouseDown(object sender, MouseEventArgs e)
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

        private void gppModNameLabel_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start("https://forum.kerbalspaceprogram.com/index.php?/topic/152136-ksp-131-galileos-planet-pack-v160-16-jan-2018/");
            }
            catch
            {
            }
        }

        private void kerModNameLabel_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start("https://forum.kerbalspaceprogram.com/index.php?/topic/17833-130-kerbal-engineer-redux-1130-2017-05-28/");
            }
            catch
            {
            }
        }

        private void kacModNameLabel_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start("https://forum.kerbalspaceprogram.com/index.php?/topic/22809-13x-kerbal-alarm-clock-v3850-may-30/");
            }
            catch
            {
            }
        }

        private void scattererModNameLabel_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start("https://forum.kerbalspaceprogram.com/index.php?/topic/103963-wip12213-scatterer-atmospheric-scattering-v00320-0320b-06072017-water-refraction/");
            }
            catch
            {
            }
        }

        private void eveModNameLabel_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start("https://forum.kerbalspaceprogram.com/index.php?/topic/149733-13-122-environmentalvisualenhancements-122-1/");
            }
            catch
            {
            }
        }

        private void doeModNameLabel_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start("https://forum.kerbalspaceprogram.com/index.php?/topic/89214-131-distant-object-enhancement-bis-v191-8-july-2017/");
            }
            catch
            {
            }
        }

        private void kopernicusModNameLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void kopernicusModNameLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void gppModNameLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void gppModNameLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void kerModNameLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void kerModNameLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void kacModNameLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void kacModNameLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void scattererModNameLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void scattererModNameLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void eveModNameLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void eveModNameLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void doeModNameLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void doeModNameLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
    }

}
