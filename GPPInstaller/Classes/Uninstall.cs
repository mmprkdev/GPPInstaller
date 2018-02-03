using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GPPInstaller
{
    class Uninstall : IUninstall
    {
        Form1 _form1;

        public void UninstallMod(Form1 form1)
        {
            _form1 = form1;

            foreach (Mod mod in _form1.modList)
            {
                if (mod.ActionToTake == "Uninstall" && mod.State_Installed == true)
                {
                    DeleteModData(mod);
                    mod.State_Installed = false;

                    if (_form1.modDic["GPP"].State_Installed == false)
                    {
                        _form1.modDic["GPP_Textures"].State_Installed = false;
                        _form1.modDic["CloudsLowRes"].State_Installed = false;
                        _form1.modDic["CloudsHighRes"].State_Installed = false;
                    }
                }
            }
        }

        public void DeleteModData(Mod mod)
        {
            DirectoryInfo installSourceDir = new DirectoryInfo(mod.InstallSourcePath);
            if (!installSourceDir.Exists)
            {
                // NOTE: If the user decides to delete the extracted mod
                // dirs in GPPInstaller then this method won't work.
                throw new DirectoryNotFoundException();
            }

            DirectoryInfo[] installSourceDirs = installSourceDir.GetDirectories();
            FileInfo[] installSourceFiles = installSourceDir.GetFiles();

            DirectoryInfo installDestDir = new DirectoryInfo(mod.InstallDestPath);
            if (!installSourceDir.Exists)
            {
                throw new DirectoryNotFoundException();
            }

            DirectoryInfo[] installDestDirs = installDestDir.GetDirectories();
            FileInfo[] installDestFiles = installDestDir.GetFiles();

            foreach (DirectoryInfo sourceDir in installSourceDirs)
            {
                foreach (DirectoryInfo destDir in installDestDirs)
                {
                    if (sourceDir.Name == destDir.Name)
                    {
                        Directory.Delete(destDir.FullName, true);
                    }
                }
            }

            foreach (FileInfo sourceFile in installSourceFiles)
            {
                foreach (FileInfo destFile in installDestFiles)
                {
                    if (sourceFile.Name == destFile.Name)
                    {
                        File.Delete(destFile.FullName);
                    }
                }
            }
        }
    }
}
