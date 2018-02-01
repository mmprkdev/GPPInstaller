using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPPInstaller
{
    public interface IDownloader
    {
        void DownloadAsync(string downloadLink, string downloadDest);
        void Download(string downloadLink, string downloadDest);
    }
}
