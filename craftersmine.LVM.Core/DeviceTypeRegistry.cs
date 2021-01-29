using craftersmine.LVM.Core.Attributes;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    public static class DeviceTypeRegistry
    {
        private static Dictionary<string, Type> _deviceTypes = new Dictionary<string, Type>();
        private static Dictionary<string, Image> _deviceIcons = new Dictionary<string, Image>();

        public static void RegisterDeviceType(Type deviceType)
        {
            string devTypeName = deviceType.Name;
            Settings.LoggerInstance.Log(LogEntryType.Info, "dev-registry", "Registering device type of " + devTypeName + "...");

            if (deviceType.GetInterface(nameof(IDevice)) == null)
            {
                Settings.LoggerInstance.Log(LogEntryType.Error, "dev-registry", "Unable to register device type of " + devTypeName + "! This device isn't inherited from BaseDevice nor IDevice");
                return;
            }

            if (deviceType.GetCustomAttribute(typeof(DeviceComponentAttribute)) == null)
            {
                Settings.LoggerInstance.Log(LogEntryType.Error, "dev-registry", "Unable to register device type of " + devTypeName + "! This device doesn't have a " + nameof(DeviceComponentAttribute) + " attribute");
                return;
            }

            if (!_deviceTypes.ContainsKey(devTypeName))
            {
                _deviceTypes.Add(devTypeName.ToLower(), deviceType);
                Settings.LoggerInstance.Log(LogEntryType.Info, "dev-registry", "Device type of " + devTypeName + " successfully registered!");
            }
            else Settings.LoggerInstance.Log(LogEntryType.Warning, "dev-registry", "Unable to register device type of " + devTypeName + "! This type of devices is already registered! Is it called to register twice?");
        }

        public static Type GetRegisteredDeviceType(string devType)
        {
            if (_deviceTypes.ContainsKey(devType))
                return _deviceTypes[devType];
            else return null;
        }

        public static Type[] GetRegisteredDeviceTypes(out string[] deviceTypesStrings)
        {
            deviceTypesStrings = _deviceTypes.Keys.ToArray();
            return _deviceTypes.Values.ToArray();
        }

        public static void RegisterDeviceTypeIcon(Type deviceType, Image icon)
        {
            _deviceIcons.Add(deviceType.Name.ToLower(), icon);
        }

        public static Image GetDeviceTypeIcon(string devType)
        {
            if (_deviceIcons.ContainsKey(devType))
                return _deviceIcons[devType];
            else return DeviceDefaultIcons.Generic;
        }
    }
}
