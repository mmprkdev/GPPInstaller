using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GPPInstaller
{
    class ModState
    {
        Core _core;

        public ModState(Core core)
        {
            _core = core;
        }

        public void SetModState()
        {
            // downloaded
            foreach (Mod mod in _core.modList)
            {
                if (mod.ModName == "CloudsLowRes" &&
                    _core.modList[GlobalInfo.gppIndex].State_Downloaded == true)
                {
                    mod.State_Downloaded = true;
                }
                else mod.State_Downloaded = false;

                if (mod.ModName == "CloudsHighRes" &&
                    _core.modList[GlobalInfo.gppIndex].State_Downloaded == true)
                {
                    mod.State_Downloaded = true;
                }
                else mod.State_Downloaded = false;

                if (File.Exists(mod.ArchiveFilePath + @"\" + mod.ArchiveFileName))
                {
                    mod.State_Downloaded = true;
                }
                else
                {
                    mod.State_Downloaded = false;
                }
            }

            // extracted
            foreach (Mod mod in _core.modList)
            {
                if (Directory.Exists(mod.ExtractedPath + @"\" + mod.ExtractedDirName))
                {
                    mod.State_Extracted = true;
                }
                else
                {
                    mod.State_Extracted = false;
                }
            }

            // installed
            foreach (Mod mod in _core.modList)
            {
                if (mod.ModType == "Clouds")
                {
                    if (mod.ModName == "CloudsLowRes")
                    {
                        if (File.Exists(@".\GameData\GPP\GPP_Clouds\Configs\GPPClouds_LowRes.cfg"))
                        {
                            mod.State_Installed = true;
                        }
                        else mod.State_Installed = false;
                    }

                    if (mod.ModName == "CloudsHighRes")
                    {
                        if (File.Exists(@".\GameData\GPP\GPP_Clouds\Configs\GPPClouds_HighRes.cfg"))
                        {
                            mod.State_Installed = true;
                        }
                        else mod.State_Installed = false;
                    }
                }
                else
                {
                    if (Directory.Exists(mod.InstallDestPath + @"\" + mod.InstallDirName))
                    {
                        mod.State_Installed = true;
                    }
                    else
                    {
                        mod.State_Installed = false;
                    }
                }
            }
        }
    }
}
