using craftersmine.LVM.Core.Components;
using craftersmine.LVM.Core.Exceptions;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Represents Lua Virtual Machine instance. This class cannot be inherited
    /// </summary>
    public sealed class Machine
    {
        private MachineComponent machineComponent;
        private Queue<Signal> signals = new Queue<Signal>();

        /// <summary>
        /// Gets current Lua processor
        /// </summary>
        public LuaProcessor Processor { get; private set; }
        /// <summary>
        /// Gets status of machine
        /// </summary>
        public bool IsRunning { get; private set; }
        public string MachineRootDirectory { get; private set; }
        public string MachineName { get; private set; }

        #region Events
        /// <summary>
        /// Occurs when machine signal sent
        /// </summary>
        public event EventHandler<SignalSentEventArgs> SignalSent;
        #endregion

        /// <summary>
        /// Gets current LVM instance
        /// </summary>
        public static Machine RunningInstance { get; private set; }

        /// <summary>
        /// Gets current LVM device bus
        /// </summary>
        public DeviceBus DeviceBus { get; private set; }

        /// <summary>
        /// Creates new LVM instance with specified machine address and display
        /// </summary>
        /// <param name="machineAddress">Machine address</param>
        /// <param name="displayInterface">Display interface</param>
        public Machine(Guid machineAddress, string machineRootDir)
        {
            IsRunning = false;

            MachineRootDirectory = machineRootDir;
            machineComponent = new MachineComponent();

            DeviceBus = new DeviceBus();
            machineComponent.Address = machineAddress;
            Processor = new LuaProcessor();
            Processor.ProcessorHalted += Processor_ProcessorHalted;

            RunningInstance = this;
        }

        private void Processor_ProcessorHalted(object sender, ProcessorHaltEventArgs e)
        {
            IsRunning = false;
            switch (e.Reason)
            {
                case ProcessorHaltReason.Halt:
                    MachineEvents.InvokeMachineHaltedEvent(new MachineHaltedEventArgs() { Reason = MachineHaltReason.ProcessorHalt });
                    ClearState();
                    break;
                case ProcessorHaltReason.Crash:
                    MachineEvents.InvokeMachineHaltedEvent(new MachineHaltedEventArgs() { Reason = MachineHaltReason.Crash, CrashException = e.CrashException });
                    break;
            }
        }

        /// <summary>
        /// Starts up LVM
        /// </summary>
        /// <param name="restart">If true doesn't reconnects system devices</param>
        public void Run(bool restart)
        {
            Processor.IsDebugMode = true;
            IsRunning = true;
            Processor.Run();
        }

        /// <summary>
        /// Stops LVM
        /// </summary>
        public void Stop()
        {
            Processor.Halt();
        }

        public MachineComponent GetMachineComponent()
        {
            return machineComponent;
        }

        /// <summary>
        /// Clears LVM state to prepare for other LVM before run
        /// </summary>
        public void ClearState()
        {
            if (IsRunning)
                throw new MachineClearStateException("Tried to clear state of running machine which could cause big problems in future");

            Processor.Halt();
            signals.Clear();
            DeviceBus.DisconnectAll(false);
            machineComponent = null;
            RunningInstance = null;
        }

        /// <summary>
        /// Gets current machine device address as string
        /// </summary>
        /// <returns>Machine address device</returns>
        public string GetMachineAddress()
        {
            return machineComponent.Address.ToString();
        }

        /// <summary>
        /// Send machine signal with specified name and data
        /// </summary>
        /// <param name="name">Signal name</param>
        /// <param name="data">Signal data</param>
        public void SendSignal(string name, params object[] data)
        {
            Signal signal = new Signal() { Name = name, Data = data };
            signals.Enqueue(signal);
            SignalSent?.Invoke(this, new SignalSentEventArgs() { Signal = signal });
        }

        /// <summary>
        /// Pulls signal on top of signal queue
        /// </summary>
        /// <returns><see cref="Signal"/> object containing signal data</returns>
        public Signal PullSignal()
        {
            return signals.Dequeue();
        }

        public void SaveMachine()
        {
            Settings.LoggerInstance.Log(LogEntryType.Info, "Saving machine data...");

            Settings.LoggerInstance.Log(LogEntryType.Info, "Saving machine devices data...");
            foreach (BaseDevice dev in DeviceBus.GetDevices())
            {
                dev.SaveDevice();
            }
            Settings.LoggerInstance.Log(LogEntryType.Info, "Machine devices data saved!");
            Settings.LoggerInstance.Log(LogEntryType.Info, "Saving machine metadata...");
            saveMachineMetadata(MachineRootDirectory, createMachineMetadata(MachineName, machineComponent.Address));
            Settings.LoggerInstance.Log(LogEntryType.Info, "Machine metadata saved!");

            Settings.LoggerInstance.Log(LogEntryType.Info, "Machine data saved!");
        }

        /// <summary>
        /// Sends <see cref="MachineEvents.MachineReboot"/> event to reboot machine
        /// </summary>
        public static void RebootExistingInstance()
        {
            MachineEvents.InvokeMachineRebootEvent();
        }

        public static IDevice[] LoadDevices(string machineRootDir)
        {
            if (!CheckMachineDirectory(machineRootDir))
                throw new InvalidMachineException("Machine doesn't exists");

            var dir = new DirectoryInfo(Path.Combine(machineRootDir, "devices"));
            var files = dir.GetFiles();

            List<IDevice> devices = new List<IDevice>();

            foreach (var devFile in files)
            {
                if (devFile.Extension == ".lvmd")
                    devices.Add(BaseDevice.LoadDevice(devFile.FullName));
            }

            return devices.ToArray();
        }

        public static Machine LoadMachine(string machineRootDir)
        {
            if (!CheckMachineDirectory(machineRootDir))
            {
                Settings.LoggerInstance.Log(LogEntryType.Error, "Unable to load machine from \"" + machineRootDir + "\"! This directory isn't correct!");
                throw new InvalidMachineException("Machine doesn't exists");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(MachineMetadata));
            TextReader reader = new StreamReader(Path.Combine(machineRootDir, "machine.lvm"));
            MachineMetadata metadata = (MachineMetadata)serializer.Deserialize(reader);

            Machine machine = new Machine(metadata.MachineAddress, machineRootDir);

            var devices = LoadDevices(machineRootDir);
            foreach (var dev in devices)
            {
                machine.DeviceBus.ConnectDevice(dev, dev.Address, true);
            }

            return machine;
        }

        private static void saveMachineMetadata(string machineRootDir, MachineMetadata metadata)
        {
            string metaFile = Path.Combine(machineRootDir, "machine.lvm");

            XmlSerializer serializer = new XmlSerializer(typeof(MachineMetadata));
            TextWriter writer = new StreamWriter(metaFile);
            serializer.Serialize(writer, metadata);
            Settings.LoggerInstance.Log(LogEntryType.Info, "Machine created in \"" + machineRootDir + "\"!");
        }

        private static MachineMetadata createMachineMetadata(string machineName)
        {
            MachineMetadata metadata = new MachineMetadata();
            metadata.MachineAddress = Guid.NewGuid();
            metadata.MachineName = machineName;
            metadata.MachineVersion = new Version(1, 0, 0, 0);
            return metadata;
        }

        private static MachineMetadata createMachineMetadata(string machineName, Guid address)
        {
            MachineMetadata metadata = new MachineMetadata();
            metadata.MachineAddress = address;
            metadata.MachineName = machineName;
            metadata.MachineVersion = new Version(1, 0, 0, 0);
            return metadata;
        }

        public static bool CreateMachine(string name, string machineRootDir)
        {
            try
            {
                if (!Directory.Exists(machineRootDir))
                    Directory.CreateDirectory(machineRootDir);
                string devicesDir = Path.Combine(machineRootDir, "devices");
                if (!Directory.Exists(devicesDir))
                    Directory.CreateDirectory(devicesDir);

                saveMachineMetadata(machineRootDir, createMachineMetadata(name));

                return true;
            }
            catch (Exception ex)
            {
                Settings.LoggerInstance.Log(LogEntryType.Error, "Unable to create machine in \"" + machineRootDir + "\"!");
                Settings.LoggerInstance.LogException(LogEntryType.Error, ex);
                return false;
            }
        }

        public static bool CheckMachineDirectory(string machineRootDir)
        {
            if (!Directory.Exists(machineRootDir))
                return false;
            if (!File.Exists(Path.Combine(machineRootDir, "machine.lvm")))
                return false;
            if (!Directory.Exists(Path.Combine(machineRootDir, "devices")))
                return false;
            return true;
        }
    }
}
