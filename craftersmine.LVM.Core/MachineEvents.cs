using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Containing static events and methods to invoke those methods
    /// </summary>
    public static class MachineEvents
    {
        /// <summary>
        /// Occurs when machine switching state to halted if shutdowns or crashes
        /// </summary>
        public static event EventHandler<MachineHaltedEventArgs> MachineHalted;
        /// <summary>
        /// Occurs when machine user-code invokes computer.reboot() function and requests restart
        /// </summary>
        public static event EventHandler MachineReboot;

        /// <summary>
        /// Raises <see cref="MachineHalted"/> event with specified arguments
        /// </summary>
        /// <param name="machineHaltedEventArgs"><see cref="MachineHalted"/> event arguments</param>
        public static void InvokeMachineHaltedEvent(MachineHaltedEventArgs machineHaltedEventArgs)
        {
            MachineHalted?.Invoke(Machine.RunningInstance, machineHaltedEventArgs);
        }

        /// <summary>
        /// Raises <see cref="MachineReboot"/> event
        /// </summary>
        public static void InvokeMachineRebootEvent()
        {
            MachineReboot?.Invoke(Machine.RunningInstance, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Contains information about machine halt event. This class cannot be inherited
    /// </summary>
    public sealed class MachineHaltedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets machine halt reason
        /// </summary>
        public MachineHaltReason Reason { get; set; }
        /// <summary>
        /// Gets or sets machine crash halt exception data
        /// </summary>
        public Exception CrashException { get; set; }
    }

    /// <summary>
    /// Contains information about sent machine signal. This class cannot be inherited
    /// </summary>
    public sealed class SignalSentEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets sent signal
        /// </summary>
        public Signal Signal { get; set; }
    }

    /// <summary>
    /// Defines machine halt reason
    /// </summary>
    public enum MachineHaltReason
    {
        /// <summary>
        /// Machine halt caused by machine shutdown
        /// </summary>
        Shutdown,
        /// <summary>
        /// Machine halt caused by processor halt
        /// </summary>
        ProcessorHalt,
        /// <summary>
        /// Machine halt caused by reboot
        /// </summary>
        Reboot,
        /// <summary>
        /// Machine halt caused by crash
        /// </summary>
        Crash
    }
}
