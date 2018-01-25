using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;
using HtmlAgilityPack;
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

// DOING: Refactor the entire application in another project
// to incorperate SOLID principals. Don't go overboard, just
// get things cleaned up.
//
// Start with coupled code, then decouple it using interfaces.

// TODO: don't pass a ref to modList through, just reference core.modList. Maybe check
// to see if there is a difference in performance.

// TODO: rework UninstallMod() to allow for mods to be uninstalled dynamically

// TODO: output the mod name to progress label when extracting and copying

// TODO: consider not passing in form1 to the download/extract/install process classes

// TODO: Create a new brach on github for the refactored project.

// TODO: consider replacing the background workers with async Tasks

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
    // Single responsibility: orchestrator of the application
    class Core
    {
        private readonly Form1 _form1;
        private readonly ModListInit _modlistInit;
        private readonly ModState _modstate;
        private readonly CheckBoxes _checkboxes;
        private readonly ActionToTake _actionToTake;
        private readonly Installer _installer;
        private readonly Uninstall _uninstall;
        private readonly ProgressBarSteps _progressBarSteps;
        private readonly Version _version;

        private CheckBox _coreCheckbox;
        private CheckBox _utilityCheckBox;
        private CheckBox _visualsCheckBox;
        private CheckBox _cloudsLowResCheckBox;
        private CheckBox _cloudsHighResCheckBox;

        // TODO: figure out if you should be passing through
        // the web client or the background workers
        //WebClient webclient = new WebClient();
        //BackgroundWorker workeExtract = new BackgroundWorker();
        //BackgroundWorker workerInstall = new BackgroundWorker();
        public List<Mod> modList = new List<Mod>(); 

        public Core(
            Form1 form1, 
            CheckBox coreCheckBox,
            CheckBox utilityCheckBox,
            CheckBox visualsCheckBox,
            CheckBox cloudsLowResCheckBox,
            CheckBox cloudsHighResCheckBox)
        {
            _modlistInit = new ModListInit();
            _modstate = new ModState();
            _checkboxes = new CheckBoxes();
            _actionToTake = new ActionToTake();
            _installer = new Installer(form1, this);
            _uninstall = new Uninstall();
            _progressBarSteps = new ProgressBarSteps();
            _version = new Version();

            _form1 = form1;
            _coreCheckbox = coreCheckBox;
            _utilityCheckBox = utilityCheckBox;
            _visualsCheckBox = visualsCheckBox;
            _cloudsLowResCheckBox = cloudsLowResCheckBox;
            _cloudsHighResCheckBox = cloudsHighResCheckBox;

            if (!InitCore())
            {
                _form1.ErrorGeneral("Failed to initialize Core...");
            }
        }

        public bool InitCore()
        {
            _modlistInit.InitModList(ref modList);
            _modstate.SetModState(ref modList);
            _checkboxes.SetCheckBoxes(
                ref modList,
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
                ref modList,
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
            _modstate.SetModState(ref modList);
            _checkboxes.SetCheckBoxes(
                ref modList,
                _coreCheckbox,
                _utilityCheckBox,
                _visualsCheckBox,
                _cloudsLowResCheckBox,
                _cloudsHighResCheckBox);

            return true;
        }

        public void InstallSuccess()
        {
            _form1.RemoveProgressBar();
            _form1.RemoveCancelButton();
            _form1.DisplayGreenCheck();
            _form1.EnableExitButton();
            _form1.EnableCheckBoxes();
            _form1.ProgressLabelUpdate("All changes applied successfully.");
            _form1.DisableApplyButton();
        }

        public void InstallCanceled()
        {
            _form1.ProgressLabelUpdate("Installation canceled.");
            _form1.DisplayRedCheck();
            _form1.RemoveCancelButton();
            _form1.EnableExitButton();
            _form1.RemoveProgressBar();
            _form1.EnableApplyButton();
            _form1.EnableCheckBoxes();
        }

        public bool Install()
        {
            _installer.DownloadMod();

            return true;
        }

        public bool Uninstall()
        {
            _uninstall.UninstallMod(ref modList);

            return true;
        }

        // TODO: Do something with this(?)
        public void ResetActionsToTake()
        {
            foreach (Mod mod in modList)
            {
                mod.ActionToTake = "";
            }
        }
        
        public void CancelInstall()
        {
            _installer.WebClientCancel();
            _installer.ExtractCancel();
            //_installer.CopyCancel();
        }

        public int NumberOfSteps()
        {
            return _progressBarSteps.NumberOfSteps(ref modList);
        }

        public void InsertVersionFile(Mod mod)
        {
            _version.InsertVersionFile(mod);
        }

        public string GetVersion(Mod mod)
        {
            return _version.GetModVersion(mod);
        }
    }

}
