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
    public class Computer
    {
#pragma warning disable IDE1006 // Naming convension
        /// <summary>
        /// Gets current machine address
        /// </summary>
        public static string address { get { return Machine.RunningInstance.GetMachineAddress(); } }

        /// <summary>
        /// Shutdowns the machine
        /// </summary>
        public static void shutdown()
        {
            Machine.RunningInstance.Stop();
        }

        /// <summary>
        /// Reboots the machine
        /// </summary>
        public static void reboot()
        {
            Machine.RebootExistingInstance();
        }

        /// <summary>
        /// Pushes machine signal with specified name and data
        /// </summary>
        /// <param name="name">Signal name</param>
        /// <param name="data">Signal data</param>
        public static void pushSignal(string name, params object[] data)
        {
            Machine.RunningInstance.SendSignal(name, data);
        }

        /// <summary>
        /// Pulls machine signal from top of queue stack
        /// </summary>
        /// <returns><see cref="LuaTable"/> with signal data</returns>
        public static LuaTable pullSignal()
        {
            var signalData = Machine.RunningInstance.PullSignal();
            LuaTable table = Machine.RunningInstance.Processor.CreateTable();

            for (int i = 1; i <= signalData.Data.Length; i++)
            {
                table[i] = signalData.Data[i - 1];
            }
            table["name"] = signalData.Name;

            return table;
        }

#pragma warning restore IDE1006
    }
}
