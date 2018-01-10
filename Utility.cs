using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace GPPInstaller
{
    class Utility
    {

        public string[] GetDownloadLinks()
        {
            string kopernicusUrl = "https://github.com/Kopernicus/Kopernicus/releases";
            string gppUrl = "https://github.com/Galileo88/Galileos-Planet-Pack/releases";
            string gppTexturesUrl = "https://github.com/Galileo88/Galileos-Planet-Pack/releases/tag/3.0.0";
            string eveUrl = "https://github.com/WazWaz/EnvironmentalVisualEnhancements/releases";
            string scattererUrl = "https://spacedock.info/mod/141/scatterer";
            string doeUrl = "https://github.com/MOARdV/DistantObject/releases";

            string kopernicusXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div[1]/div[2]/div[2]/ul/li[1]/a";
            string gppXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div[1]/div[2]/div[2]/ul/li[1]/a";
            string gppTexturesXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div/div[2]/div[2]/ul/li[1]/a";
            string eveXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div[1]/div[2]/div[2]/ul/li[1]/a";
            string scattererXpath = "/html[1]/body[1]/div[3]/div[1]/div[2]/a[1]";
            string doeXpath = "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div[1]/div[2]/div[2]/ul/li[1]/a";

            // "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div[1]/div[2]/div[1]/h1/a";
            // "/html/body/div[4]/div/div/div[2]/div[1]/div[2]/div/div[2]/div[1]/h1/a"
            // /html/body/div[4]/div/div/div[2]/div[1]/div[2]/div/div[2]/div[2]/ul/li[1]/a/strong"

            string[] links = new string[6];

            links[0] = GetLinkGitHub(kopernicusUrl, kopernicusXpath);
            links[1] = GetLinkGitHub(gppUrl, gppXpath);
            links[2] = GetLinkGitHub(gppTexturesUrl, gppTexturesXpath);
            links[3] = GetLinkGitHub(eveUrl, eveXpath);
            links[4] = GetLinkSpaceDock(scattererUrl, scattererXpath);
            links[5] = GetLinkGitHub(doeUrl, doeXpath);

            return links;
        }

        private string GetLinkGitHub(string url, string xpath)
        {
            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(url);
            var anchor = htmlDoc.DocumentNode.SelectNodes(xpath);
            var outterHtml = anchor.Select(node => node.OuterHtml);
            string item = outterHtml.ElementAt(0);

            int leadingEnd = item.IndexOf('"');
            string link = item.Remove(0, leadingEnd + 1);
            int trailingStart = link.IndexOf('"');
            link = link.Remove(trailingStart, link.Length - trailingStart);
            link = "https://github.com" + link;

            return link;

        }

        private string GetLinkSpaceDock(string url, string xpath)
        {
            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(url);
            var anchor = htmlDoc.DocumentNode.SelectNodes(xpath);
            var outterHtml = anchor.Select(node => node.OuterHtml);
            string item = outterHtml.ElementAt(0);

            int leadingEnd = item.IndexOf("h") - 1;
            string href = item.Remove(0, leadingEnd + 1);
            leadingEnd = href.IndexOf('"') + 1;
            href = href.Remove(0, leadingEnd);
            int trailingStart = href.LastIndexOf('"');
            href = href.Remove(trailingStart, (href.Length - trailingStart));

            string link = "https://spacedock.info" + href;
            return link;
        }

        public string DownloadLinkToExtractedDir(string downloadLink)
        {
            int subStart = downloadLink.LastIndexOf('/') + 1;
            int subEnd = downloadLink.LastIndexOf('.');
            int length = subEnd - subStart;
            string extractedDir = downloadLink.Substring(subStart, length);

            return extractedDir;
        }

        public string DownloadLinkToExtractedDir(string downloadLink, bool isScatterer)
        {
            string extractedDir;

            int offset = downloadLink.IndexOf('/');
            offset = downloadLink.IndexOf('/', offset + 1);
            offset = downloadLink.IndexOf('/', offset + 1);
            offset = downloadLink.IndexOf('/', offset + 1);
            int count = downloadLink.Length - offset;
            extractedDir = downloadLink.Remove(0, count);

            int start = extractedDir.IndexOf('/');
            int end = extractedDir.LastIndexOf('/');
            count = end - start;
            extractedDir = extractedDir.Remove(start, count);
            extractedDir = extractedDir.Replace('/', '-');

            return extractedDir;
        }

        public string DownloadLinkToZip(string downloadLink)
        {
            int subStart = downloadLink.LastIndexOf('/') + 1;
            int length = downloadLink.Length - subStart;
            string archive = downloadLink.Substring(subStart, length);

            return archive;
        }

        public string DownloadLinkToZip(string downloadLink, bool isScatterer)
        {
            string archive;
            
            int offset = downloadLink.IndexOf('/');
            offset = downloadLink.IndexOf('/', offset + 1);
            offset = downloadLink.IndexOf('/', offset + 1);
            offset = downloadLink.IndexOf('/', offset + 1);
            int count = downloadLink.Length - offset;
            archive = downloadLink.Remove(0, count);

            int start = archive.IndexOf('/');
            int end = archive.LastIndexOf('/');
            count = end - start;
            archive = archive.Remove(start, count);
            archive = archive.Replace('/', '-');
            archive += ".zip"; 

            return archive;
        }
    }
}
