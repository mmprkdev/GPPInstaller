﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPPInstaller
{
    public interface IActionToTake
    {
        void SetActionToTake(
            CheckBox coreCheckBox,
            CheckBox utilityCheckBox,
            CheckBox visualsCheckBox,
            CheckBox cloudsLowResCheckBox,
            CheckBox cloudsHighResCheckBox);
    }
}
