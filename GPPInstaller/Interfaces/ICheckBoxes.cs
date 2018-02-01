using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPPInstaller
{
    public interface ICheckBoxes
    {
        void SetCheckBoxes(
            Form1 form1,
            CheckBox coreCheckBox,
            CheckBox utilityCheckBox,
            CheckBox visualsCheckBox,
            CheckBox cloudsLowResCheckBox,
            CheckBox cloudsHighResCheckBox);
    }
}
