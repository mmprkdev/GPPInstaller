using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.IO.Compression;

namespace GPPInstaller
{
    class Converter : IConverter
    {
        public string SerializeModListToJson(List<Mod> modList)
        {
            string json = JsonConvert.SerializeObject(modList);
            return json;
        }

        public void DeserializeJsonToModList(string json, ref List<Mod> modList)
        {
            modList = JsonConvert.DeserializeObject<List<Mod>>(json);
        }

        public void Unzip(string zipFile, string destDir)
        {
            using (var archive = ZipFile.OpenRead(zipFile))
            {
                foreach (var entry in archive.Entries)
                {
                    if (entry.IsFolder())
                    {
                        Directory.CreateDirectory(Path.Combine(destDir, entry.FullName));
                    }
                    else
                    {
                        entry.ExtractToFile(Path.Combine(destDir, entry.FullName));
                    }
                }
            }
        }
    }
}
