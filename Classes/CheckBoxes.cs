﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPPInstaller
{
    // Single Responsibility: set form checkboxes based on current mod state
    class CheckBoxes
    {
        Core _core;

        public CheckBoxes(Core core)
        {
            _core = core;
        }

        public void SetCheckBoxes(
            CheckBox coreCheckBox,
            CheckBox utilityCheckBox,
            CheckBox visualsCheckBox,
            CheckBox cloudsLowResCheckBox,
            CheckBox cloudsHighResCheckBox)
        {
            foreach (Mod mod in _core.modList)
            {
                if (mod.ModType == "Core")
                {
                    if (mod.State_Installed == true)
                    {
                        coreCheckBox.Checked = true;
                    }
                    else coreCheckBox.Checked = false;
                }

                if (mod.ModType == "Utility")
                {
                    if (mod.State_Installed == true)
                    {
                        utilityCheckBox.Checked = true;
                    }
                    else utilityCheckBox.Checked = false;
                }

                if (mod.ModType == "Visuals")
                {
                    if (mod.State_Installed == true)
                    {
                        visualsCheckBox.Checked = true;
                    }
                    else
                    {
                        visualsCheckBox.Checked = false;
                        cloudsHighResCheckBox.Checked = false;
                        cloudsLowResCheckBox.Checked = false;
                    }
                }

                if (mod.ModName == "CloudsLowRes")
                {
                    if (mod.State_Installed == true)
                    {
                        cloudsLowResCheckBox.Checked = true;
                    }
                    else cloudsLowResCheckBox.Checked = false;
                }

                if (mod.ModName == "CloudsHighRes")
                {
                    if (mod.State_Installed == true)
                    {
                        cloudsHighResCheckBox.Checked = true;
                    }
                    else cloudsHighResCheckBox.Checked = false;
                }
            }
        }
    }
}
