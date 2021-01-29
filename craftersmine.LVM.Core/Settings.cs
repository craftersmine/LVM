using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Contains static properties for various LVM settings
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Enable or disable logging
        /// </summary>
        public static bool EnableLogging { get; set; } = true;

        /// <summary>
        /// Gets or sets current logger instance
        /// </summary>
        public static Logger LoggerInstance { get; set; }

        /// <summary>
        /// Gets or sets wait time for writing character into EEPROM
        /// </summary>
        public static int EEPROMWriteTime { get; set; } = 5;

        /// <summary>
        /// Gets or sets EEPROM maximum parameter count allowed to store
        /// </summary>
        public static int EEPROMMaxParamCount { get; set; } = 16;
        public static int EEPROMMaxSize { get; set; } = 8192;
    }
}
