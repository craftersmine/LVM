using craftersmine.LVM.Core.Attributes;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Defines basic machine device. Recommended to use <see cref="BaseDevice"/> for creating devices
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Gets or sets device address
        /// </summary>
        public Guid Address { get; set; }
        /// <summary>
        /// Gets or sets device info
        /// </summary>
        [DeviceIgnoredProperty]
        public DeviceInfo Info { get; set; }
        [DeviceIgnoredProperty]
        public Image DeviceIcon { get; set; }
        /// <summary>
        /// Gets <see cref="DeviceComponentAttribute"/> of this device
        /// </summary>
        /// <returns><see cref="DeviceComponentAttribute"/> of device class</returns>
        public DeviceComponentAttribute GetComponentAttribute();
        /// <summary>
        /// Gets Lua callbacks infos of device
        /// </summary>
        /// <returns><see cref="LuaCallbackInfo"/> array with methods with <see cref="LuaCallbackAttribute"/> attribute</returns>
        public LuaCallbackInfo[] GetLuaCallbacksInfos();
        /// <summary>
        /// Gets Lua fields infos of device
        /// </summary>
        /// <returns><see cref="LuaCallbackInfo"/> array with properties with <see cref="LuaCallbackAttribute"/> attribute</returns>
        public LuaCallbackInfo[] GetLuaFieldsInfos();
    }
}
