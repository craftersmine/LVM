using craftersmine.LVM.Core.Attributes;
using craftersmine.LVM.Core.Configurators;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Components
{
    /// <summary>
    /// Represents an EEPROM. This class cannot be inherited
    /// </summary>
    [DeviceComponent(ComponentType = "eeprom", DefaultTooltip = "Contains machine firmware", UserFriendlyName = "EEPROM", DeviceConfigurator = typeof(EEPROMConfigurator))]
    public sealed class EEPROM : BaseDevice
    {
        [DeviceIgnoredProperty]
        private string Code { get; set; }
        private Dictionary<string, object> _params = new Dictionary<string, object>();

        public string BiosCodeFilepath { get; set; }
        public bool UseStandardBios { get; set; } = true;

        public EEPROM()
        {
            DeviceIcon = DeviceDefaultIcons.EEPROM;
        }

        /// <summary>
        /// Is EEPROM Read-only
        /// </summary>
        [LuaCallback(IsGetter = true, IsSetter = false)]
        public bool isReadOnly { get; set; }

        /// <summary>
        /// Gets EEPROM max size
        /// </summary>
        [LuaCallback(IsGetter = true, IsSetter = false), DeviceIgnoredProperty]
        public double eepromSize { get { return Settings.EEPROMMaxSize; } }

        /// <summary>
        /// Gets EEPROM code
        /// </summary>
        /// <returns><see cref="string"/> with BIOS Lua code</returns>
        [LuaCallback(Doc = "get(): string -- Gets EEPROM code", IsDirect = true)]
        public string get()
        {
            if (UseStandardBios)
                return File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Common\\bios.lua"));
            else
            {
                try
                {
                    Code = File.ReadAllText(BiosCodeFilepath);
                    return Code;
                }
                catch (Exception ex)
                {
                    Settings.LoggerInstance.Log(LogEntryType.Error, "BIOS-load", "Unable to load BIOS from \"" + BiosCodeFilepath + "\"!");
                    Settings.LoggerInstance.LogException(LogEntryType.Error, "BIOS-load", ex);
                    return "";
                }
            }
        }

        /// <summary>
        /// Writes a new code into EEPROM if it is not Read-Only
        /// </summary>
        /// <param name="code"></param>
        /// <returns>true if it is succeeded</returns>
        [LuaCallback(Doc = "set(code: string): boolean -- Writes code in EEPROM if it read-write, returns true if write succeeded", IsDirect = true)]
        public bool write(string code)
        {
            char[] data = new char[code.Length];
            if (!isReadOnly)
                if (code.Length > Math.Ceiling(eepromSize))
                {

                    Settings.LoggerInstance.Log(LogEntryType.Info, "EEPROM", "Writing EEPROM...");
                    for (int i = 0; i < code.Length; i++)
                    {
                        data[i] = code[i];
                        System.Threading.Thread.Sleep(Settings.EEPROMWriteTime);
                    }
                    Code = new string(data);
                    Settings.LoggerInstance.Log(LogEntryType.Done, "EEPROM", "Writing EEPROM succeeded!");
                    return true;
                }
            Settings.LoggerInstance.Log(LogEntryType.Error, "EEPROM", "Unable to write EEPROM. EEPROM is read-only!");
            return false;

        }

        public void SetCode(string code)
        {
            Code = code;
        }

        /// <summary>
        /// Erases EEPROM if it is not Read-Only
        /// </summary>
        /// <returns>true if it is succeeded</returns>
        [LuaCallback(Doc = "erase(): boolean -- Erases EEPROM if it is not read-only and returns true if erase succeeded", IsDirect = true)]
        public bool erase()
        {
            if (!isReadOnly)
            {
                Settings.LoggerInstance.Log(LogEntryType.Info, "Erasing EEPROM...");
                for (int i = 0; i < Code.Length; i++)
                {
                    System.Threading.Thread.Sleep(Settings.EEPROMWriteTime);
                }
                Code = "";
                Settings.LoggerInstance.Log(LogEntryType.Info, "EEPROM erased!");
                return true;
            }
            Settings.LoggerInstance.Log(LogEntryType.Error, "EEPROM", "Unable to erase EEPROM. EEPROM is read-only!");
            return false;
        }

        /// <summary>
        /// Sets an user-specified parameter with specified key and value
        /// </summary>
        /// <param name="key">Parameter key</param>
        /// <param name="value">Parameter value</param>
        /// <returns>true if value is set, otherwise false</returns>
        [LuaCallback(Doc = "setParam(key: string, value: any): boolean -- Sets user-defined parameter and saves it onto EEPROM, returns true if parameter was set, otherwise false", IsDirect = true)]
        public bool setParam(string key, object value)
        {
            if (_params.Count < Settings.EEPROMMaxParamCount)
            {
                if (_params.ContainsKey(key))
                    _params[key] = value;
                else
                    _params.Add(key, value);
                return true;
            }
            else if (_params.Count == Settings.EEPROMMaxParamCount)
            {
                if (_params.ContainsKey(key))
                {
                    _params[key] = value;
                    return true;
                }
                else return false;
            }
            return false;
        }

        /// <summary>
        /// Gets an user-specified parameter with specified key and value
        /// </summary>
        /// <param name="key">Parameter key</param>
        /// <returns><see cref="object"/> with parameter value or null</returns>
        [LuaCallback(Doc = "getParam(key: string): any -- Gets user-defined parameter saved onto EEPROM and returns it if it exists, otherwise returns nil", IsDirect = true)]
        public object getParam(string key)
        {
            if (_params.ContainsKey(key))
                return _params[key];
            else return null;
        }

        /// <summary>
        /// Removes user-specified parameter from EEPROM
        /// </summary>
        /// <param name="key">Parameter key</param>
        /// <returns>true if it succseeded</returns>
        [LuaCallback(Doc = "clearParam(key: string): boolean -- Clears user-defined parameter from EEPROM, returns true if it succeeded")]
        public bool clearParam(string key)
        {
            if (_params.ContainsKey(key))
            {
                _params.Remove(key);
                return true;
            }
            else return false;
        }

        [LuaCallback(IsDirect = true)]
        public bool makeReadonly()
        {
            isReadOnly = true;
            return isReadOnly;
        }
    }
}
