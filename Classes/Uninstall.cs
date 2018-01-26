using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GPPInstaller
{
    class Uninstall
    {
        Core _core;

        public Uninstall(Core core)
        {
            _core = core;
        }

        public void UninstallMod()
        {
            if (_core.modList[GlobalInfo.kopericusIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(".\\GameData\\Kopernicus")) Directory.Delete(".\\GameData\\Kopernicus", true);
                if (Directory.Exists(".\\GameData\\ModularFlightIntegrator")) Directory.Delete(".\\GameData\\ModularFlightIntegrator", true);
                File.Delete(".\\GameData\\ModuleManager.2.8.1.dll");
                File.Delete(".\\GameData\\ModuleManagerLicense.md");
                File.Delete(".\\GameData\\ModuleManager.ConfigCache");
                File.Delete(".\\GameData\\ModuleManager.ConfigSHA");
                File.Delete(".\\GameData\\ModuleManager.Physics");
                File.Delete(".\\GameData\\ModuleManager.TechTree");

                _core.modList[GlobalInfo.kopericusIndex].State_Installed = false;
            }

            if (_core.modList[GlobalInfo.gppIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(".\\GameData\\GPP")) Directory.Delete(".\\GameData\\GPP", true);

                _core.modList[GlobalInfo.gppIndex].State_Installed = false;
            }

            if (_core.modList[GlobalInfo.eveIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\EnvironmentalVisualEnhancements")) Directory.Delete(@".\GameData\EnvironmentalVisualEnhancements", true);

                _core.modList[GlobalInfo.eveIndex].State_Installed = false;
            }

            if (_core.modList[GlobalInfo.scattererIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\scatterer")) Directory.Delete(@".\GameData\scatterer", true);

                _core.modList[GlobalInfo.scattererIndex].State_Installed = false;
            }

            if (_core.modList[GlobalInfo.doeIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\DistantObject")) Directory.Delete(@".\GameData\DistantObject", true);

                _core.modList[GlobalInfo.doeIndex].State_Installed = false;
            }

            if (_core.modList[GlobalInfo.cloudsLowResIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);

                _core.modList[GlobalInfo.cloudsLowResIndex].State_Installed = false;
            }

            if (_core.modList[GlobalInfo.cloudsHighResIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\GPP\GPP_Clouds")) Directory.Delete(@".\GameData\GPP\GPP_Clouds", true);

                _core.modList[GlobalInfo.cloudsHighResIndex].State_Installed = false;
            }

            if (_core.modList[GlobalInfo.kerIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\KerbalEngineer")) Directory.Delete(@".\GameData\KerbalEngineer", true);
                File.Delete(@".\GameData\CHANGES.txt");
                File.Delete(@".\GameData\LICENCE.txt");
                File.Delete(@".\GameData\README.htm");

                _core.modList[GlobalInfo.kerIndex].State_Installed = false;
            }

            if (_core.modList[GlobalInfo.kacIndex].ActionToTake == "Uninstall")
            {
                if (Directory.Exists(@".\GameData\TriggerTech")) Directory.Delete(@".\GameData\TriggerTech", true);

                _core.modList[GlobalInfo.kacIndex].State_Installed = false;
            }
        }
    }
}
