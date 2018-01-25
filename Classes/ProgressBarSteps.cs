using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPPInstaller
{
    class ProgressBarSteps
    {
        public int NumberOfSteps(ref List<Mod> modList)
        {
            int result = 0;

            foreach (Mod mod in modList)
            {
                // Downloads
                if (mod.State_Downloaded == false &&
                mod.ActionToTake == "Install" &&
                mod.DownloadAddress != "")
                {
                    result++;
                }

                // Extractions
                if (mod.State_Extracted == false &&
                    mod.ActionToTake == "Install" &&
                    mod.ArchiveFileName != "")
                {
                    result++;
                }

                // Installations
                if (mod.State_Installed == false &&
                    mod.ActionToTake == "Install")
                {
                    if (mod.ModType != "Clouds")
                    {
                        result++;
                    }
                    else
                    {
                        result++;
                    }
                }
            }

            return result;
        }
    }
}
