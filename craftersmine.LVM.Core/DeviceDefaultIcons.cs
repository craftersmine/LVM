using craftersmine.LVM.Core.Properties;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    public static class DeviceDefaultIcons
    {
        public static Image EEPROM { get; private set; } = Resources.card;
        public static Image Machine { get; private set; } = Resources.computer;
        public static Image Generic { get; private set; } = Resources.generic;
        public static Image GPU { get; private set; } = Resources.graphics;
        public static Image Harddisk { get; private set; } = Resources.harddisk;
        public static Image Optical { get; private set; } = Resources.optical;
        public static Image Keyboard { get; private set; } = Resources.keyboard;
        public static Image Screen { get; private set; } = Resources.screen;
    }
}
