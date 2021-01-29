using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Attributes
{
    /// <summary>
    /// Specifies class as proper Device to be used in machines
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class DeviceComponentAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DeviceComponentAttribute"/> with specified parameters
        /// </summary>
        public DeviceComponentAttribute()
        {
        }

        /// <summary>
        /// Gets or sets device component type that being used in machines. It is recomended to use defined types from <see cref="DeviceTypes"/> class if device exists in it. The default value of this parameter is "generic"
        /// </summary>
        /// <example>You can have class named like MyCoolDevice but having <see cref="ComponentType"/> set as "mydevice". That make your device visible in list as "mydevice" instead of "MyCoolDevice".</example>
        public string ComponentType { get; set; } = "generic";
        public string DeviceIcon { get; set; } = "generic";
        public string DefaultTooltip { get; set; } = "Generic device";
        public bool ShowStatusBarIcon { get; set; } = true;
        public string UserFriendlyName { get; set; } = "Generic Device";
    }
}
