using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Contains constants with various error messages
    /// </summary>
    public static class MachineErrorMessages
    {
        /// <summary>
        /// No such component error message
        /// </summary>
        public const string NoSuchComponent = "no such component";
        /// <summary>
        /// File not found error message
        /// </summary>
        public const string FileNotFound = "file not found";
        /// <summary>
        /// Bad file descriptor error message
        /// </summary>
        public const string BadFileDescriptor = "bad file descriptor";
        /// <summary>
        /// Unsupported mode error message
        /// </summary>
        public const string UnsupportedMode = "unsupported mode";
        /// <summary>
        /// Permission denied error message
        /// </summary>
        public const string PermissionDenied = "access denied";
        /// <summary>
        /// User exists error message
        /// </summary>
        public const string UserExists = "user exists";
        /// <summary>
        /// No bootable medium found error message
        /// </summary>
        public const string NoBootableMediumFound = "no bootable medium found";
        /// <summary>
        /// Machine halted error message
        /// </summary>
        public const string MachineHalted = "machine halted";
        /// <summary>
        /// Invalid fill value when trying call fill GPU function error message
        /// </summary>
        public const string InvalidFillValue = "invalid fill value";
        /// <summary>
        /// Invalid device address error message
        /// </summary>
        public const string InvalidAddress = "invalid address";
        /// <summary>
        /// Device is not a screen error message
        /// </summary>
        public const string NotAScreen = "not a screen";
        /// <summary>
        /// Invalid GPU pallete index error message
        /// </summary>
        public const string InvalidPaletteIndex = "invalid palette index";
        /// <summary>
        /// Invalid GPU color depth error message
        /// </summary>
        public const string UnsupportedDepth = "unsupported depth";
        /// <summary>
        /// Invalid GPU buffer index error message
        /// </summary>
        public const string InvalidBufferIndex = "invalid buffer index";
        /// <summary>
        /// Unsupported view size error message
        /// </summary>
        public const string UnsupportedViewportSize = "unsupported viewport size";
        /// <summary>
        /// No such method error message
        /// </summary>
        public const string NoSuchMethod = "no such method";
        /// <summary>
        /// No such field error message
        /// </summary>
        public const string NoSuchField = "no such field";
        /// <summary>
        /// No EEPROM device found on device bus error message
        /// </summary>
        public const string NoEepromFound = "no eeprom found";
        /// <summary>
        /// Failed to load BIOS code from EEPROM error message
        /// </summary>
        public const string FailedToLoadBios = "failed to load bios";
    }
}
