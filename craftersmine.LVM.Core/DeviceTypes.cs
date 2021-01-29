using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Contains default device types
    /// </summary>
    public static class DeviceTypes
    {
        /// <summary>
        /// An EEPROM chip
        /// </summary>
        public const string EEPROM = "eeprom";
        /// <summary>
        /// A computer motherboard
        /// </summary>
        public const string Machine = "machine";
        /// <summary>
        /// A managed filesystem. Like formatted hard disk, optical media or floppy disks
        /// </summary>
        public const string ManagedStorage = "filesystem";
        /// <summary>
        /// An unmanaged storage like a hard disk
        /// </summary>
        public const string UnmanagedStorageHarddisk = "harddisk";
        /// <summary>
        /// An unmanaged storage like optical media
        /// </summary>
        public const string UnmanagedStorageOptical = "optical";
        /// <summary>
        /// An unmanaged storage like floppy disk
        /// </summary>
        public const string UnmanagedStorageFloppy = "floppy";
        /// <summary>
        /// A generic keyboard
        /// </summary>
        public const string Keyboard = "keyboard";
        /// <summary>
        /// A generic screen
        /// </summary>
        public const string Screen = "screen";
        /// <summary>
        /// A Graphics Processing Unit or GPU
        /// </summary>
        public const string Gpu = "gpu";
        /// <summary>
        /// A local network adapter card
        /// </summary>
        public const string Network = "network";
        /// <summary>
        /// An internet card
        /// </summary>
        public const string Internet = "internet";
    }
}
