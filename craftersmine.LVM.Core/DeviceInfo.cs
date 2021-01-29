using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Contains information about device
    /// </summary>
    public struct DeviceInfo
    {
        /// <summary>
        /// Gets or sets device name. Device name usually represents user-firendly device name like Keyboard, GPU, etc.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets device model. Device model usually represents device model number like KBD-1253 Super.
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Gets or sets device description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets device serial number.
        /// </summary>
        public string Serial { get; set; }
        /// <summary>
        /// Gets or sets device vendor name.
        /// </summary>
        public string Vendor { get; set; }
        /// <summary>
        /// Gets or sets device class. Recommended to use internal device classes from <see cref="DeviceClasses"/> class
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// Gets or sets device type. Recommended to use internal device types from <see cref="DeviceTypes"/> class
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets device identifier. Device identifier is kind of PID &amp; VID, recommended to use hexademical value like 0xFFFFFFFF, where first 4 bytes is PID, and 4 last bytes is 4 bytes
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets device vendor parameters. They could be used in software or firmware to determine specific capabilities
        /// </summary>
        public DeviceVendorParameter[] VendorParameters { get; set; }
        /// <summary>
        /// Gets or sets device version.
        /// </summary>
        public Version Version { get; set; }
    }
}
