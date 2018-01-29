using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPPInstaller
{
    public interface IConverter
    {
        string SerializeModListToJson(List<Mod> modList);
    }
}
