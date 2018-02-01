using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace GPPInstaller
{
    class Downloader : IDownloader
    {
        // NOTE: this isn't working. maybe look into why
        public void DownloadAsync(string downloadLink, string destFile)
        {
            var webclient = new WebClient();
            var uri = new Uri(downloadLink);
            webclient.DownloadFileAsync(uri, destFile);
        }

        public void Download(string downloadLink, string destFile)
        {
            var webclient = new WebClient();
            webclient.DownloadFile(downloadLink, destFile);
        }
    }
}
