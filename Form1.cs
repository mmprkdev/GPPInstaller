using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace GPPInstaller
{
    public partial class Form1 : Form
    {
        private readonly CheckExe _checkExe;
        private readonly KSPVersion _kspVersion;
        private readonly Core _core;

        // Single responsibility: the user
        // What we need to send to Core through the constructor:
        // - Form1 
        // - all the checkboxes
        public Form1()
        {
            InitializeComponent();
            _kspVersion = new KSPVersion(this);
            _checkExe = new CheckExe(this);
            _core = new Core(
                this,
                core_checkBox,
                utility_checkBox,
                visuals_checkBox,
                lowResClouds_checkBox,
                highResClouds_checkBox);

            Directory.CreateDirectory(@".\GPPInstaller");
            Directory.CreateDirectory(@".\GameData");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "GPP Installer (GPP v" + _core.GetVersion(_core.modList[GlobalInfo.gppIndex]) + ") (KSP v" + _kspVersion.GetKSPVersionNumber() + ")";
            DisableApplyButton();

            if (UpdateAvailable())
            {
                Updater updater = new Updater();
                updater.Show(this);
            }
        }

        // TODO: implement updater (???)
        private bool UpdateAvailable()
        {
            return false;
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
            int numSteps = _core.NumberOfSteps();

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
            _core.PreInstall();

            exitButton.Enabled = false;
            cancelButton.Visible = true;
            pictureBox1.Visible = false;
            applyButton.Enabled = false;
            DisableCheckBoxes();
            ProgressBar1Init();

            _core.Uninstall();
            _core.Install();
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
            _core.CancelInstall();
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
            RemoveCancelButton();
            DisplayGreenCheck();
            EnableExitButton();
            EnableCheckBoxes();
            ProgressLabelUpdate("All changes applied successfully.");
            DisableApplyButton();
        }

        public void InstallCanceled()
        {
            DisplayRedCheck();
            RemoveCancelButton();
            EnableExitButton();
            RemoveProgressBar();
            EnableApplyButton();
            EnableCheckBoxes();
        }

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
