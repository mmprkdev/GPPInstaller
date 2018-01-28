using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GPPInstaller
{
    class CheckExe
    {
        Form1 _form1;

        public CheckExe(Form1 form1)
        {
            _form1 = form1;
        }

        public void CheckForExe()
        {
            if (!File.Exists(GlobalInfo.ksp64bitExeFile))
            {
                if (File.Exists(GlobalInfo.ksp32bitExeFile))
                {
                    _form1.Error("32 bit version of KSP was detected. GPP requires a 64 bit \n" + "version of KSP in order to run.");
                }
                else
                {
                    _form1.Error("Did not detect KSP exicutable file...");
                }
            }
        }
    }
}
