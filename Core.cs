using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;
using System.Linq;

// NOTE: Adding a new mod requires you to make changes
// here:
// - Core.SetCheckBoxes()
// - Core.ProcessActionToTake()
// - Core.UninstallMod()
// - Core.AddLinks()
// - Core.AddExtractedPath()
// - Form1.DisableCheckBoxes()
// - Form1.EnableCheckBoxes()

// NOTE: I'm deciding to use static download links instead of scraping them from the 
// web and using the latest mod releasees because using the latest mod releases without
// manually checking for compatibility between all the different mods could cause problems
// and the users save files will most likely become unusable between updates. Better to keep things
// static and maybe update the installer bi-weekly - monthly and prompt the user if they want to update
// the installer to a new version.

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
    class Core
    {
        private readonly Form1 _form1;
        private readonly IModListInit _modlistInit;
        private readonly IModState _modstate;
        private readonly ICheckBoxes _checkboxes;
        private readonly IActionToTake _actionToTake;
        private readonly IInstaller _installer;
        private readonly IUninstall _uninstall;
        private readonly IProgressBarSteps _progressBarSteps;
        private readonly IModVersion _modVersion;

        private CheckBox _coreCheckbox;
        private CheckBox _utilityCheckBox;
        private CheckBox _visualsCheckBox;
        private CheckBox _cloudsLowResCheckBox;
        private CheckBox _cloudsHighResCheckBox;

        public List<Mod> modList = new List<Mod>(); 

        public Core(
            Form1 form1, 
            CheckBox coreCheckBox,
            CheckBox utilityCheckBox,
            CheckBox visualsCheckBox,
            CheckBox cloudsLowResCheckBox,
            CheckBox cloudsHighResCheckBox)
        {
            _modlistInit = new ModListInit(this);
            _modstate = new ModState(this);
            _checkboxes = new CheckBoxes(this);
            _actionToTake = new ActionToTake(this);
            _installer = new Installer(form1, this);
            _uninstall = new Uninstall(this);
            _progressBarSteps = new ProgressBarSteps(this);
            _modVersion = new ModVersion();

            _form1 = form1;
            _coreCheckbox = coreCheckBox;
            _utilityCheckBox = utilityCheckBox;
            _visualsCheckBox = visualsCheckBox;
            _cloudsLowResCheckBox = cloudsLowResCheckBox;
            _cloudsHighResCheckBox = cloudsHighResCheckBox;

            if (!InitCore())
            {
                _form1.Error("Failed to initialize Core...");
                EndOfInstall();
            }
        }

        public bool InitCore()
        {
            _modlistInit.InitModList();
            _modstate.SetModState();
            _checkboxes.SetCheckBoxes(
                _coreCheckbox,
                _utilityCheckBox,
                _visualsCheckBox,
                _cloudsLowResCheckBox,
                _cloudsHighResCheckBox);

            return true;
        }

        // called before install, after applyButton is activated 
        public bool PreInstall()
        {
            _actionToTake.SetActionToTake(
                _coreCheckbox,
                _utilityCheckBox,
                _visualsCheckBox,
                _cloudsLowResCheckBox,
                _cloudsHighResCheckBox);

            return true;
        }

        // called after Install, cancelation, error
        public bool EndOfInstall()
        {
            _modstate.SetModState();
            _checkboxes.SetCheckBoxes(
                _coreCheckbox,
                _utilityCheckBox,
                _visualsCheckBox,
                _cloudsLowResCheckBox,
                _cloudsHighResCheckBox);

            return true;
        }

        public void Install()
        {
            try
            {
                _installer.DownloadMod();
            }
            catch (WebException)
            {
                _form1.Error("Install failed. No internet connection detected...");
                EndOfInstall();
            }
            catch (InvalidDataException)
            {
                _form1.Error("Install failed. Unable to extract archives...");
                EndOfInstall();
            }
            catch (DirectoryNotFoundException)
            {
                _form1.Error("Install failed. Unable to locate extracted mod directory.");
                EndOfInstall();
            }
        }

        public void Uninstall()
        {
            _uninstall.UninstallMod();
        }

        public void CancelInstall()
        {
            _installer.WebClientCancel();
            _installer.ExtractCancel();
            //_installer.CopyCancel();
        }

        public int NumberOfSteps()
        {
            return _progressBarSteps.NumberOfSteps();
        }

        public void InsertVersionFile(Mod mod)
        {
            _modVersion.InsertVersionFile(mod);
        }

        public string GetVersion(Mod mod)
        {
            return _modVersion.GetModVersion(mod);
        }

    }

}
