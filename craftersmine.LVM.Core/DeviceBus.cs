using craftersmine.LVM.Core.Exceptions;
using craftersmine.LVM.Core.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Represents a machine device bus. This class cannot be inherited
    /// </summary>
    public sealed class DeviceBus
    {
        private Dictionary<Guid, IDevice> devices = new Dictionary<Guid, IDevice>();

        /// <summary>
        /// Connects device to the device bus and sends "machine_device_connected" signal
        /// </summary>
        /// <param name="device">Device to connect</param>
        /// <param name="sendSignal">Should the "machine_device_connected" signal be sent</param>
        /// <param name="deviceAddress">Connecting device address</param>
        public void ConnectDevice(IDevice device, Guid deviceAddress, bool sendSignal)
        {
            device.Address = deviceAddress;
            devices.Add(deviceAddress, device);

            if (!sendSignal)
                return;

            Machine.RunningInstance.SendSignal("machine_device_connected", device.Address.ToString(), device.Info.Class, device.Info.Type);
        }

        /// <summary>
        /// Gets an array of devices connected to the bus
        /// </summary>
        /// <returns><see cref="IDevice"/> array containing connected devices</returns>
        public IDevice[] GetDevices()
        {
            return devices.Values.ToArray();
        }

        /// <summary>
        /// Gets a connected device with specified address and returns it, if device not found returns null
        /// </summary>
        /// <param name="address">Device address to get</param>
        /// <returns><see cref="IDevice"/> if device exists with that address, otherwise null</returns>
        public IDevice GetDevice(string address)
        {
            foreach (var dev in devices)
            {
                if (dev.Key.ToString() == address)
                    return dev.Value;
                else continue;
            }
            return null;
        }

        /// <summary>
        /// Gets a connected device with specified address, converts it to T and returns it, otherwise returns <see cref="IDevice"/> default
        /// </summary>
        /// <typeparam name="T">Device type to convert device</typeparam>
        /// <param name="address">Device address to get</param>
        /// <returns>Device with T type if device exists with that address, otherwise <see cref="IDevice"/> default</returns>
        public T GetDevice<T>(string address)
        {
            var dev = GetDevice(address);
            if (dev.GetType() == typeof(T))
                return (T)dev;
            else return default;
        }

        public T[] GetDevicesOfType<T>(string deviceType)
        {
            List<T> devs = new List<T>();

            foreach (var dev in devices)
            {
                if (dev.Value.GetType().Name.ToLower() == deviceType.ToLower())
                    devs.Add((T)dev.Value);
            }
            return devs.ToArray();
        }

        /// <summary>
        /// Tries to call device method and return it values, otherwise returns null and error message
        /// </summary>
        /// <param name="address">Device address</param>
        /// <param name="method">Device method</param>
        /// <param name="args">Method arguments</param>
        /// <returns></returns>
        public object InvokeDeviceMethod(string address, string method, object[] args)
        {
            var dev = GetDevice(address);
            try
            {
                return dev.InvokeMethod(method, args);
            }
            catch (Exception ex)
            {
                if (ex is NoSuchMethodException)
                    return new object[] { null, MachineErrorMessages.NoSuchMethod };
                else return new object[] { null, ex.Message };
            }
        }

        /// <summary>
        /// Disconnects device from computer
        /// </summary>
        /// <param name="deviceAddress">Address of device to be disconnected</param>
        /// <param name="sendSignal">Should the "machine_device_disconnected" signal be sent</param>
        public void DisconnectDevice(Guid deviceAddress, bool sendSignal)
        {
            if (sendSignal)
                Machine.RunningInstance.SendSignal("machine_device_disconnected", devices[deviceAddress].Address.ToString(), devices[deviceAddress].Info.Class, devices[deviceAddress].Info.Type);

            devices.Remove(deviceAddress);
        }

        /// <summary>
        /// Disconnects all devices
        /// </summary>
        /// <param name="sendSignal">Should the "machine_device_disconnected" signal be sent</param>
        public void DisconnectAll(bool sendSignal)
        {
            devices.Clear();
        }
    }
}
