using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GPPInstaller
{
    class Errors
    {
        Form1 form1;

        public Errors(Form1 form1)
        {
            this.form1 = form1;
        }
        
        // TODO: left off here
        public void NoInternetConnectionError()
        {
            form1.ProgressLabelUpdate("Error: No internet connection was detected.");
            form1.DisplayYellowWarning();
            form1.RemoveProgressBar();
            form1.RemoveCancelButton();
        }
    }
}
