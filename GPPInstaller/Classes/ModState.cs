﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace GPPInstaller
{
    class ModState : IModState
    {
        Form1 _form1;

        public void SetModState(Form1 form1)
        {
            _form1 = form1;

            // downloaded, extracted
            foreach (Mod mod in _form1.modList)
            {
                if (Directory.Exists(mod.ExtractedPath + @"\" + mod.ExtractedDirName))
                {
                    mod.State_Downloaded = true;
                    mod.State_Extracted = true;
                }
                else
                {
                    mod.State_Downloaded = false;
                    mod.State_Extracted = false;
                }
            }

            // installed
            foreach (Mod mod in _form1.modList)
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
                else if (Directory.Exists(mod.InstallDestPath + @"\" + mod.InstallDirName))
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
