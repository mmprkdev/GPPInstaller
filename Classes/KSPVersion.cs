using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GPPInstaller
{
    class KSPVersion : IKSPVersion
    {
        Form1 _form1;

        public KSPVersion(Form1 form1)
        {
            _form1 = form1;
        }

        public string GetKSPVersionNumber()
        {
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
