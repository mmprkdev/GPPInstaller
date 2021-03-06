﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPPInstaller
{
    // Single Responsibility: set form checkboxes based on current mod state
    class CheckBoxes : ICheckBoxes
    {
        Form1 _form1;

        public void SetCheckBoxes(
            Form1 form1,
            CheckBox coreCheckBox,
            CheckBox utilityCheckBox,
            CheckBox visualsCheckBox,
            CheckBox cloudsLowResCheckBox,
            CheckBox cloudsHighResCheckBox)
        {
            _form1 = form1;

            // core
            if (_form1.modDic["Kopernicus"].State_Installed == true &&
                _form1.modDic["GPP"].State_Installed == true &&
                _form1.modDic["GPP_Textures"].State_Installed == true &&
                _form1.modDic["KSCSwitcher"].State_Installed == true)
            {
                coreCheckBox.Checked = true;
            }
            else coreCheckBox.Checked = false;

            // visuals
            if (_form1.modDic["EVE"].State_Installed == true &&
                _form1.modDic["Scatterer"].State_Installed == true &&
                _form1.modDic["SigmaSkyBox"].State_Installed == true)
            {
                visualsCheckBox.Checked = true;
            }
            else visualsCheckBox.Checked = false;

            // cloudsLowRes
            if (_form1.modDic["CloudsLowRes"].State_Installed == true)
            {
                cloudsLowResCheckBox.Checked = true;
            }
            else cloudsLowResCheckBox.Checked = false;

            // cloudsHighRes
            if (_form1.modDic["CloudsHighRes"].State_Installed == true)
            {
                cloudsHighResCheckBox.Checked = true;
            }
            else cloudsHighResCheckBox.Checked = false;

            // utility
            if (_form1.modDic["KerbalEngineer"].State_Installed == true &&
                _form1.modDic["KerbalAlarmClock"].State_Installed == true)
            {
                utilityCheckBox.Checked = true;
            }
            else utilityCheckBox.Checked = false;

            //TODO: Left off here
            foreach (Mod mod in _form1.modList)
            {
                //if (mod.ModType == "Core")
                //{
                //    if (mod.State_Installed == true)
                //    {
                //        coreCheckBox.Checked = true;
                //    }
                //    else coreCheckBox.Checked = false;
                //}

                //if (mod.ModType == "Utility")
                //{
                //    if (mod.State_Installed == true)
                //    {
                //        utilityCheckBox.Checked = true;
                //    }
                //    else utilityCheckBox.Checked = false;
                //}

                //if (mod.ModType == "Visuals")
                //{
                //    if (mod.State_Installed == true)
                //    {
                //        visualsCheckBox.Checked = true;
                //    }
                //    else
                //    {
                //        visualsCheckBox.Checked = false;
                //        cloudsHighResCheckBox.Checked = false;
                //        cloudsLowResCheckBox.Checked = false;
                //    }
                //}

                //if (mod.ModName == "CloudsLowRes")
                //{
                //    if (mod.State_Installed == true)
                //    {
                //        cloudsLowResCheckBox.Checked = true;
                //    }
                //    else cloudsLowResCheckBox.Checked = false;
                //}

                //if (mod.ModName == "CloudsHighRes")
                //{
                //    if (mod.State_Installed == true)
                //    {
                //        cloudsHighResCheckBox.Checked = true;
                //    }
                //    else cloudsHighResCheckBox.Checked = false;
                //}
            }
        }
    }
}
