using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPPInstaller
{
    public interface IDataStorage
    {
        void WriteStringToFile(string targetFile, string text);
        string ReadFileIntoString(string sourceFile);
    }
}
