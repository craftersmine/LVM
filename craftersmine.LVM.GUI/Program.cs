using craftersmine.LVM.Core;
using craftersmine.LVM.Core.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Screen = craftersmine.LVM.Core.Components.Screen;

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
            Settings.LoggerInstance = new Logger(Environment.GetEnvironmentVariable("TEMP"), "LVM");

            Settings.LoggerInstance.Log(LogEntryType.Info, "dev-registry", "Registering standard devices...");
            DeviceTypeRegistry.RegisterDeviceType(typeof(MachineComponent));
            DeviceTypeRegistry.RegisterDeviceType(typeof(EEPROM));
            DeviceTypeRegistry.RegisterDeviceType(typeof(Screen));
            DeviceTypeRegistry.RegisterDeviceType(typeof(GPU));
            Settings.LoggerInstance.Log(LogEntryType.Done, "dev-registry", "Standard devices successfully registered!");
            DeviceTypeRegistry.RegisterDeviceTypeIcon(typeof(MachineComponent), DeviceDefaultIcons.Machine);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MachineLauncherForm());
        }
    }
}
