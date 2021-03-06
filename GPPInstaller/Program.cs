﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPPInstaller
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // TODO: consider instantiating core here
            Application.Run(new Form1(
                new ModState(),
                new ModListInit(),
                new CheckBoxes(),
                new ActionToTake(),
                new Installer(),
                new Uninstall(),
                new ProgressBarSteps(),
                new Version(),
                new CheckExe(),
                new Converter(),
                new DataStorage(),
                new Downloader()));
        }
    }
}
