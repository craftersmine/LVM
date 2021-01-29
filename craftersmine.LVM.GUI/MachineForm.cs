using craftersmine.LVM.Core;
using craftersmine.LVM.Core.Components;
using craftersmine.LVM.Core.Exceptions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Screen = craftersmine.LVM.Core.Components.Screen;

namespace craftersmine.LVM.GUI
{
    public partial class MachineForm : Form
    {
        private Machine machine;
        private Dictionary<string, ToolStripStatusLabel> icons = new Dictionary<string, ToolStripStatusLabel>();
        private string machineDir;
        private string tooltipCtor = "{devType} - {devAddr}\r\n\r\n{devTooltip}";
        private Screen currentScreenDevice;
        private UpdateTimer updater;

        public MachineForm(Machine machine)
        {
            InitializeComponent();

            MachineEvents.MachineHalted += MachineEvents_MachineHalted;
            MachineEvents.MachineReboot += MachineEvents_MachineReboot;

            DeviceStatusIconManager.OnSetDeviceStatus += DeviceStatusIconManager_OnSetDeviceStatus;
            DeviceStatusIconManager.OnSetDeviceStatusTooltip += DeviceStatusIconManager_OnSetDeviceStatusTooltip;

            machineDir = "D:\\TestLVMMachine";

            updater = new UpdateTimer(30f);
            updater.Update += Updater_Update;

            this.machine = machine;
        }

        private void Updater_Update(object sender, UpdateEventArgs e)
        {
            updateScreen();
        }

        private void updateScreen()
        {
            Bitmap gpuBuffer = getGpuBuffer();
            if (InvokeRequired)
                try
                {
                    Invoke(new Action(() => {
                        drawGpuBuffer(gpuBuffer);
                        status.Text = string.Format("Last Update Time: {0:F2}ms | Lag time: {1:F2}ms", updater.UpdateTime.TotalMilliseconds, updater.Lag.TotalMilliseconds);
                    }));
                }
                catch { }
            else { drawGpuBuffer(gpuBuffer); }
        }

        private void drawGpuBuffer(Bitmap gpuBuffer)
        {
            if (gpuBuffer != null)
            {
                screen.Width = gpuBuffer.Width;
                screen.Height = gpuBuffer.Height;
                screen.BackgroundImage = gpuBuffer;
            }
        }

        private Bitmap getGpuBuffer()
        {
            return currentScreenDevice.GpuBuffer;
        }

        private void DeviceStatusIconManager_OnSetDeviceStatusTooltip(object sender, SetDeviceStatusTooltipEventArgs e)
        {
            if (icons.ContainsKey(e.DeviceAddress))
                icons[e.DeviceAddress].ToolTipText = GetTooltip(e.DeviceAddress, e.DeviceType, e.Tooltip);
        }

        private void DeviceStatusIconManager_OnSetDeviceStatus(object sender, SetDeviceStatusEventArgs e)
        {
            if (icons.ContainsKey(e.DeviceAddress))
            {
                Image img = null;
                foreach (var entry in e.DeviceStatusIcons)
                {
                    if (entry.Status == e.Status)
                    {
                        img = entry.Icon;
                        break;
                    }
                }
                if (img != null)
                    icons[e.DeviceAddress].Image = img;
            }
        }

        private void MachineEvents_MachineReboot(object sender, EventArgs e)
        {
            RebootMachine();
        }

