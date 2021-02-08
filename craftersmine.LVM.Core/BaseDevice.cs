using craftersmine.LVM.Core.Attributes;
using craftersmine.LVM.Core.Exceptions;
using craftersmine.LVM.Core.Extensions;

using NLua;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Represents a base device type. It is recommended to create new device types by inheriting this class
    /// </summary>
    [Serializable, XmlRoot(ElementName = "Device")]
    public class BaseDevice : IDevice
    {
        /// <summary>
        /// Gets or sets device address
        /// </summary>
        [XmlAttribute(AttributeName = "address")]
        public Guid Address { get; set; }
        /// <summary>
        /// Gets or sets device info
        /// </summary>
        [DeviceIgnoredProperty]
        public DeviceInfo Info { get; set; }
        [DeviceIgnoredProperty]
        public Image DeviceIcon { get; set; } = DeviceDefaultIcons.Generic;

        /// <summary>
        /// Gets device address as string. Has a <see cref="LuaCallbackAttribute"/>
        /// </summary>
        /// <returns>Device address <see cref="string"/></returns>
        [LuaCallback(Doc = "getAddress(): string -- Returns device address")]
        public string getAddress()
        {
            return Address.ToString();
        }

        /// <summary>
        /// Gets device info table. Has a <see cref="LuaCallbackAttribute"/>
        /// </summary>
        /// <returns><see cref="LuaTable"/> with device info</returns>
        [LuaCallback(Doc = "getDeviceInfo(): table -- Returns device info table")]
        public LuaTable getDeviceInfo()
        {
            LuaTable table = Machine.RunningInstance.Processor.CreateTable();

            if (Info.Class.IsNullEmptyOrWhitespace())
                table["class"] = Info.Class;
            if (Info.Name.IsNullEmptyOrWhitespace())
                table["name"] = Info.Name;
            if (Info.Description.IsNullEmptyOrWhitespace())
                table["description"] = Info.Description;
            if (Info.Model.IsNullEmptyOrWhitespace())
                table["model"] = Info.Model;
            if (Info.Vendor.IsNullEmptyOrWhitespace())
                table["vendor"] = Info.Vendor;
            if (Info.Type.IsNullEmptyOrWhitespace())
                table["type"] = Info.Type;
            if (Info.Serial.IsNullEmptyOrWhitespace())
                table["serial"] = Info.Serial;
            table["id"] = Info.Id;
            table["version"] = Info.Version.ToString();
            
            foreach(var devInfoParam in Info.VendorParameters)
            {
                table[devInfoParam.Key.ToString()] = devInfoParam.Value;
            }

            return table;
        }

        /// <summary>
        /// Gets <see cref="DeviceComponentAttribute"/> of this device
        /// </summary>
        /// <returns><see cref="DeviceComponentAttribute"/> of device class</returns>
        public DeviceComponentAttribute GetComponentAttribute()
        {
            DeviceComponentAttribute attribute = (DeviceComponentAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(DeviceComponentAttribute));
            if (attribute == null)
                throw new InvalidDeviceException(this.GetType().ToString() + " type is not valid device type! Did you forgot to add DeviceComponentAttribute?");
            return attribute;
        }

        /// <summary>
        /// Gets Lua callbacks infos of device
        /// </summary>
        /// <returns><see cref="LuaCallbackInfo"/> array with methods with <see cref="LuaCallbackAttribute"/> attribute</returns>
        public LuaCallbackInfo[] GetLuaCallbacksInfos()
        {
            List<LuaCallbackInfo> luaCallbacks = new List<LuaCallbackInfo>();

            var callbacks = this.GetLuaCallbacks();

            foreach (var callback in callbacks)
            {
                LuaCallbackInfo luaCallbackMethodInfo = new LuaCallbackInfo();
                var attrib = this.GetLuaCallbackAttribute(callback.Name);
                luaCallbackMethodInfo.Name = callback.Name;
                luaCallbackMethodInfo.Doc = attrib.Doc;
                luaCallbackMethodInfo.IsGetter = attrib.IsGetter;
                luaCallbackMethodInfo.IsSetter = attrib.IsSetter;
                luaCallbackMethodInfo.IsDirect = attrib.IsDirect;
                luaCallbacks.Add(luaCallbackMethodInfo);
            }

            return luaCallbacks.ToArray();
        }

        /// <summary>
        /// Gets Lua fields infos of device
        /// </summary>
        /// <returns><see cref="LuaCallbackInfo"/> array with properties with <see cref="LuaCallbackAttribute"/> attribute</returns>
        public LuaCallbackInfo[] GetLuaFieldsInfos()
        {
            List<LuaCallbackInfo> luaFields = new List<LuaCallbackInfo>();

            var fields = this.GetLuaFields();

            foreach (var field in fields)
            {
                LuaCallbackInfo luaCallbackMethodInfo = new LuaCallbackInfo();
                var attrib = this.GetLuaCallbackAttribute(field.Name);
                luaCallbackMethodInfo.Name = field.Name;
                luaCallbackMethodInfo.Doc = attrib.Doc;
                luaCallbackMethodInfo.IsGetter = attrib.IsGetter;
                luaCallbackMethodInfo.IsSetter = attrib.IsSetter;
                luaCallbackMethodInfo.IsDirect = attrib.IsDirect;
                luaFields.Add(luaCallbackMethodInfo);
            }

            return luaFields.ToArray();
        }

        public void SaveDevice()
        {
            string devFilePath = Path.Combine(Machine.RunningInstance.MachineRootDirectory, "devices", this.GetType().Name.ToLower() + "_" + Address + ".lvmd");
            if (!Directory.Exists(Path.Combine(Machine.RunningInstance.MachineRootDirectory, "devices")))
                Directory.CreateDirectory(Path.Combine(Machine.RunningInstance.MachineRootDirectory, "devices"));
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            TextWriter writer = new StreamWriter(devFilePath);
            serializer.Serialize(writer, this);
            writer.Close();
        }

        public static IDevice LoadDevice(string filename)
        {
            Settings.LoggerInstance.Log(LogEntryType.Info, "device-loaddevice", "Trying to load device from \"" + filename + "\"...");
            string devType = Path.GetFileNameWithoutExtension(filename).Split('_')[0];
            TextReader reader = new StreamReader(filename);
            Type registeredDevType = DeviceTypeRegistry.GetRegisteredDeviceType(devType);
            if (registeredDevType == null)
            {
                Settings.LoggerInstance.Log(LogEntryType.Error, "device-loaddevice", "Unable to load device from \"" + filename + "\"! Device type is not registered!");
                return null;
            }
            XmlSerializer serializer = new XmlSerializer(registeredDevType);
            serializer.UnknownAttribute += Serializer_UnknownAttribute;
            var deserializedDev = serializer.Deserialize(reader);
            reader.Close();
            if (deserializedDev != null)
                return (IDevice)deserializedDev;
            else throw new DeviceLoadException("Unable to load device");
        }

        private static void Serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            Settings.LoggerInstance.Log(LogEntryType.Warning, "device-loaddevice", "Unknown attribute found while loading device: " + e.Attr.Name);
        }
    }
}
