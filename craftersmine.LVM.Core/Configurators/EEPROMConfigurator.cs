using craftersmine.LVM.Core.Attributes;
using craftersmine.LVM.Core.Components;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace craftersmine.LVM.Core.Configurators
{
    [DeviceConfigurator]
    public partial class EEPROMConfigurator : UserControl, IDeviceConfigurator
    {
        public bool AutoWidth { get; set; } = true;
        public bool AutoHeight { get; set; } = false;

        public EEPROMConfigurator()
        {
            InitializeComponent();
        }

        public void ApplyConfig(IDevice dev)
        {
            if (useInternalRom.Checked)
                ((EEPROM)dev).UseStandardBios = true;
            if (useCustomRom.Checked)
            {
                ((EEPROM)dev).UseStandardBios = false;
                ((EEPROM)dev).BiosCodeFilepath = eepromPath.SelectedPath;
            }
            ((EEPROM)dev).isReadOnly = isReadOnly.Checked;
        }

        public void LoadConfig(IDevice dev)
        {
            eepromPath.SelectedPath = ((EEPROM)dev).BiosCodeFilepath;
            isReadOnly.Checked = ((EEPROM)dev).isReadOnly;
            if (((EEPROM)dev).UseStandardBios)
            {
                useInternalRom.Checked = true;
                useCustomRom.Checked = false;
            }
            else
            {
                useInternalRom.Checked = false;
                useCustomRom.Checked = true;
            }
        }

        private void useInternalRom_CheckedChanged(object sender, EventArgs e)
        {
            eepromPath.Enabled = false;
        }

        private void useCustomRom_CheckedChanged(object sender, EventArgs e)
        {
            eepromPath.Enabled = true;
            eepromPath.SelectedPath = Path.Combine(Environment.CurrentDirectory, "Common\\bios.lua");
        }
    }
}
