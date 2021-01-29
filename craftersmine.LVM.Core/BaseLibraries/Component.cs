using craftersmine.LVM.Core.Exceptions;
using craftersmine.LVM.Core.Extensions;

using NLua;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.BaseLibraries
{
    /// <summary>
    /// Basic LibComputer Lua module API
    /// </summary>
    public class Component
    {
#pragma warning disable IDE1006 // Naming convension
        /// <summary>
        /// Returns a documentation of specified method of specified device
        /// </summary>
        /// <param name="address">Device address</param>
        /// <param name="method">Method name</param>
        /// <returns><see cref="string"/> with documentation</returns>
        public static string doc(string address, string method)
        {
            var device = Machine.RunningInstance.DeviceBus.GetDevice(address);
            var attribute = device.GetLuaCallbackAttribute(method);
            if (attribute != null)
                return attribute.Doc;
            else return "";
        }

        /// <summary>
        /// Tries to invoke specified method at specified device with specified arguments
        /// </summary>
        /// <param name="address">Device address</param>
        /// <param name="method">Invoking method name</param>
        /// <param name="args">Invoking method arguments</param>
        /// <returns><see cref="LuaTable"/> with invoked method returned values</returns>
        /// <exception cref="LuaMachineException">When method or component not found</exception>
        public static LuaTable invoke(string address, string method, params object[] args)
        {
            LuaTable table = Machine.RunningInstance.Processor.CreateTable();

            var device = Machine.RunningInstance.DeviceBus.GetDevice(address);
            if (device == null)
                throw new LuaMachineException("Unable to find device with address " + address, MachineErrorMessages.NoSuchComponent);

            try
            {
                var invokeResult = device.InvokeMethod(method, args);

                if (invokeResult != null)
                {
                    if (invokeResult.GetType().BaseType == typeof(Array))
                    {
                        var arr = (object[])invokeResult;
                        for (int i = 1; i <= arr.Length; i++)
                        {
                            table[i] = arr[i - 1];
                        }
                    }
                    else
                    {
                        table[1] = invokeResult;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is NoSuchMethodException)
                    throw new LuaMachineException(ex.Message, MachineErrorMessages.NoSuchMethod);
                else throw ex;
            }

            return table;
        }

        /// <summary>
        /// Gets a list of connected devices to the machine with specified filter
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="exact">true if filter needs to be exact</param>
        /// <returns><see cref="LuaTable"/> with connected devices</returns>
        public static LuaTable list(string filter, bool exact)
        {
            var table = Machine.RunningInstance.Processor.CreateTable();

            var devices = Machine.RunningInstance.DeviceBus.GetDevices();
            foreach (var dev in devices)
            {
                string devName = dev.GetDeviceTypeName();
                if (filter != null || filter != string.Empty || !string.IsNullOrWhiteSpace(filter))
                {
                    if (!exact) 
                        if (devName.Contains(filter))
                            table[dev.Address.ToString()] = dev.GetDeviceTypeName();
                    else 
                        if (devName == filter)
                            table[dev.Address.ToString()] = dev.GetDeviceTypeName();
                }
                else
                    table[dev.Address.ToString()] = dev.GetDeviceTypeName();
            }

            return table;
        }

        /// <summary>
        /// Gets methods and fields of specified device
        /// </summary>
        /// <param name="address">Device address</param>
        /// <returns><see cref="LuaTable"/> with device methods and fields</returns>
        public static LuaTable methods(string address)
        {
            var table = Machine.RunningInstance.Processor.CreateTable();

            var device = Machine.RunningInstance.DeviceBus.GetDevice(address);
            if (device == null)
                throw new LuaMachineException("Unable to find device with address " + address, MachineErrorMessages.NoSuchComponent);

            var callbacksMethodsInfos = device.GetLuaCallbacksInfos();
            var callbacksFieldsInfos = device.GetLuaFieldsInfos();

            foreach (var callbackInfo in callbacksMethodsInfos)
            {
                var tbl = Machine.RunningInstance.Processor.CreateTable();
                tbl["direct"] = callbackInfo.IsDirect;
                tbl["getter"] = callbackInfo.IsGetter;
                tbl["setter"] = callbackInfo.IsSetter;

                table[callbackInfo.Name] = tbl;
            }

            foreach (var fieldInfo in callbacksFieldsInfos)
            {
                var tbl = Machine.RunningInstance.Processor.CreateTable();
                tbl["direct"] = fieldInfo.IsDirect;
                tbl["getter"] = fieldInfo.IsGetter;
                tbl["setter"] = fieldInfo.IsSetter;

                table[fieldInfo.Name] = tbl;
            }

            return table;
        }

        /// <summary>
        /// Gets a value of specified device field
        /// </summary>
        /// <param name="address">Device address</param>
        /// <param name="field">Field name</param>
        /// <returns><see cref="object"/> as value of field</returns>
        /// <exception cref="LuaMachineException">When field is not found</exception>
        public static object getFieldValue(string address, string field)
        {
            var device = Machine.RunningInstance.DeviceBus.GetDevice(address);
            if (device == null)
                throw new LuaMachineException("Unable to find device with address " + address, MachineErrorMessages.NoSuchComponent);

            try
            {
                return device.GetPropertyValue(field);
            }
            catch (Exception ex)
            {
                if (ex is NoSuchPropertyException)
                    throw new LuaMachineException(ex.Message, MachineErrorMessages.NoSuchField);
                else throw ex;
            }
        }

        /// <summary>
        /// Sets a value of specified device field
        /// </summary>
        /// <param name="address">Device address</param>
        /// <param name="field">Field name</param>
        /// <param name="value">Field value</param>
        /// <exception cref="LuaMachineException">When field is not found</exception>
        public static void setFieldValue(string address, string field, object value)
        {
            var device = Machine.RunningInstance.DeviceBus.GetDevice(address);
            if (device == null)
                throw new LuaMachineException("Unable to find device with address " + address, MachineErrorMessages.NoSuchComponent);

            try
            {
                device.SetPropertyValue(field, value);
            }
            catch (Exception ex)
            {
                if (ex is NoSuchPropertyException)
                    throw new LuaMachineException(ex.Message, MachineErrorMessages.NoSuchField);
                else throw ex;
            }
        }

        /// <summary>
        /// Returns a <see cref="string"/> with type of specified device
        /// </summary>
        /// <param name="address">Device address</param>
        /// <returns></returns>
        public static string type(string address)
        {
            var device = Machine.RunningInstance.DeviceBus.GetDevice(address);
            if (device != null)
                return device.GetDeviceTypeName();
            else throw new LuaMachineException("Unable to find device with address " + address, MachineErrorMessages.NoSuchComponent);
        }

#pragma warning restore IDE1006
    }
}
