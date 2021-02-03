using craftersmine.LVM.Core.Attributes;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            
        }

        public void LoadConfig(IDevice device)
        {
            
        }
    }
}
