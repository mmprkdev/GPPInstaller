using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPPInstaller
{
    class ActionToTake
    {
        public void SetActionToTake(
            ref List<Mod> modList,
            CheckBox coreCheckBox,
            CheckBox utilityCheckBox,
            CheckBox visualsCheckBox,
            CheckBox cloudsLowResCheckBox,
            CheckBox cloudsHighResCheckBox)
        {
            foreach (Mod mod in modList)
            {
                if (mod.State_Installed == false)
                {
                    if (mod.ModType == "Core" &&
                        coreCheckBox.Checked == true)
                    {
                        mod.ActionToTake = "Install";
                    }

                    if (mod.ModType == "Utility" &&
                        utilityCheckBox.Checked == true)
                    {
                        mod.ActionToTake = "Install";
                    }

                    if (mod.ModType == "Visuals" &&
                             visualsCheckBox.Checked)
                    {
                        mod.ActionToTake = "Install";
                    }

                    if (mod.ModName == "CloudsLowRes" &&
                        cloudsLowResCheckBox.Checked)
                    {
                        mod.ActionToTake = "Install";
                    }

                    if (mod.ModName == "CloudsHighRes" &&
                        cloudsHighResCheckBox.Checked)
                    {
                        mod.ActionToTake = "Install";
                    }
                }

                if (mod.State_Installed == true)
                {
                    if (mod.ModType == "Core" &&
                        coreCheckBox.Checked == false)
                    {
                        mod.ActionToTake = "Uninstall";
                    }

                    if (mod.ModType == "Utility" &&
                        utilityCheckBox.Checked == false)
                    {
                        mod.ActionToTake = "Uninstall";
                    }

                    if (mod.ModType == "Visuals" &&
                        visualsCheckBox.Checked == false)
                    {
                        mod.ActionToTake = "Uninstall";
                    }

                    if (mod.ModName == "CloudsLowRes" &&
                        cloudsLowResCheckBox.Checked == false)
                    {
                        mod.ActionToTake = "Uninstall";
                    }

                    if (mod.ModName == "CloudsHighRes" &&
                        cloudsHighResCheckBox.Checked == false)
                    {
                        mod.ActionToTake = "Uninstall";
                    }
                }
            }
        }
    }
}
