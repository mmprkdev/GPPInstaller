using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GPPInstaller
{
    class Converter : IConverter
    {
        public string SerializeModListToJson(List<Mod> modList)
        {
            string json = JsonConvert.SerializeObject(modList);
            return json;
        }
    }
}
