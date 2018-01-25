using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GPPInstaller
{
    class Version
    {
        public void InsertVersionFile(Mod mod)
        {
            // {"MAJOR":1,"MINOR":5,"PATCH":99,"BUILD":0}
            if (mod.ModType != "Clouds")
            {
                string content = mod.VersionNumber;
                string path = mod.InstallDestPath + @"\" + mod.InstallDirName + @"\" + mod.ModName + ".ver";
                File.WriteAllText(path, content);
            }
        }

        // NOTE: this is set up to only work with GPP
        // TODO: make it work with all mods
        public string GetModVersion(Mod mod)
        {
            try
            {
                string dirName = mod.ExtractedDirName;
                int offset = dirName.LastIndexOf(".");
                offset = dirName.LastIndexOf(".", offset - 1);
                int leadingEnd = dirName.LastIndexOf(".", offset - 1) + 1;
                string result = dirName.Remove(0, leadingEnd);

                return result;
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}
