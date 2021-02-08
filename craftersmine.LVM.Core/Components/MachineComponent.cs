using craftersmine.LVM.Core.Attributes;
using craftersmine.LVM.Core.Configurators;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Components
{
    /// <summary>
    /// Represents a motherboard device of machine. This class cannot be inherited
    /// </summary>
    [DeviceComponent(ComponentType = "machine", DefaultTooltip = "Virtual machine", UserFriendlyName = "Machine", DeviceConfigurator = typeof(MachineConfigurator))]
    public sealed class MachineComponent : BaseDevice
    {
        [DeviceIgnoredProperty]
        public string MachineRootDirectory { get; set; }
        [DeviceIgnoredProperty]
        public string MachineName { get; set; }

        public MachineComponent()
        {
            DeviceIcon = DeviceDefaultIcons.Machine;
        }

        /// <summary>
        /// Plays beepcode on system beeper
        /// </summary>
        /// <param name="morseCode">
        /// <para>Code to play</para>
        /// <para>. = short beep</para>
        /// <para>- = long beep</para>
        /// <para>ex. "-.-" - long short long</para>
        /// </param>
        [LuaCallback(Doc = "beep(morseCode | [frequency, length]): nil -- Plays beepcode on system beeper", IsDirect = true)]
        public void beep(string morseCode)
        {
            SoundGenerator.BeepMorse(morseCode);
        }

        /// <summary>
        /// Plays sine wave sound with specified frequency and duration
        /// </summary>
        /// <param name="freq">Sine wave frequency</param>
        /// <param name="length">Sound play duration</param>
        [LuaCallback(Doc = "beep(morseCode | [frequency, length]): nil -- Plays sine on system beeper", IsDirect = true)]
        public void beep(double freq, double length)
        {
            SoundGenerator.PlaySine((float)freq, (float)length);
        }
    }
}
