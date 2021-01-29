using craftersmine.LVM.Core;
using craftersmine.LVM.Core.Attributes;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace craftersmine.LVM.GUI
{
    public partial class DeviceTypeSelectForm : Form
    {
        public DeviceTypeSelectForm()
        {
            InitializeComponent();
            foreach (var devType in DeviceTypeRegistry.GetRegisteredDeviceTypes(out string[] types))
            {
                var attr = devType.GetCustomAttribute<DeviceComponentAttribute>();
                var icon = DeviceTypeRegistry.GetDeviceTypeIcon(devType.Name.ToLower());
                icons.Images.Add(devType.Name.ToLower(), icon);
                devList.Items.Add(new ListViewItem() { Text = attr.UserFriendlyName, ImageKey = devType.Name.ToLower() });
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            
        }
    }
}
