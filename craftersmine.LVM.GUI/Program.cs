using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using craftersmine.LVM.Core;
using craftersmine.LVM.GUI.Forms;

namespace craftersmine.LVM.GUI
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new Logger(Environment.GetEnvironmentVariable("TEMP"), "LVM"); 
            SettingsManager.DisplayFontManager = new CustomFontManager(Path.Combine(Application.StartupPath, "unscii.ttf"), 16f);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new VMForm());
        }
    }
}
