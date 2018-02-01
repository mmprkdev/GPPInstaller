using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GPPInstaller
{
    class DataStorage : IDataStorage
    {
        public void WriteStringToFile(string targetFile, string text)
        {

            try
            {
                using (FileStream stream = File.Open(targetFile, FileMode.Create, FileAccess.Write))
                using (var sw = new StreamWriter(stream))
                {
                    sw.Write(text);
                    sw.Close();
                }
            }
            catch (Exception)
            {
                //_form1.Error("UnauthorizedAccessException");
                throw new UnauthorizedAccessException();
            }
        }

        public string ReadFileIntoString(string sourceFile)
        {
            try
            {
                string result;
                using (FileStream stream = File.OpenRead(sourceFile))
                using (var sr = new StreamReader(stream))
                {
                    result = sr.ReadToEnd();
                }

                return result;
            }
            catch (Exception)
            {
                //_form1.Error("UnauthorizedAccessException");
                throw new UnauthorizedAccessException();
            }
        }
    }
}
