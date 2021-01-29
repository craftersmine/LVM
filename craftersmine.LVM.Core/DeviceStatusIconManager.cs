using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Allows you to set machine window status bar icon and tooltip. This class cannot be inherited
    /// </summary>
    public sealed class DeviceStatusIconManager
    {
        private static Dictionary<string, DeviceStatusIcon[]> icons = new Dictionary<string, DeviceStatusIcon[]>();
        private static Dictionary<string, string> tooltips = new Dictionary<string, string>();

        /// <summary>
        /// Occurs when device tries to update its status in machine window status bar icon
        /// </summary>
        public static event EventHandler<SetDeviceStatusEventArgs> OnSetDeviceStatus;
        /// <summary>
        /// Occurs when device tries to update its status bar icon tooltip
        /// </summary>
        public static event EventHandler<SetDeviceStatusTooltipEventArgs> OnSetDeviceStatusTooltip;

        /// <summary>
        /// Registers status icon for specified device
        /// </summary>
        /// <param name="deviceAddress">Device address for status icon</param>
        /// <param name="statusIcons">Possible status icons. Should contain at least 1 element marked as "default"</param>
        public static void RegisterStatusIconForDevice(string deviceAddress, DeviceStatusIcon[] statusIcons)
        {
            if (!icons.ContainsKey(deviceAddress))
            {
                if (statusIcons.Length >= 1)
                {
                    bool isDefaultExists = false;
                    for (int i = 0; i < statusIcons.Length; i++)
                        if (statusIcons[i].Status == "default")
                            isDefaultExists = true;
                    if (isDefaultExists)
                        icons.Add(deviceAddress, statusIcons);
                    else throw new ArgumentException("No default icon found in status icons collection", nameof(statusIcons));
                }
                else throw new ArgumentException("Status icons array should contain at least 1 element to be used as default", nameof(statusIcons));
            }
        }

        /// <summary>
        /// Registers default tooltip for tooltip of specified device
        /// </summary>
        /// <param name="deviceAddress">Device address</param>
        /// <param name="tooltip">Status icon tooltip</param>
        public static void SetStatusIconTooltipForDevice(string deviceAddress, string devType, string tooltip)
        {
            if (!icons.ContainsKey(deviceAddress))
            {
                tooltips.Add(deviceAddress, tooltip);
                OnSetDeviceStatusTooltip?.Invoke(Machine.RunningInstance, new SetDeviceStatusTooltipEventArgs() { DeviceAddress = deviceAddress, Tooltip = tooltip, DeviceType = devType });
            }
        }

        /// <summary>
        /// Raises <see cref="OnSetDeviceStatus"/> event with specified arguments
        /// </summary>
        /// <param name="deviceAddress">Device addres</param>
        /// <param name="status">Status id to set</param>
        public static void SetDeviceStatus(string deviceAddress, string status)
        {
            if (icons.ContainsKey(deviceAddress))
            {
                OnSetDeviceStatus?.Invoke(Machine.RunningInstance, new SetDeviceStatusEventArgs() { DeviceAddress = deviceAddress, Status = status, DeviceStatusIcons = icons[deviceAddress] });
            }
        }

        public static void ClearRegisteredIcons()
        {
            icons.Clear();
        }
    }

    /// <summary>
    /// Contains information about status icon
    /// </summary>
    public struct DeviceStatusIcon
    {
        /// <summary>
        /// Gets or sets status icon image
        /// </summary>
        public Image Icon { get; set; }
        /// <summary>
        /// Gets or sets status id
        /// </summary>
        public string Status { get; set; }
    }

    /// <summary>
    /// Contains information about sent device status. This class cannot be inherited
    /// </summary>
    public sealed class SetDeviceStatusEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets device address which invoked this event
        /// </summary>
        public string DeviceAddress { get; set; }
        /// <summary>
        /// Gets or sets status value to change
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Gets or sets possible device status icons
        /// </summary>
        public DeviceStatusIcon[] DeviceStatusIcons { get; set; }
    }

    /// <summary>
    /// Contains information about sent device status bar icon tooltip. This class cannot be inherited
    /// </summary>
    public sealed class SetDeviceStatusTooltipEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets device address which invoked this event
        /// </summary>
        public string DeviceAddress { get; set; }
        /// <summary>
        /// Gets or sets status bar icon tooltip
        /// </summary>
        public string Tooltip { get; set; }
        public string DeviceType { get; set; }
    }
}
