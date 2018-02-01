using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GPPInstaller
{
    class Version : IVersion
    {
        Form1 _form1;

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

        public string GetKSPVersionNumber(Form1 form1)
        {
            _form1 = form1;

            string target = ".\\readme.txt";

            if (File.Exists(target))
            {
                string[] readmeLines = File.ReadAllLines(target);
                string versionNumberLine = readmeLines[14];

                char[] versionChars = new char[5];
                for (int lineI = 8, charI = 0; lineI <= 12; lineI++, charI++)
                {
                    versionChars[charI] = versionNumberLine[lineI];
                }

                return new string(versionChars);
            }
            else
            {
                _form1.Error("Could not determine KSP version...");
                return "";
            }

        }
    }
}