        private void MachineEvents_MachineHalted(object sender, MachineHaltedEventArgs e)
        {
            updater.Stop();
            if (e.Reason == MachineHaltReason.Crash)
            {
                if (e.CrashException.Message.Contains(MachineErrorMessages.NoEepromFound))
                    MessageBox.Show("Machine EEPROM not found or empty. It prevents booting machine, install proper EEPROM with code or make it yourself.", "Machine EEPROM not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (e.CrashException.Message.Contains(MachineErrorMessages.FailedToLoadBios))
                    MessageBox.Show("Unable to load BIOS from EEPROM, it maybe caused by corrupted EEPROM code, syntax code or something else. You can find more information below:\r\n\r\n" + e.CrashException.Message, "Machine BIOS load failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else MessageBox.Show("Machine has crashed with unknown error!\r\n\r\nSomething critical happened while running VM! More information can be found below:\r\n\r\n" + e.CrashException.Message + "\r\nStack trace:\r\n" + e.CrashException.StackTrace);
            }
            else if (e.Reason == MachineHaltReason.Shutdown)
            {
                Machine.RunningInstance.ClearState();
            }

            if (InvokeRequired)
                Invoke(new Action(() => { this.Close(); }));
            else { this.Close(); }
        }

        private void launchRequired(object sender, EventArgs e)
        {
            LaunchMachine(machineDir);
        }

        private void shutdown_Click(object sender, EventArgs e)
        {
            ShutdownMachine();
        }

        private void reboot_Click(object sender, EventArgs e)
        {
            RebootMachine();
        }
        
        private void LaunchMachine(string machineDir)
        {
            //Machine machine = Machine.LoadMachine(machineDir, displayInterface);
            //machine.DeviceBus.ConnectDevice(machine.GetMachineComponent(), machine.GetMachineComponent().Address, true);
            //EEPROM eeprom = machine.DeviceBus.GetDevicesOfType<EEPROM>("eeprom")[0];
            //Screen scr = new Screen();
            //machine.DeviceBus.ConnectDevice(new GPU(), Guid.NewGuid(), true);
            //machine.DeviceBus.ConnectDevice(scr, Guid.NewGuid(), true);
            //eeprom.SetCode("local gpu = component.proxy(component.list('gpu')()); local screen = component.proxy(component.list('screen')()); gpu.bind(screen.address); gpu.set(0, 1, 'A');");
            //foreach (var dev in machine.DeviceBus.GetDevices())
            //{
            //    var devCompAttr = dev.GetComponentAttribute();
            //    if (devCompAttr.ShowStatusBarIcon)
            //        CreateIcon(dev.Address.ToString(), dev.DeviceIcon, GetTooltip(dev.Address.ToString(), devCompAttr.ComponentType, devCompAttr.DefaultTooltip));
            //}
            //var screens = machine.DeviceBus.GetDevicesOfType<Screen>(DeviceTypes.Screen);
            //if (screens.Length <= 0)
            //    throw new MachineCrashException("Unable to launch machine! No screen device found!");
            //currentScreenDevice = screens[0];


            foreach (var dev in machine.DeviceBus.GetDevices())
            {
                var devCompAttr = dev.GetComponentAttribute();
                if (devCompAttr.ShowStatusBarIcon)
                    CreateIcon(dev.Address.ToString(), dev.DeviceIcon, GetTooltip(dev.Address.ToString(), devCompAttr.ComponentType, devCompAttr.DefaultTooltip));
            }
            var screens = machine.DeviceBus.GetDevicesOfType<Screen>(DeviceTypes.Screen);
            if (screens.Length <= 0)
                throw new MachineCrashException("Unable to launch machine! No screen device found!");
            currentScreenDevice = screens[0];


            updater.Start();
            machine.Run(false);
        }

        private void ShutdownMachine()
        {
            var dlgRes = MessageBox.Show("It will send a system signal which will request shutdown feature. It will not work if running system doesn't handle such function. Send signal to shutdown machine?", "Request machine shutdown", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            switch (dlgRes)
            {
                case DialogResult.Yes:
                    machine.SendSignal("machine_shutdown_requested");
                    break;
            }
        }

        private void RebootMachine()
        {
            var dlgRes = MessageBox.Show("It will shut machine down without warning and restart it, save all work before doing this because it could cause severe damage to system or corrupt data! Power off machine?", "Powering off the machine", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            switch (dlgRes)
            {
                case DialogResult.Yes:
                    updater.Stop();
                    machine.Stop();
                    LaunchMachine(machineDir);
                    break;
            }
        }

        private void poweroffMenu_Click(object sender, EventArgs e)
        {
            var dlgRes = MessageBox.Show("It will shut machine down without warning, save all work before doing this because it could cause severe damage to system or corrupt data! Power off machine?", "Powering off the machine", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            switch (dlgRes)
            {
                case DialogResult.Yes:
                    machine.Stop();
                    break;
            }
        }

        public string GetTooltip(string devAddress, string devType, string tooltip)
        {
            string t = tooltipCtor.Replace("{devAddr}", devAddress).Replace("{devType}", devType).Replace("{devTooltip}", tooltip);
            return t;
        }

        private void DisplayInitialized(object sender, EventArgs e)
        {

        }

        private void MachineForm_Shown(object sender, EventArgs e)
        {
            LaunchMachine(machineDir);
        }

        public ToolStripStatusLabel CreateIcon(string devAddress, Image icon, string tooltipText)
        {
            var icn = new ToolStripStatusLabel() { Image = icon, DisplayStyle = ToolStripItemDisplayStyle.Image };
            icn.Margin = new Padding(0, 0, 3, 0);
            icn.ToolTipText = tooltipText;
            icons.Add(devAddress, icn);
            statusStrip1.Items.Add(icn);
            return icn;
        }
    }
}
