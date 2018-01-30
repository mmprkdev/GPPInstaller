using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.ComponentModel;

namespace GPPInstaller
{
    public interface IInstaller
    {
        void DownloadMod(Form1 form1);
        //void webclient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e);
        //void webclient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e);
        void WebClientCancel();
        void ExtractMod(Form1 form1);
        void workerExtract_DoWork(object sender, DoWorkEventArgs e);
        void workerExtract_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e);
        void ExtractCancel();
        void CopyMod(Form1 form1);
        void workerCopy_DoWork(object sender, DoWorkEventArgs e);
        void workerCopy_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e);
        void CopyCancel();
        void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs);
    }
}
