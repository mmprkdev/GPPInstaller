using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPPInstaller
{
    public interface IUninstall
    {
        void UninstallMod(Form1 form1);
        void DeleteModData(Mod mod);
    }
}
