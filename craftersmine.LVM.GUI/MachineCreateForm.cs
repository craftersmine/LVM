using craftersmine.LVM.Core;
using craftersmine.LVM.Core.Attributes;
using craftersmine.LVM.Core.Exceptions;

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
    public partial class MachineCreateForm : Form
    {
        public IDeviceConfigurator currentConfigurator;
        public IDevice currentDevice;

        public MachineCreateForm()
        {
            InitializeComponent();
            foreach (var devType in DeviceTypeRegistry.GetRegisteredDeviceTypes(out string[] types))
            {
                var attr = devType.GetCustomAttribute<DeviceComponentAttribute>();
                var icon = DeviceTypeRegistry.GetDeviceTypeIcon(devType.Name.ToLower());
                icons.Images.Add(devType.Name.ToLower(), icon);
            }
        }

        private void MachineCreateForm_Load(object sender, EventArgs e)
        {

        }

        private void addDev_Click(object sender, EventArgs e)
        {
            var selForm = new DeviceTypeSelectForm();
            selForm.ShowDialog();
            BaseDevice dev = selForm.CreatedDevice;
            selForm.Dispose();
            var attr = dev.GetComponentAttribute();
            devices.Items.Add(new ListViewItem() { Text = attr.UserFriendlyName, Tag = dev, ImageKey = dev.GetType().Name.ToLower() });
        }

        private void devices_ItemActivate(object sender, EventArgs e)
        {
            if (devices.SelectedItems.Count > 0)
            {
                IDevice dev = (IDevice)devices.SelectedItems[0].Tag;

                if (currentConfigurator != null)
                {
                    currentConfigurator.ApplyConfig(currentDevice);
                }

                configuratorPanel.Controls.Clear();

                var configurator = dev.GetComponentAttribute().DeviceConfigurator;
                if (configurator != null)
                {
                    if (configurator.GetCustomAttribute<DeviceConfiguratorAttribute>() != null || configurator.IsSubclassOf(typeof(IDeviceConfigurator)) || configurator.IsSubclassOf(typeof(Control)))
                    {
                        var configuratorObj = (Control)configurator.GetConstructor(Type.EmptyTypes).Invoke(null);
                        configuratorPanel.Controls.Add(configuratorObj);
                        if (((IDeviceConfigurator)configuratorObj).AutoHeight)
                            configuratorObj.Height = configuratorPanel.Height - 2;
                        if (((IDeviceConfigurator)configuratorObj).AutoWidth)
                            configuratorObj.Width = configuratorPanel.Width - 2;
                        currentConfigurator = (IDeviceConfigurator)configuratorObj;
                        currentDevice = dev;
                        currentConfigurator.LoadConfig(currentDevice);
                    }
                    else throw new DeviceConfiguratorException("Invalid Device Configurator \"" + configurator.Name + "\"! Check for " + nameof(DeviceConfiguratorAttribute) + ", " + nameof(IDeviceConfigurator) + " interface inheritance and it is a control");
                }
                else Settings.LoggerInstance.Log(LogEntryType.Warning, "No configurator is assigned to device type \"" + dev.GetComponentAttribute().ComponentType + "\"! Device cannot be configured through GUI! Assign proper configurator through \"" + nameof(DeviceComponentAttribute) + "\"");
            }
        }
    }
}
