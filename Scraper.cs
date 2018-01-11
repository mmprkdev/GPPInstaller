using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace GPPInstaller
{
    class Scraper
    {
        public Scraper()
        {
        }

        public void AddLinks(ref List<Mod> modList)
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

            // DownloadAddress
            modList[0].DownloadAddress = GetLink(kopernicusUrl, kopernicusXpath);
            modList[1].DownloadAddress = GetLink(gppUrl, gppXpath);
            modList[2].DownloadAddress = GetLink(gppTexturesUrl, gppTexturesXpath);
            modList[3].DownloadAddress = GetLink(eveUrl, eveXpath);
            modList[4].DownloadAddress = GetLink(scattererUrl, scattererXpath);
            modList[5].DownloadAddress = GetLink(doeUrl, doeXpath);

            // ArchiveFileName
            modList[0].ArchiveFileName = GetArchiveFileName(modList[0].DownloadAddress);
            modList[1].ArchiveFileName = GetArchiveFileName(modList[1].DownloadAddress);
            modList[2].ArchiveFileName = GetArchiveFileName(modList[2].DownloadAddress);
            modList[3].ArchiveFileName = GetArchiveFileName(modList[3].DownloadAddress);
            modList[4].ArchiveFileName = GetArchiveFileName(modList[4].DownloadAddress, true);
            modList[5].ArchiveFileName = GetArchiveFileName(modList[5].DownloadAddress);

            // ExtractedDirName
            modList[0].ExtractedDirName = RemoveZip(modList[0].ArchiveFileName);
            modList[1].ExtractedDirName = RemoveZip(modList[1].ArchiveFileName);
            modList[2].ExtractedDirName = RemoveZip(modList[2].ArchiveFileName);
            modList[3].ExtractedDirName = RemoveZip(modList[3].ArchiveFileName);
            modList[4].ExtractedDirName = RemoveZip(modList[4].ArchiveFileName);
            modList[5].ExtractedDirName = RemoveZip(modList[5].ArchiveFileName);

            // ExtractedPath 
            for (int i = 0; i < 8; i++)
            {
                if (modList[i].ModType == "Clouds")
                {
                    modList[i].ExtractedPath = AddExtractedPath(modList[i].ModName, modList[1].ExtractedDirName);
                }
                else
                {
                    modList[i].ExtractedPath = AddExtractedPath(modList[i].ModName, modList[i].ExtractedDirName);
                }
                
            }
        }

        private string GetLink(string url, string xpath)
        {
            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(url);
            var anchor = htmlDoc.DocumentNode.SelectNodes(xpath);

            // NOTE: anchor is sometimes null. I think it may just
            // be unreliablity in fetching data the internet. Retry
            // for a certain amount of time, then throw an error (???).

            var outterHtml = anchor.Select(node => node.OuterHtml);
            string item = outterHtml.ElementAt(0);

            string link;

            if (url == "https://spacedock.info/mod/141/scatterer")
            {
                int leadingEnd = item.IndexOf("h") - 1;
                string href = item.Remove(0, leadingEnd + 1);
                leadingEnd = href.IndexOf('"') + 1;
                href = href.Remove(0, leadingEnd);
                int trailingStart = href.LastIndexOf('"');
                href = href.Remove(trailingStart, (href.Length - trailingStart));

                link = "https://spacedock.info" + href;
            }
            else
            {
                int leadingEnd = item.IndexOf('"');
                link = item.Remove(0, leadingEnd + 1);
                int trailingStart = link.IndexOf('"');
                link = link.Remove(trailingStart, link.Length - trailingStart);
                link = "https://github.com" + link;
            }

            return link;
        }

        public string GetArchiveFileName(string downloadLink)
        {
            int subStart = downloadLink.LastIndexOf('/') + 1;
            int length = downloadLink.Length - subStart;
            string archive = downloadLink.Substring(subStart, length);

            return archive;
        }

        public string GetArchiveFileName(string downloadLink, bool isScatter)
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

        private string RemoveZip(string zipFile)
        {
            int trailingStart = zipFile.LastIndexOf('.');
            int count = zipFile.Length - trailingStart;
            string result = zipFile.Remove(trailingStart, count);

            return result;
        }
        
        private string AddExtractedPath(string modName, string extractedDirName)
        {
            if (modName == "CloudsLowRes")
            {
                return @".\GPPInstaller\" + extractedDirName + @"\Optional Mods\GPP_Clouds\Low-res Clouds_GameData inside\GameData\GPP";
            }
            else if (modName == "CloudsHighRes")
            {
                return @".\GPPInstaller\" + extractedDirName + @"\Optional Mods\GPP_Clouds\High-res Clouds_GameData inside\GameData\GPP";
            }
            else
            {
                return @".\GPPInstaller\" + extractedDirName + @"\GameData\GPP";
            } 
        }

    }
}
