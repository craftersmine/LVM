using craftersmine.LVM.Core.Attributes;
using craftersmine.LVM.Core.Components;
using craftersmine.LVM.Core.Extensions;

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
    public partial class MachineConfigurator : UserControl, IDeviceConfigurator
    {
        public MachineConfigurator()
        {
            InitializeComponent();
        }

        public bool AutoWidth { get; set; }
        public bool AutoHeight { get; set; }

        public void ApplyConfig(IDevice device)
        {
            ((MachineComponent)device).MachineName = machineName.Text;
            if (machineRootDir.SelectedPath.IsNullEmptyOrWhitespace())
                machineRootDir.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ((MachineComponent)device).MachineRootDirectory = machineRootDir.SelectedPath;
        }

        public void LoadConfig(IDevice device)
        {
            machineName.Text = ((MachineComponent)device).MachineName;
            machineRootDir.SelectedPath = ((MachineComponent)device).MachineRootDirectory;
            if (machineRootDir.SelectedPath.IsNullEmptyOrWhitespace())
                machineRootDir.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
    }
}
